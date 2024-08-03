using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace blazor_giftcard.Models
{
    public class Subscriber
    {

        public int Id { get; set; }


        public int IdUser { get; set; }

        public int IdSubscriberWallet { get; set; }


        public string SubscriberName { get; set; }

        public SubscriberWallet SubscriberWallet { get; set; }




    }
}
