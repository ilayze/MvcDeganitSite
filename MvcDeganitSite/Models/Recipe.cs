using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcDeganitSite.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }

        [Required(ErrorMessage="נא הכנס שם למתכון")]
        [Display(Name="שם המתכון")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name="תאור המתכון")]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage="נא הכנס קישור למתכון")]
        [Display(Name="קישור למתכון")]
        public string Url { get; set; }

        [Display(Name="תמונה")]
        public string PictureName { get; set; }
        //navigation property each recipe has one category
         [Display(Name = "קטגוריה ראשית")]
        public int MainCategoryID { get; set; }
        public virtual MainCategory MainCategory { get; set; }

        public virtual ICollection<NavigationWord> NavigationWords { get; set; }


        [Display(Name="?מתכון מוצלח")]
        public bool Excellent { get; set; }     //represent if the recipe is "excellent"
    }
}