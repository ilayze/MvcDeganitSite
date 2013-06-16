using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDeganitSite.Models;
using MvcDeganitSite.ViewModels;

namespace MvcDeganitSite.Controllers
{ 
    public class MainCategoryController : Controller
    {
         private RecipesContext db = new RecipesContext();

        //
        // GET: /MainCategory/

        public ViewResult Index()
        {
            return View(db.MainCategories.ToList());
        }

        //
        // GET: /MainCategory/Details/5

        public ViewResult Details(int id)
        {
            MainCategory maincategory = db.MainCategories.Find(id);
            return View(maincategory);
        }

        //
        // GET: /MainCategory/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ErrorMessag = "";
            return View();
        }

      

        //
        // POST: /MainCategory/Create

        [HttpPost]
        public ActionResult Create(MainCategory maincategory)
        {
            if (ModelState.IsValid)
            {
                var exist = db.MainCategories.Where(m => m.Name == maincategory.Name);
                if (!exist.Any() )
                {
                    db.MainCategories.Add(maincategory);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    string error = "הקטגוריה הראשית קיימת כבר נא שני שם קטגוריה";
                    ViewBag.ErrorMessage = error;
                }
            }

            return View(maincategory);
        }
        
        //
        // GET: /MainCategory/Edit/5
 
        public ActionResult Edit(int id)
        {
            MainCategory maincategory = db.MainCategories.Find(id);
            return View(maincategory);
        }

        //
        // POST: /MainCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(MainCategory maincategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maincategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maincategory);
        }

        //
        // GET: /MainCategory/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            var ToDelete = new DeleteMainCategory();
            MainCategory maincategory = db.MainCategories.Find(id);
            var recipes = db.Recipes.Where(m=>m.MainCategoryID==maincategory.MainCategoryId);
           
            ToDelete.mainCategory = maincategory;
            ToDelete.recipes = recipes;
            return View(ToDelete);
        }

        //
        // POST: /MainCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            MainCategory maincategory = db.MainCategories.Find(id);
            db.MainCategories.Remove(maincategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}