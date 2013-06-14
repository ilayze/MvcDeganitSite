using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcDeganitSite.Models;
using System.Web.Mvc;

namespace MvcDeganitSite.ViewModels
{
    //used in the HomeController/Index
    public class CategoryToDisplay
    {
        public IEnumerable<MainCategory> MainCategories { get; set; }
        public IEnumerable<Recipe> Recipes { get; set; }
        public int RecID { get; set; }
        public IEnumerable<NavigationWord> NavigateList { get; set; }
        public IEnumerable<Recipe> RecipeByChoosenNavigateWord { get; set; }
            
    }
}