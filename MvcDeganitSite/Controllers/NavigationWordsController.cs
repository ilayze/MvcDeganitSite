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
    public class NavigationWordsController : Controller
    {
        private RecipesContext db = new RecipesContext();

        //
        // GET: /NavigationWords/

        public ViewResult Index()
        {
            return View(db.NavigationWords.ToList());
        }

        //
        // GET: /NavigationWords/Details/5

        public ViewResult Details(string id)
        {
            NavigationWord navigationword = db.NavigationWords.Find(id);
            return View(navigationword);
        }

        //
        // GET: /NavigationWords/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = "";
            return View();
        } 

        //
        // POST: /NavigationWords/Create

        [HttpPost]
        public ActionResult Create(NavigationWord navigationword)
        {
            if (ModelState.IsValid)
            {
                var exist = db.NavigationWords.Where(m => m.Name == navigationword.Name);
                if (!exist.Any() )
                {
                    db.NavigationWords.Add(navigationword);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    string error = "מילת ניווט קיימת כבר נא שני שם של מילת ניווט";
                    ViewBag.ErrorMessage = error;
                }
            }

            return View(navigationword);
        }
        
        //
        // GET: /NavigationWords/Edit/5
 
        public ActionResult Edit(string id)
        {
            NavigationWord navigationword = db.NavigationWords.Find(id);
            return View(navigationword);
        }

        //
        // POST: /NavigationWords/Edit/5

        [HttpPost]
        public ActionResult Edit(NavigationWord navigationword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(navigationword).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(navigationword);
        }

        //
        // GET: /NavigationWords/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            NavigationWord navigationword = db.NavigationWords.Find(id);
            return View(navigationword);
        }

        //
        // POST: /NavigationWords/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            NavigationWord navigationword = db.NavigationWords.Find(id);
            db.NavigationWords.Remove(navigationword);
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