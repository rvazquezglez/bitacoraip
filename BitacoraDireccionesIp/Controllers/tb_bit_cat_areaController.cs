using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitacoraDireccionesIp.Models;

namespace BitacoraDireccionesIp.Controllers
{
    public class tb_bit_cat_areaController : Controller
    {
        private bdd_bitacora_ipEntities db = new bdd_bitacora_ipEntities();

        //
        // GET: /tb_bit_cat_area/

        public ActionResult Index()
        {
            return View(db.tb_bit_cat_area.ToList());
        }

        //
        // GET: /tb_bit_cat_area/Details/5

        public ActionResult Details(int id = 0)
        {
            tb_bit_cat_area tb_bit_cat_area = db.tb_bit_cat_area.Find(id);
            if (tb_bit_cat_area == null)
            {
                return HttpNotFound();
            }
            return View(tb_bit_cat_area);
        }

        //
        // GET: /tb_bit_cat_area/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /tb_bit_cat_area/Create

        [HttpPost]
        public ActionResult Create(tb_bit_cat_area tb_bit_cat_area)
        {
            if (ModelState.IsValid)
            {
                db.tb_bit_cat_area.Add(tb_bit_cat_area);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_bit_cat_area);
        }

        //
        // GET: /tb_bit_cat_area/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tb_bit_cat_area tb_bit_cat_area = db.tb_bit_cat_area.Find(id);
            if (tb_bit_cat_area == null)
            {
                return HttpNotFound();
            }
            return View(tb_bit_cat_area);
        }

        //
        // POST: /tb_bit_cat_area/Edit/5

        [HttpPost]
        public ActionResult Edit(tb_bit_cat_area tb_bit_cat_area)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_bit_cat_area).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_bit_cat_area);
        }

        //
        // GET: /tb_bit_cat_area/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tb_bit_cat_area tb_bit_cat_area = db.tb_bit_cat_area.Find(id);
            if (tb_bit_cat_area == null)
            {
                return HttpNotFound();
            }
            return View(tb_bit_cat_area);
        }

        //
        // POST: /tb_bit_cat_area/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_bit_cat_area tb_bit_cat_area = db.tb_bit_cat_area.Find(id);
            db.tb_bit_cat_area.Remove(tb_bit_cat_area);
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