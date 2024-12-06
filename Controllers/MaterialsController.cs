using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTrack_API.Data;
using StockTrack_API.Models.Interfaces.Enums;
using StockTrack_API.Models;
using StockTrack_API.Services;
using StockTrack_API.Utils;
using StockTrack_API.Models.Interfaces.Response.Material;
using StockTrack_API.Models.Interfaces.Request;
using StockTrack_API.Models.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;

namespace StockTrack_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class MaterialsController : ControllerBase
    {
        // Contexto do banco de dados
        private readonly DataContext _context;

        // Serviços
        private readonly InstitutionService _institutionService;
        private readonly UserService _userService;
        private readonly MovimentationService _movimentationService;

        // Construtor
        public MaterialsController(
            DataContext context,
            InstitutionService institutionService,
            MovimentationService movimentationService,
            UserService userService
        )
        {
            _context = context;
            _institutionService = institutionService;
            _movimentationService = movimentationService;
            _userService = userService;
        }

        // Busca pelo ID do Material
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            try
            {
                // Busca o ID da instituição pelo serviço, pelo httpContextAcessor
                int institutionId = _institutionService.GetInstitutionId();

                // Busca o material pelo ID e pelo institutionId no banco de dados através do contexto
                Material? material = await _context.ST_MATERIALS
                    .Include(m => m.Status)
                    .FirstOrDefaultAsync(m =>
                        m.Id == id && m.InstitutionId == institutionId && m.Active == true
                    );

                // O resultado pode ser null
                if (material == null)
                {
                    return NotFound();
                }

                return Ok(EnvelopeFactory.factoryEnvelope(material));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync(int lastDocId = 0, int limit = 20)
        {
            try
            {
                int institutionId = _institutionService.GetInstitutionId();

                List<Material> list = await _context
                    .ST_MATERIALS.Where(m => m.InstitutionId == institutionId && m.Id > lastDocId && m.Active == true)
                    .OrderBy(m => m.Id)
                    .Take(limit)
                    .Include(m => m.Status)
                    .Include(m => m.MaterialWarehouses)
                    .ToListAsync();

                List<GetAllRes> materials = list.Select(material => new GetAllRes
                {
                    Id = material.Id,
                    Name = material.Name,
                    Description = material.Description,
                    ImageURL = material.ImageURL,
                    Manufacturer = material.Manufacturer,
                    RecordNumber = material.RecordNumber,
                    MaterialType = material.MaterialType.ToString(),
                    Measure = material.Measure,
                    Status = material.Status.Select(status => new MaterialStatusDto
                    {
                        Status = status.Status.ToString(),
                        Quantity = status.Quantity
                    }
                    ).ToList(),
                    MaterialWarehouses = material.MaterialWarehouses.Select(
                        mw => mw.WarehouseId).ToList(),
                }).ToList();

                return Ok(EnvelopeFactory.factoryEnvelopeArray(materials));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("add-new")]
        public async Task<IActionResult> AddAsync(AddNewMaterialReq newMaterial)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Recuperação de informações do usuário e validação de permissão
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserRole == EUserRole.USER || !userInstitution.Active)
                    throw new Exception("Sem autorização.");

                // Validação de campos obrigatórios
                if (string.IsNullOrWhiteSpace(newMaterial.Name))
                    throw new Exception("Nome do material é obrigatório.");

                if (newMaterial.WarehouseId <= 0)
                    throw new Exception("Área do armazém é obrigatória.");

                // Verificação de almoxarifado
                Warehouse? wh = await _context.ST_WAREHOUSES
                    .FirstOrDefaultAsync(w => w.Id == newMaterial.WarehouseId && w.Active && w.InstitutionId == institutionId);

                if (wh == null)
                    throw new Exception("Almoxarifado não encontrado ou inválido!");

                // Verificação de materiais já existentes com o mesmo registro
                Material? existingMaterial = await _context.ST_MATERIALS
                    .Where(m => m.InstitutionId == institutionId && m.Active == true)
                    .FirstOrDefaultAsync(m => m.RecordNumber == newMaterial.RecordNumber);

                if (existingMaterial != null)
                    throw new Exception("Número de registro já utilizado para outro material ativo!");

                // Validação específica de tipo do material
                var materialType = Enum.Parse<EMaterialType>(newMaterial.MaterialType);

                switch (materialType)
                {
                    case EMaterialType.LOAN:
                        if (newMaterial.quantity <= 0) newMaterial.quantity = 1;
                        if (newMaterial.RecordNumber <= 0)
                            throw new Exception("Número de registro inválido!");
                        break;

                    case EMaterialType.CONSUMPTION:
                        if (newMaterial.quantity <= 0)
                            throw new Exception("Quantidade inválida!");
                        break;
                }

                // Criação do material
                Material materialToAdd = new()
                {
                    Active = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Name,
                    Description = newMaterial.Description,
                    ImageURL = newMaterial.ImageURL,
                    InstitutionId = institutionId,
                    Manufacturer = newMaterial.Manufacturer,
                    MaterialType = materialType,
                    Measure = newMaterial.Measure,
                    Name = newMaterial.Name,
                    RecordNumber = newMaterial.RecordNumber,
                    Status = new List<MaterialStatus>
            {
                new MaterialStatus
                {
                    Quantity = newMaterial.quantity,
                    Status = EMaterialStatus.AVAILABLE
                }
            },
                };

                // Adiciona e salva o material
                await _context.ST_MATERIALS.AddAsync(materialToAdd);
                await _context.SaveChangesAsync();

                // Busca e valida material adicionado
                Material? res = await _context.ST_MATERIALS
                    .FirstOrDefaultAsync(m => m.Id == materialToAdd.Id && m.Active);

                if (res == null)
                    throw new Exception("Houve um erro ao referenciar seu material. Verifique se o mesmo foi adicionado!");

                // Confirma transação
                await transaction.CommitAsync();
                // Registra a movimentação
                await _movimentationService.AddMaterial(institutionId, materialToAdd.Id, user.Name, materialToAdd.Name, materialToAdd.Description, materialToAdd.Quantity);

                return Ok(EnvelopeFactory.factoryEnvelope(res));
            }
            catch (Exception ex)
            {
                // Reverte transação em caso de erro
                await transaction.RollbackAsync();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UpdateMaterialReq updatedMaterial)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Recupera informações do usuário e valida permissão
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserRole == EUserRole.USER || !userInstitution.Active)
                    throw new Exception("Sem autorização.");

                // Busca o material existente para atualização
                Material? material = await _context.ST_MATERIALS
                    .Where(m => m.InstitutionId == institutionId && m.Active)
                    .Include(m => m.MaterialWarehouses) // Inclui relação N:N
                    .FirstOrDefaultAsync(m => m.Id == updatedMaterial.Id);

                if (material == null)
                    throw new Exception("Material não encontrado ou inativo!");

                // Validação de registro único, se alterado
                if (updatedMaterial.RecordNumber.HasValue && updatedMaterial.RecordNumber != material.RecordNumber)
                {
                    bool recordExists = await _context.ST_MATERIALS
                        .Where(m => m.InstitutionId == institutionId && m.Active)
                        .AnyAsync(m => m.RecordNumber == updatedMaterial.RecordNumber);

                    if (recordExists)
                        throw new Exception("Número de registro já utilizado para outro material ativo!");
                }

                // Atualiza os campos fornecidos
                material.Name = updatedMaterial.Name ?? material.Name;
                material.Description = updatedMaterial.Description ?? material.Description;
                material.ImageURL = updatedMaterial.ImageURL ?? material.ImageURL;
                material.Manufacturer = updatedMaterial.Manufacturer ?? material.Manufacturer;
                material.MaterialType = updatedMaterial.MaterialType != null
                    ? Enum.Parse<EMaterialType>(updatedMaterial.MaterialType)
                    : material.MaterialType;
                material.Measure = updatedMaterial.Measure ?? material.Measure;
                material.RecordNumber = updatedMaterial.RecordNumber ?? material.RecordNumber;
                material.Active = updatedMaterial.Active ?? material.Active;
                material.UpdatedAt = DateTime.Now;
                material.UpdatedBy = user.Name;

                // Verifica e vincula o almoxarifado, se enviado
                if (updatedMaterial.WarehouseId.HasValue)
                {
                    Warehouse? warehouse = await _context.ST_WAREHOUSES
                        .FirstOrDefaultAsync(w => w.Id == updatedMaterial.WarehouseId && w.Active && w.InstitutionId == institutionId);

                    if (warehouse == null)
                        throw new Exception("Almoxarifado inválido ou inativo!");

                    // Verifica se o vínculo já existe
                    bool warehouseLinked = material.MaterialWarehouses.Any(mw => mw.WarehouseId == warehouse.Id);
                    if (!warehouseLinked)
                    {
                        material.MaterialWarehouses.Add(new MaterialWarehouses
                        {
                            MaterialId = material.Id,
                            WarehouseId = warehouse.Id,
                        });
                    }
                }

                // Atualiza o material no banco de dados
                _context.ST_MATERIALS.Update(material);
                await _context.SaveChangesAsync();

                // Confirma transação
                await transaction.CommitAsync();
                // Registra a movimentação
                await _movimentationService.UpdateMaterial(institutionId, material.Id, user.Name, material.Name, material.Description, material.Quantity);

                return Ok(EnvelopeFactory.factoryEnvelope(material));
            }
            catch (Exception ex)
            {
                // Reverte transação em caso de erro
                await transaction.RollbackAsync();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{materialId}")]
        public async Task<IActionResult> DeleteAsync(int materialId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int institutionId = _institutionService.GetInstitutionId();
                var (user, userInstitution) = _userService.GetUserAndInstitution(institutionId);

                if (userInstitution.UserRole == EUserRole.USER || userInstitution.Active == false)
                {
                    throw new Exception("Sem autorização.");
                }

                Material? materialToDelete =
                    await _context.ST_MATERIALS.FirstOrDefaultAsync(x => x.Id == materialId && x.Active == true)
                    ?? throw new Exception("Material não encontrado");

                List<Solicitation>? listSolicitationMaterials = await _context.ST_SOLICITATIONS
                    .Include(s => s.Items) // Inclui os itens relacionados
                    .Where(s => s.Items.Any(i => i.MaterialId == materialId) && (s.Status != ESolicitationStatus.RETURNED || s.Status != ESolicitationStatus.DECLINED))
                    .ToListAsync();

                if (listSolicitationMaterials.Count > 0)
                {
                    throw new Exception(
                        $"Almoxarifado não pode ser excluído pois contém {listSolicitationMaterials.Count} solicitações ativas ou pendentes. Recuse-as para prosseguir."
                    );
                }

                await _movimentationService.DeleteMaterial(
                    institutionId,
                    materialToDelete.Id,
                    user.Name,
                    materialToDelete.Name
                );

                materialToDelete.Active = false;
                materialToDelete.UpdatedAt = DateTime.Now;
                materialToDelete.UpdatedBy = user.Name;

                _context.ST_MATERIALS.Update(materialToDelete);
                await _context.SaveChangesAsync();

                // Confirma transação
                await transaction.CommitAsync();
                // Registra a movimentação
                await _movimentationService.UpdateMaterial(institutionId, materialToDelete.Id, user.Name, materialToDelete.Name, materialToDelete.Description, materialToDelete.Quantity);

                return Ok();
            }
            catch (Exception ex)
            {
                // Reverte transação em caso de erro
                await transaction.RollbackAsync();
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
