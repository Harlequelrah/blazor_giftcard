using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace blazor_giftcard.Models
{
    public class Subscription
    {

        public int Id { get; set; }

        public int IdPackage { get; set; }

        public int? NbrCarteGenere { get; set; }

        public double BudgetRestant {  get ; set; }

        public int IdSubscriber {get; set;}

        public string DateSouscription { get; set; }

        public DateTime? DateExpiration { get; set; }

        public double? MontantParCarte { get; set; }

    }
}
