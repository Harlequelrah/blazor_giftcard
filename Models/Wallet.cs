using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace blazor_giftcard.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public double Solde { get; set; }
        public string Devise { get; set; }
    }
    public class MerchantWallet : Wallet
    {
        public MerchantWallet() : base() { }
    }
    public class SubscriberWallet : Wallet
    {
        public SubscriberWallet() : base() { }
    }
    public class BeneficiaryWallet : Wallet
    {
        public BeneficiaryWallet() : base() { }
    }

}
