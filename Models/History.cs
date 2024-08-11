using System;

namespace blazor_giftcard.Models
{
        public  class Historique
    {
        public int Id { get; set;}
        public string Action { get; set;}
        public string Date { get; set;}
        public double Montant { get; set;}
    }
    public class SubscriberHistory : Historique
    {
        public int IdSubscriber { get; set; }
        public SubscriberHistory():base()
        {
        }
        public SubscriberHistory(int idsubscriber):base()
        {
            IdSubscriber = idsubscriber;
        }
    }
    public class BeneficiaryHistory : Historique
    {
        public int IdBeneficiary { get; set; }
        public BeneficiaryHistory():base()
        {
        }
        public BeneficiaryHistory(int idBeneficiary):base()
        {
            IdBeneficiary = idBeneficiary;
        }
    }
    public class MerchantHistory : Historique
    {
        public int IdMerchant { get; set; }
        public MerchantHistory():base()
        {
        }
        public MerchantHistory(int idMerchant):base()
        {
            IdMerchant = idMerchant;
        }
    }
}
