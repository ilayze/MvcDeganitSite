using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDeganitSite.Models;

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

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /MainCategory/Create

        [HttpPost]
        public ActionResult Create(MainCategory maincategory)
        {
            if (ModelState.IsValid)
            {
                db.MainCategories.Add(maincategory);
                db.SaveChanges();
                return RedirectToAction("Index");  
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
 
        public ActionResult Delete(int id)
        {
            MainCategory maincategory = db.MainCategories.Find(id);
            return View(maincategory);
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