using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace blazor_giftcard.Models
{
    public class SubscriberHistory
    {
        public int Id { get; set; }
        public double Montant { get; set; }

        public string Date { get; set; }
        public int IdSubscriber { get; set; }
        public SubscriberActions Action { get; set; }

        public enum SubscriberActions
        {
            Initial,
            Enregistrement,
            Souscription,
            PackageExpiration,
        }

    }


}
