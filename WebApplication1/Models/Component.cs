using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Component
    {
        public int Id { get; set; }

        [Required]
        public string Naam { get; set; }

        public string Datasheet { get; set; }
        public int Aantal { get; set; }
        public int Aankoopprijs { get; set; }

        public int IdCategorie { get; set; }
        public Categorie Categorie { get; set; }
    }
}