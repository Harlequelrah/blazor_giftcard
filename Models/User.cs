using System;

namespace blazor_giftcard.Models

{
    public class User
    {
        public int Id { get; set; }

        public string NomRole { get; set; }

        public string NomComplet { get; set; }

        public string Email { get; set; }
        public string Telephone { get; set; }
        public string DateInscription { get; set; }
        public string Adresse { get; set; }
        public bool IsActive { get; set; }

    }

}
