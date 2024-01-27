﻿using System.ComponentModel.DataAnnotations;

namespace ExamenOpdracht.Models
{
        public class User
        {

            [Display(Name = "Gebruiker")]
            public string UserName { get; set; }

            [Display(Name = "Voornaam")]
            public string FirstName { get; set; }

            [Display(Name = "Familienaam")]
            public string LastName { get; set; }

            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Display(Name = "Geblokkeerd")]
            public bool Blocked { get; set; }

            [Display(Name = "Rollen")]
            public List<string> Roles { get; set; }

        }
    public class UserRolesViewModel
    {
        [Display(Name = "Gebruiker")]
        public string UserName { get; set; }

        [Display(Name = "Rollen")]
        public List<string> Roles { get; set; }
    }
}

