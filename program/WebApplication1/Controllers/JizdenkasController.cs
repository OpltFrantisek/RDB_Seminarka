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
    public class JizdenkasController : Controller
    {
        private RDB_SeminarkaEntities db = new RDB_SeminarkaEntities();

        // GET: Jizdenkas
        public ActionResult Index()
        {
            var jizdenka = db.Jizdenka.Include(j => j.Jizda).Include(j => j.Klient);
            return View(jizdenka.ToList());
        }

        // GET: Jizdenkas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jizdenka jizdenka = db.Jizdenka.Find(id);
            if (jizdenka == null)
            {
                return HttpNotFound();
            }
            return View(jizdenka);
        }

        // GET: Jizdenkas/Create
        public ActionResult Create()
        {
            ViewBag.linka = new SelectList(db.Jizda, "linka", "spz");
            ViewBag.email = new SelectList(db.Klient, "email", "jmeno");
            return View();
        }

        // POST: Jizdenkas/Create
        // Chcete-li zajistit ochranu před útoky typu OVERPOST, povolte konkrétní vlastnosti, k nimž chcete vytvořit vazbu. 
        // Další informace viz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "linka,email,cas,cislo")] Jizdenka jizdenka)
        {
            if (ModelState.IsValid)
            {
                db.Jizdenka.Add(jizdenka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.linka = new SelectList(db.Jizda, "linka", "spz", jizdenka.linka);
            ViewBag.email = new SelectList(db.Klient, "email", "jmeno", jizdenka.email);
            return View(jizdenka);
        }

        // GET: Jizdenkas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jizdenka jizdenka = db.Jizdenka.Find(id);
            if (jizdenka == null)
            {
                return HttpNotFound();
            }
            ViewBag.linka = new SelectList(db.Jizda, "linka", "spz", jizdenka.linka);
            ViewBag.email = new SelectList(db.Klient, "email", "jmeno", jizdenka.email);
            return View(jizdenka);
        }

        // POST: Jizdenkas/Edit/5
        // Chcete-li zajistit ochranu před útoky typu OVERPOST, povolte konkrétní vlastnosti, k nimž chcete vytvořit vazbu. 
        // Další informace viz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "linka,email,cas,cislo")] Jizdenka jizdenka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jizdenka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.linka = new SelectList(db.Jizda, "linka", "spz", jizdenka.linka);
            ViewBag.email = new SelectList(db.Klient, "email", "jmeno", jizdenka.email);
            return View(jizdenka);
        }

        // GET: Jizdenkas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jizdenka jizdenka = db.Jizdenka.Find(id);
            if (jizdenka == null)
            {
                return HttpNotFound();
            }
            return View(jizdenka);
        }

        // POST: Jizdenkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Jizdenka jizdenka = db.Jizdenka.Find(id);
            db.Jizdenka.Remove(jizdenka);
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
