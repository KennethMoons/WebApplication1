using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategorieController : Controller
    {
        private Dal db = new Dal();

        // GET: Categorie
        public ActionResult Index()
        {
            return View(db.Categorieën.ToList());
        }

        // GET: Categorie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categorieën.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // GET: Categorie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naam")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Categorieën.Add(categorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorie);
        }

        // GET: Categorie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categorieën.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // POST: Categorie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorie);
        }

        // GET: Categorie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categorieën.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // POST: Categorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorie categorie = db.Categorieën.Find(id);
            db.Categorieën.Remove(categorie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
