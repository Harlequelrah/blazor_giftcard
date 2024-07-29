
using System.Collections.Generic;

using System;


namespace blazor_giftcard.Models
{
    public class SubscriptionDto
    {


        public int IdPackage { get; set; }

        public int? NbrCarteGenere { get; set; } = 0;


        public int IdSubscriber { get; set; }
        public double? MontantParCarte { get; set; } = null;

    }
}
