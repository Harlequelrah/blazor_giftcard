
namespace blazor_giftcard.Models
{
        public class BeneficiaryDto
        {

                public int IdSubscription { get; set; }

                public string Email { get; set; }


                public string? TelephoneNumero { get; set; }

                public string Nom { get; set; }

                public string Prenom { get; set; }

                public bool Has_gochap { get; set; }

        }
        public class Beneficiary
        {
                public int Id { get; set; }
                public int? IdUser { get; set; }

                public int IdSubscriber { get; set; }

                public int IdBeneficiaryWallet { get; set; }

                public string Nom { get; set; }

                public string Prenom { get; set; }

                public string TelephoneNumero { get; set; }

                public bool Has_gochap { get; set; }

                public byte[]? ProfilePhoto { get; set; }

        }
}
