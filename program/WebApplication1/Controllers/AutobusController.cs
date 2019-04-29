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
    public class AutobusController : Controller
    {
        private RDB_SeminarkaEntities db = new RDB_SeminarkaEntities();

        // GET: Autobus
        public ActionResult Index()
        {
            var autobus = db.Autobus.Include(a => a.Znacka1);
            return View(autobus.ToList());
        }

        // GET: Autobus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autobus autobus = db.Autobus.Find(id);
            if (autobus == null)
            {
                return HttpNotFound();
            }
            return View(autobus);
        }

        // GET: Autobus/Create
        public ActionResult Create()
        {
            ViewBag.znacka = new SelectList(db.Znacka, "znacka1", "znacka1");
            return View();
        }

        // POST: Autobus/Create
        // Chcete-li zajistit ochranu před útoky typu OVERPOST, povolte konkrétní vlastnosti, k nimž chcete vytvořit vazbu. 
        // Další informace viz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "spz,znacka")] Autobus autobus)
        {
            if (ModelState.IsValid)
            {
                db.Autobus.Add(autobus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.znacka = new SelectList(db.Znacka, "znacka1", "znacka1", autobus.znacka);
            return View(autobus);
        }

        // GET: Autobus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autobus autobus = db.Autobus.Find(id);
            if (autobus == null)
            {
                return HttpNotFound();
            }
            ViewBag.znacka = new SelectList(db.Znacka, "znacka1", "znacka1", autobus.znacka);
            return View(autobus);
        }

        // POST: Autobus/Edit/5
        // Chcete-li zajistit ochranu před útoky typu OVERPOST, povolte konkrétní vlastnosti, k nimž chcete vytvořit vazbu. 
        // Další informace viz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "spz,znacka")] Autobus autobus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autobus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.znacka = new SelectList(db.Znacka, "znacka1", "znacka1", autobus.znacka);
            return View(autobus);
        }

        // GET: Autobus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autobus autobus = db.Autobus.Find(id);
            if (autobus == null)
            {
                return HttpNotFound();
            }
            return View(autobus);
        }

        // POST: Autobus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Autobus autobus = db.Autobus.Find(id);
            db.Autobus.Remove(autobus);
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
