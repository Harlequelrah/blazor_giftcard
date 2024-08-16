
using System;
using System.ComponentModel.DataAnnotations;
namespace blazor_giftcard.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string NomPackage { get; set; }
        public int?  NbrJour{ get; set; }
        public double Budget{ get; set; }
        public double Prix {get; set; }

        public double MontantBase { get; set; }
        public string Description { get; set; }
    }
    public class PackageDto
    {
        public string NomPackage { get; set; }
        public int?  NbrJour{ get; set; }
        public double Budget{ get; set; }
        public double Prix {get; set; }

        public double MontantBase { get; set; }
        public string Description { get; set; }
    }
}
