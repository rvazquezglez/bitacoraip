using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

using System.Web.Mvc;
using BitacoraDireccionesIp.Models;
using System.Transactions;

namespace BitacoraDireccionesIp.Controllers
{
    public class tb_bit_usuarioController : Controller
    {
        private bdd_bitacora_ipEntities db = new bdd_bitacora_ipEntities();

        //
        // GET: /tb_bit_usuario/

        public ActionResult Index()
        {
            var tb_bit_usuario = db.tb_bit_usuario
                .Include(t => t.tb_bit_cat_area)
                .Include(t => t.tb_bit_cat_dga)
                .Include(t => t.tb_bit_cat_empresa)
                .Include(t => t.tb_bit_cat_piso)
                .Include(t => t.tb_bit_cat_regional)
                .Where(x=> x.fec_baja == null);
            return View(tb_bit_usuario.ToList());
        }

        //
        // GET: /tb_bit_usuario/Details/5

        public ActionResult Details(int id = 0)
        {
            tb_bit_usuario tb_bit_usuario = db.tb_bit_usuario.Find(id);
            if (tb_bit_usuario == null)
            {
                return HttpNotFound();
            }
            return View(tb_bit_usuario);
        }

        //
        // GET: /tb_bit_usuario/Create

        public ActionResult Create()
        {
            ViewBag.fk_cve_area = new SelectList(db.tb_bit_cat_area, "pk_cve_area", "des_area");
            ViewBag.fk_cve_dga = new SelectList(db.tb_bit_cat_dga, "pk_cve_dga", "des_dga");
            ViewBag.fk_cve_empresa = new SelectList(db.tb_bit_cat_empresa, "pk_cve_empresa", "des_empresa");
            ViewBag.fk_cve_piso = new SelectList(db.tb_bit_cat_piso, "pk_cve_piso", "des_piso");
            ViewBag.fk_cve_regional = new SelectList(db.tb_bit_cat_regional, "pk_cve_regional", "des_regional");
            return View();
        }

        //
        // POST: /tb_bit_usuario/Create

        [HttpPost]
        public ActionResult Create(tb_bit_usuario tb_bit_usuario)
        {
            if (ModelState.IsValid)
            {
                tb_bit_usuario.fec_alta = DateTime.Now;
                db.tb_bit_usuario.Add(tb_bit_usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_cve_area = new SelectList(db.tb_bit_cat_area, "pk_cve_area", "des_area", tb_bit_usuario.fk_cve_area);
            ViewBag.fk_cve_dga = new SelectList(db.tb_bit_cat_dga, "pk_cve_dga", "des_dga", tb_bit_usuario.fk_cve_dga);
            ViewBag.fk_cve_empresa = new SelectList(db.tb_bit_cat_empresa, "pk_cve_empresa", "des_empresa", tb_bit_usuario.fk_cve_empresa);
            ViewBag.fk_cve_piso = new SelectList(db.tb_bit_cat_piso, "pk_cve_piso", "des_piso", tb_bit_usuario.fk_cve_piso);
            ViewBag.fk_cve_regional = new SelectList(db.tb_bit_cat_regional, "pk_cve_regional", "des_regional", tb_bit_usuario.fk_cve_regional);
            return View(tb_bit_usuario);
        }

        //
        // GET: /tb_bit_usuario/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tb_bit_usuario tb_bit_usuario = db.tb_bit_usuario.Find(id);            
            this.ControllerContext.HttpContext.Session["usuario_hist"] = tb_bit_usuario;
            if (tb_bit_usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_cve_area = new SelectList(db.tb_bit_cat_area, "pk_cve_area", "des_area", tb_bit_usuario.fk_cve_area);
            ViewBag.fk_cve_dga = new SelectList(db.tb_bit_cat_dga, "pk_cve_dga", "des_dga", tb_bit_usuario.fk_cve_dga);
            ViewBag.fk_cve_empresa = new SelectList(db.tb_bit_cat_empresa, "pk_cve_empresa", "des_empresa", tb_bit_usuario.fk_cve_empresa);
            ViewBag.fk_cve_piso = new SelectList(db.tb_bit_cat_piso, "pk_cve_piso", "des_piso", tb_bit_usuario.fk_cve_piso);
            ViewBag.fk_cve_regional = new SelectList(db.tb_bit_cat_regional, "pk_cve_regional", "des_regional", tb_bit_usuario.fk_cve_regional);
            return View(tb_bit_usuario);
        }

        //
        // POST: /tb_bit_usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(tb_bit_usuario tb_bit_usuario)
        {
            if (ModelState.IsValid)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var historico = CreateHistorico((tb_bit_usuario)this.ControllerContext.HttpContext.Session["usuario_hist"]);
                    db.tb_bit_usuario_historico.Add(historico);                        
                    db.Entry(tb_bit_usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    ts.Complete();
                }
                return RedirectToAction("Index");
            }
            ViewBag.fk_cve_area = new SelectList(db.tb_bit_cat_area, "pk_cve_area", "des_area", tb_bit_usuario.fk_cve_area);
            ViewBag.fk_cve_dga = new SelectList(db.tb_bit_cat_dga, "pk_cve_dga", "des_dga", tb_bit_usuario.fk_cve_dga);
            ViewBag.fk_cve_empresa = new SelectList(db.tb_bit_cat_empresa, "pk_cve_empresa", "des_empresa", tb_bit_usuario.fk_cve_empresa);
            ViewBag.fk_cve_piso = new SelectList(db.tb_bit_cat_piso, "pk_cve_piso", "des_piso", tb_bit_usuario.fk_cve_piso);
            ViewBag.fk_cve_regional = new SelectList(db.tb_bit_cat_regional, "pk_cve_regional", "des_regional", tb_bit_usuario.fk_cve_regional);
            return View(tb_bit_usuario);
        }

        private tb_bit_usuario_historico CreateHistorico(tb_bit_usuario tb_bit_usuario)
        {
            return new tb_bit_usuario_historico
            {
                ape_materno = tb_bit_usuario.ape_materno,
                ape_paterno = tb_bit_usuario.ape_paterno,
                cve_usuario = tb_bit_usuario.pk_cve_usuario,
                des_observacion = tb_bit_usuario.des_observacion,
                fk_cve_area = tb_bit_usuario.fk_cve_area,
                fk_cve_dga = tb_bit_usuario.fk_cve_dga,
                fk_cve_empresa = tb_bit_usuario.fk_cve_empresa,
                fk_cve_piso = tb_bit_usuario.fk_cve_piso,
                fk_cve_regional = tb_bit_usuario.fk_cve_regional,
                nom_user_name = tb_bit_usuario.nom_user_name,
                nom_usuario = tb_bit_usuario.nom_usuario,
                fecha_cambio = DateTime.Now

            };

        }

        //
        // GET: /tb_bit_usuario/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tb_bit_usuario tb_bit_usuario = db.tb_bit_usuario.Find(id);
            if (tb_bit_usuario == null)
            {
                return HttpNotFound();
            }
            return View(tb_bit_usuario);
        }

        //
        // POST: /tb_bit_usuario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_bit_usuario tb_bit_usuario = db.tb_bit_usuario.Find(id);
            using (TransactionScope ts = new TransactionScope())
            {
                db.tb_bit_usuario_historico.Add(CreateHistorico(tb_bit_usuario));
                                
                /*tb_bit_usuario.tb_bit_ip.ToList().ForEach(r => db.tb_bit_ip.Remove(r));*/

                tb_bit_usuario.fec_baja = DateTime.Now;

                db.SaveChanges();


                ts.Complete();
            }
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