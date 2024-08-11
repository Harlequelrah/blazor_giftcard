using System;

namespace blazor_giftcard.Models
{
    public class Merchant
    {

        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdMerchantWallet { get; set; }
        public MerchantWallet MerchantWallet { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string? ProfilPhoto { get; set; }
    }
}
