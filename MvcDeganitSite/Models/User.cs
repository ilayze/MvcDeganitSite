using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcDeganitSite.Models
{
    public class User
    {
        [Key]
        public string Name { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}