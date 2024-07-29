
namespace blazor_giftcard.Models
{
    public class BeneficiaryDto
    {

        public int IdSubscription { get; set; }

        public int IdPackage { get; set; }

           public string Email { get; set; }



        public string Nom { get; set; }

        public string Prenom { get; set; }

        public bool Has_gochap { get; set; }

    }
}
