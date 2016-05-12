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
    public class ComponentController : Controller
    {
        private Dal db = new Dal();

        public SelectList CategorieList()
        {
            return new SelectList(db.Categorieën.ToList(), "Id", "Naam");
        }

        public List<Component> vulComponenten()
        {
            List<Component> dalComponenten = db.Componenten.ToList();
            List<Component> componenten = new List<Component>();
            foreach (Component dl in dalComponenten)
            {
                Categorie c = db.Categorieën.Find(dl.IdCategorie);
                Component component = new Component();
                component.Aankoopprijs = dl.Aankoopprijs;
                component.Aantal = dl.Aantal;
                component.Categorie = c;
                component.Datasheet = dl.Datasheet;
                component.Id = dl.Id;
                component.Naam = dl.Naam;
                componenten.Add(component);
            }
            return componenten;
        }

        // GET: Component
        public ActionResult Index(string zoeken,string sorteer)
        {
            List<Component> componenten = vulComponenten();
            ViewBag.NaamSorteer = String.IsNullOrEmpty(sorteer) ? "name_desc" : "";
            if (!String.IsNullOrEmpty(zoeken))
            {
                componenten = componenten.Where(c => c.Naam == zoeken).ToList();
            }
            if(sorteer == "name_desc")
            {
                componenten = componenten.OrderByDescending(c => c.Naam).ToList();
            }
            if(sorteer == null)
            {
                componenten = componenten.OrderBy(c => c.Naam).ToList();
            }
            return View(componenten);
        }

        // GET: Component/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Componenten.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            Categorie cat = db.Categorieën.Find(component.IdCategorie);
            Component c = new Component();
            c.Aankoopprijs = component.Aankoopprijs;
            c.Aantal = component.Aantal;
            c.Categorie = cat;
            c.Datasheet = component.Datasheet;
            c.Id = component.Id;
            c.Naam = component.Naam;
            return View(c);
        }

        // GET: Component/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Categorieën = CategorieList();
            return View();
        }

        // POST: Component/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int idCategorie = Int32.Parse(collection["Categorieën"].Split(',')[0]);
                string datasheet = collection["Datasheet"].Split(',')[0];
                string naam = collection["Naam"].Split(',')[0];
                int aantal = Int32.Parse(collection["Aantal"].Split(',')[0]);
                int aankoopprijs = Int32.Parse(collection["Aankoopprijs"].Split(',')[0]);
                Component component = new Component();
                component.Aankoopprijs = aankoopprijs;
                component.Aantal = aantal;
                component.Datasheet = datasheet;
                component.IdCategorie = idCategorie;
                component.Naam = naam;
                db.Componenten.Add(component);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Component/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Componenten.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            Categorie cat = db.Categorieën.Find(component.IdCategorie);
            Component c = new Component();
            c.Aankoopprijs = component.Aankoopprijs;
            c.Aantal = component.Aantal;
            c.Categorie = cat;
            c.Datasheet = component.Datasheet;
            c.Id = component.Id;
            c.Naam = component.Naam;
            SelectList list = CategorieList();
            foreach(SelectListItem s in list)
            {
                if(Int32.Parse(s.Value) == cat.Id)
                {
                    s.Selected = true;
                }
            }
            ViewBag.Categorie = cat.Naam;
            ViewBag.Categorieën = CategorieList();
            return View(c);
        }

        // POST: Component/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int idCategorie = Int32.Parse(collection["Categorieën"].Split(',')[0]);
                string datasheet = collection["Datasheet"].Split(',')[0];
                string naam = collection["Naam"].Split(',')[0];
                int aantal = Int32.Parse(collection["Aantal"].Split(',')[0]);
                int aankoopprijs = Int32.Parse(collection["Aankoopprijs"].Split(',')[0]);
                Component component = new Component();
                component.Aankoopprijs = aankoopprijs;
                component.Aantal = aantal;
                component.Datasheet = datasheet;
                component.IdCategorie = idCategorie;
                component.Naam = naam;
                db.Entry(component).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Component/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Componenten.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // POST: Component/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Component component = db.Componenten.Find(id);
            db.Componenten.Remove(component);
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
