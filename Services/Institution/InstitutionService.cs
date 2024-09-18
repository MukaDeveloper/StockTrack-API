using System.Security.Claims;

namespace StockTrack_API.Utils
{
    public class InstitutionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InstitutionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetInstitutionId()
        {
            string? contextAcessor =
                (_httpContextAccessor.HttpContext?.User.FindFirstValue("institutionId"))
                ?? throw new Exception("Requisição inválida.");
            int? institutionId = int.Parse(contextAcessor);

            if (!institutionId.HasValue)
            {
                throw new Exception("Identificação da instituição não localizada.");
            }

            return institutionId.Value;
        }
    }
}
