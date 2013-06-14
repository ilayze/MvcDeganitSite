using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDeganitSite.ViewModels
{
    //used in RecipeController/NavigationWords/PopulateAssigendRecipeNavigationWords
    public class assignedRecipeNavigationWords
    {
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}