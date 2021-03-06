﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDeganitSite.Models;
using MvcDeganitSite.ViewModels;

namespace MvcDeganitSite.Controllers
{ 
    public class RecipesController : Controller
    {
        private RecipesContext db = new RecipesContext();

        //
        // GET: /Recipes/
        public ActionResult Index(string searchString,AllRecipesView viewStrategy=AllRecipesView.Advance)
        {
            var userName = User.Identity.Name;
            if(String.IsNullOrEmpty(userName))
            {
                return RedirectToAction("LogOn","Account");
            }

            if (userName == "דגנית")
            {
                ViewBag.Administarator = true;
            }
            else
            {
                ViewBag.Administarator = false;
            }

            ViewBag.CurrentUser = userName;

            var recipes = db.Recipes.Include(r => r.MainCategory);
            if(!String.IsNullOrEmpty(searchString))
            {
                recipes= recipes.Where(r => r.Name.Contains(searchString) || r.Description.Contains(searchString) );
            }
            
            ViewBag.viewStrategy = viewStrategy;
            if(String.IsNullOrEmpty(searchString))
            {
                ViewBag.searchString="";
            }
            else
            {
                ViewBag.searchString=searchString;
            }
                        
            return View(recipes.ToList());
        }

        //
        // GET: /Recipes/Details/5

        public ViewResult Details(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            return View(recipe);
        }

        public ActionResult ChooseMainCategory()
        {
            ViewBag.MainCategoryID = new SelectList(db.MainCategories, "MainCategoryID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult ChooseMainCategory(Recipe recipe)
        {
           Int32 Mid= recipe.MainCategoryID;
           return RedirectToAction("Create", "Recipes", new { Meid = Mid });
        }

        //
        // GET: /Recipes/Create

        public ActionResult Create(int Meid=1)
        {

            ViewBag.MainCategoryID = new SelectList(db.MainCategories, "MainCategoryId", "Name",Meid );


            return View();
        } 

        //
        // POST: /Recipes/Create

        [HttpPost]
        public ActionResult Create(Recipe recipe,HttpPostedFileBase userFile)
        {
            var files = Request.Files;
            var userName = User.Identity.Name;
            
            if (userFile!=null)
            {
                var fileName = Path.GetFileName(userFile.FileName);
                var path = Path.Combine("~/Content/recipesPictures", fileName);
                userFile.SaveAs(Server.MapPath(path));
                recipe.PictureName = fileName;
            }
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                if (!String.IsNullOrEmpty(userName))
                {
                    db.Users.Where(usr => usr.Name == userName).Single().Recipes.Add(recipe);
                }
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MainCategoryID = new SelectList(db.MainCategories, "MainCategoryId", "Name", recipe.MainCategoryID);
            return View(recipe);
        }
        
        //
        // GET: /Recipes/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            ViewBag.MainCategoryID = new SelectList(db.MainCategories, "MainCategoryId", "Name", recipe.MainCategoryID);
            return View(recipe);
        }

        //
        // POST: /Recipes/Edit/5

        [HttpPost]
        public ActionResult Edit(Recipe recipe,HttpPostedFileBase userFile)
        {
            if (userFile != null)
            {
                var fileName = Path.GetFileName(userFile.FileName);
                var path = Path.Combine("~/Content/recipesPictures", fileName);
                userFile.SaveAs(Server.MapPath(path));
                recipe.PictureName = fileName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MainCategoryID = new SelectList(db.MainCategories, "MainCategoryId", "Name", recipe.MainCategoryID);
            return View(recipe);
        }

        //
        // GET: /Recipes/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            return View(recipe);
        }

        //
        // POST: /Recipes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult NavigationWords(int id)
        {
            Recipe recipe = db.Recipes
                .Include(n => n.NavigationWords)
                .Where( r => r.RecipeID==id)
                .Single();
            PopulateAssigendRecipeNavigationWords(recipe);

            return View();
        }

        [HttpPost]
        public ActionResult NavigationWords(int id, FormCollection formCollection, string[] selectedNavigationWords)
        {
            var recipeToUptade = db.Recipes
                .Include(n => n.NavigationWords)
                .Where(r => r.RecipeID == id)
                .Single();
            if (TryUpdateModel(recipeToUptade, "", null, new string[] { "NavigationWords" }))
            {
                try
                {
                    UptadeNavigationWords(selectedNavigationWords, recipeToUptade);
                    db.Entry(recipeToUptade).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "אין אפשרות לשמור את השינויים נסה שוב ואם חוזר על עצמו פנה למנהל האתר");
                    return View();
                }
            }
            PopulateAssigendRecipeNavigationWords(recipeToUptade);
            return View();

        }

        public ActionResult SuccessfulRecipes()
        {
            var userName = User.Identity.Name;
            if (String.IsNullOrEmpty(userName))
            {
                return RedirectToAction("LogOn", "Account");
            }

            if (userName == "דגנית")
            {
                ViewBag.Administarator = true;
            }
            else
            {
                ViewBag.Administarator = false;
            }

            ViewBag.CurrentUser = userName;

            var successfulRecipes = db.Recipes.Where(r => r.Excellent == true);
            return View(successfulRecipes);

        }

        public ActionResult AddRecipe(int id)
        {
            string userName = User.Identity.Name;
           
            var recipe = db.Recipes.Where(r => r.RecipeID == id).Single();
            bool exist = (from user in recipe.Users
                          where user.Name == userName
                          select user).Any();
            if (!exist)
            {
                var userInDb = (from user in db.Users
                                where user.Name == userName
                                select user).Single();
                userInDb.Recipes.Add(recipe);
                db.SaveChanges();

                ViewBag.AddRecipeMessage = "המתכון " + recipe.Name +" "+ "הוסף בהצלחה!";
            }
            else
            {
                ViewBag.AddRecipeMessage = "שגיאה בהוספת המתכון: המתכון כבר קיים במאגר שלך!";
            }

            return View();
        }

        private void UptadeNavigationWords(string[] selectedNavigationWords, Recipe recipeToUptade)
        {
            if (selectedNavigationWords == null)
            {
                recipeToUptade.NavigationWords = new List<NavigationWord>();
                return;
            }

            var selectNavHashSet = new HashSet<string>(selectedNavigationWords);  //all the checkbox that selected
            var RecipeNavigationWords= new HashSet<string>(recipeToUptade.NavigationWords.Select(n => n.Name)); //all the recipe Navigation words
            foreach (var nav in db.NavigationWords)  //go throw all the navigation words in the database 
            {
                if (selectNavHashSet.Contains(nav.Name))
                {
                    //selected in the checkbox and the recipe doesn't have it already
                    if (!RecipeNavigationWords.Contains(nav.Name))
                    {
                        recipeToUptade.NavigationWords.Add(nav);
                    }

                }
                else
                {
                    if(RecipeNavigationWords.Contains(nav.Name))
                    {
                        recipeToUptade.NavigationWords.Remove(nav);
                    }
                }
            }
        }

        private void PopulateAssigendRecipeNavigationWords(Recipe recipe)
        {
            var allNavigationWords = db.NavigationWords;
            var recipeNavigationWords = new HashSet<string>(recipe.NavigationWords.Select(n => n.Name));
            var viewModel = new List<assignedRecipeNavigationWords>();
            foreach (var navigationword in allNavigationWords)
            {
                viewModel.Add(new assignedRecipeNavigationWords
                {
                    Name = navigationword.Name,
                    Assigned = recipeNavigationWords.Contains(navigationword.Name)

                });
            }
            ViewBag.NavigationWords = viewModel;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

    public enum AllRecipesView
    {
        Basic,
        Advance
    }
}