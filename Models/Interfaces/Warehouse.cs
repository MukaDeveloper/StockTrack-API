using System.Text.Json.Serialization;

namespace StockTrack_API.Models.Interfaces
{
    public class Warehouse : BaseModel
    {
        // Informações básicas do Almoxarifado
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Associação do vínculo de materiais com almoxarifados
        public List<MaterialWarehouses>? MaterialWarehouses { get; set; }
        public List<WarehouseUsers>? WarehouseUsers { get; set; }

        // Informações da Área que o Almoxarifado pertence
        public int AreaId { get; set; }
        public Area? Area { get; set; }
    }
}
