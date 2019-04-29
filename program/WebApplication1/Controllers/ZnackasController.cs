using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ZnackasController : Controller
    {
        private RDB_SeminarkaEntities db = new RDB_SeminarkaEntities();

        // GET: Znackas
        public ActionResult Index()
        {
            return View(db.Znacka.ToList());
        }

        // GET: Znackas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Znacka znacka = db.Znacka.Find(id);
            if (znacka == null)
            {
                return HttpNotFound();
            }
            return View(znacka);
        }

        // GET: Znackas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Znackas/Create
        // Chcete-li zajistit ochranu před útoky typu OVERPOST, povolte konkrétní vlastnosti, k nimž chcete vytvořit vazbu. 
        // Další informace viz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "znacka1")] Znacka znacka)
        {
            if (ModelState.IsValid)
            {
                db.Znacka.Add(znacka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(znacka);
        }

        // GET: Znackas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Znacka znacka = db.Znacka.Find(id);
            if (znacka == null)
            {
                return HttpNotFound();
            }
            return View(znacka);
        }

        // POST: Znackas/Edit/5
        // Chcete-li zajistit ochranu před útoky typu OVERPOST, povolte konkrétní vlastnosti, k nimž chcete vytvořit vazbu. 
        // Další informace viz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "znacka1")] Znacka znacka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(znacka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(znacka);
        }

        // GET: Znackas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Znacka znacka = db.Znacka.Find(id);
            if (znacka == null)
            {
                return HttpNotFound();
            }
            return View(znacka);
        }

        // POST: Znackas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Znacka znacka = db.Znacka.Find(id);
            db.Znacka.Remove(znacka);
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
