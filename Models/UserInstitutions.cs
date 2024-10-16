using StockTrack_API.Models.Interfaces.Enums;

namespace StockTrack_API.Models
{
    public class UserInstitution
    {
        // Faz a 'ponte' entre usuário e instituição
        // É a relação de muitos para muitos entre usuários e instituições
        // Tabela associativa atributiva (keys => UserId, InstitutionId)
        public int UserId { get; set; }
        public User? User { get; set; }

        public int InstitutionId { get; set; }
        public Institution? Institution { get; set; }

        // Define se o usuário está ativo na instituição
        public bool Active { get; set; } = true;
        // Define a permissão do usuário na instituição
        public EUserRole UserRole { get; set; }
    }
}
