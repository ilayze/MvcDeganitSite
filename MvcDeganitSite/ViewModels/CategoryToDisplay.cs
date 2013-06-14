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
        /// <summary>
        /// all main categories from database
        /// </summary>
        public IEnumerable<MainCategory> MainCategories { get; set; }   
        public IEnumerable<Recipe> Recipes { get; set; }    
        public int mainCategoryId { get; set; }

        /// <summary>
        /// all navigation words
        /// </summary>
        public IEnumerable<NavigationWord> NavigateList { get; set; }

        /// <summary>
        /// contains the recipes when clicked on navigation word in the main page
        /// </summary>
        public IEnumerable<Recipe> RecipeByChoosenNavigateWord { get; set; }

        public NavigationWord chosenNavigationWord { get; set; }
            
    }
}