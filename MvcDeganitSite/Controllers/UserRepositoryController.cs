using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDeganitSite.Models;

namespace MvcDeganitSite.Controllers
{
    public class UserRepositoryController : Controller
    {

        private RecipesContext db = new RecipesContext();

        //
        // GET: /UserRepository/

        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            if (String.IsNullOrEmpty(userName))
            {
                return RedirectToAction("LogOn", "Account");
            }

            ViewBag.CurrentUser = userName;

            var Recipes = from recipe in db.Recipes
                      from user in recipe.Users
                      where user.Name == userName
                      select recipe;

            return View(Recipes);
        }

        
        public ActionResult DeleteFromRepository(int id)
        {
            var userName = User.Identity.Name;
            var userEntity = (from user in db.Users
                              where user.Name == userName
                              select user).Single();
            var recipeToRemove=db.Recipes.Find(id);

            userEntity.Recipes.Remove(recipeToRemove);
            db.SaveChanges();

            return View("Index",userEntity.Recipes);
        }

    }
}
