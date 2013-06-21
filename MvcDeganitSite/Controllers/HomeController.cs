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
        public ActionResult Index(int? categoryId,string navigationName)
        {
           string username= User.Identity.Name;
           if (String.IsNullOrEmpty(username))
           {
               return RedirectToAction("LogOn","Account");
           }

           if (username == "דגנית")
           {
               ViewBag.Administrator = true;
           }
           else
           {
               ViewBag.Administrator = false;
           }

            var viewModel = new CategoryToDisplay();
            viewModel.MainCategories = db.MainCategories;
            viewModel.NavigateList = db.NavigationWords;
           
            ViewBag.Message = "אתר המתכונים של דגנית";
            if (categoryId != null)
            {
                viewModel.mainCategoryId = viewModel.MainCategories.Where(m => m.MainCategoryId == categoryId).Single().MainCategoryId;
                viewModel.Recipes = db.Recipes.Where(r => r.MainCategoryID == viewModel.mainCategoryId);
                //filters by current user
                viewModel.Recipes = from recipe in viewModel.Recipes
                                    from user in recipe.Users
                                    where user.Name == username
                                    select recipe;
                                  
                ViewBag.Name = viewModel.MainCategories.Where(m => m.MainCategoryId == categoryId).Single().Name;
            }
            
            if (!String.IsNullOrEmpty(navigationName))
            {
                var navWord = db.NavigationWords.Where(n => n.Name == navigationName).Single();
                viewModel.chosenNavigationWord = navWord;

                List<Recipe> recipesByNavigationWords = new List<Recipe>();
                foreach (var recipe in db.Recipes)
                {
                    foreach (var recipeNavWord in recipe.NavigationWords)
                    {
                        if (recipeNavWord.Name == navWord.Name)
                        {
                            recipesByNavigationWords.Add(recipe);
                        }
                    }
                }

                recipesByNavigationWords = (from recipe in recipesByNavigationWords
                                           from user in recipe.Users
                                           where user.Name == username
                                           select recipe).ToList();

                viewModel.RecipeByChoosenNavigateWord = recipesByNavigationWords;
                    

               // viewModel.RecipeByChoosenNavigateWord = db.Recipes.Where(n =>).
                ViewBag.Nav = navigationName;
            }
            
            
            return View(viewModel);
        }

        public ActionResult About()
        {
            return View();
        }
        
    }
}
