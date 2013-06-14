using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDeganitSite.Models;
using MvcDeganitSite.ViewModels;



namespace MvcDeganitSite.Controllers
{
    public class HomeController : Controller
    {
        private RecipesContext db = new RecipesContext();
        public ActionResult Index(int? id,string name)
        {
            var viewModel = new CategoryToDisplay();
            viewModel.MainCategories = db.MainCategories;
          //  ViewBag.NavigationWords = new SelectList(db.NavigationWords,"Name","Name");
            viewModel.NavigateList = db.NavigationWords;
           
            ViewBag.Message = "אתר המתכונים של דגנית";
            if (id != null)
            {
                viewModel.RecID = viewModel.MainCategories.Where(m => m.MainCategoryId == id).Single().MainCategoryId;
                viewModel.Recipes = db.Recipes.Where(r => r.MainCategoryID == viewModel.RecID);
                ViewBag.Name = viewModel.MainCategories.Where(m => m.MainCategoryId == id).Single().Name;
            }
            /*
            if (!String.IsNullOrEmpty(name))
            {
                var navWord = db.NavigationWords.Where(n => n.Name == name).Single();
                IEnumerable<HashSet<string>> recipeHS;
                foreach(var recipe in db.Recipes)
                   foreach(var navv in recipe.NavigationWords)
                   {
                        
                   }
                    

                viewModel.RecipeByChoosenNavigateWord = db.Recipes.Where(n =>).
                ViewBag.Nav = name;
            }
             * */
            
            return View(viewModel);
        }

        public ActionResult About()
        {
            return View();
        }
        
    }
}
