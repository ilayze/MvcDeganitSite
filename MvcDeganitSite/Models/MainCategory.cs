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
        [Display(Name="שם הקטגוריה")]
        public string Name { get; set; }
        [Display(Name="נתיב לתמונה")]
        public string picture { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
       

    }
    
}