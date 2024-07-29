using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace blazor_giftcard.Models
{
    public class SubscriberWallet
    {
        public int Id { get; set; }
        public double Solde { get; set; }
        public string Devise { get; set; }
    }

}
