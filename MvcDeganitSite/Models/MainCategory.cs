using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcDeganitSite.Models
{
    public class MainCategory
    {
        public int MainCategoryId { get; set; }
        [Required(ErrorMessage="נא להכניס שם")]
        public string Name { get; set; }
        public string picture { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
       

    }
    
}