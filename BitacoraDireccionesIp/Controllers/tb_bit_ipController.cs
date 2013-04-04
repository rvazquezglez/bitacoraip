using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitacoraDireccionesIp.Models;
using System.Transactions;

namespace BitacoraDireccionesIp.Controllers
{
    public class tb_bit_ipController : Controller
    {
        private bdd_bitacora_ipEntities db = new bdd_bitacora_ipEntities();

        //
        // GET: /tb_bit_ip/

        public ActionResult Index()
        {
            var tb_bit_ip = db.tb_bit_ip.Include(t => t.tb_bit_cat_equipo_computo_marca).Include(t => t.tb_bit_cat_equipo_telefono_modelo).Include(t => t.tb_bit_cat_tipo_equipo).Include(t => t.tb_bit_cat_wireless).Include(t => t.tb_bt_cat_equipo_computo_modelo).Include(t => t.tb_bit_cat_switch).Include(t => t.tb_bit_usuario);
            return View(tb_bit_ip.ToList());        
        }

        public ActionResult ListByUser(int cveusuario)
        {
            var tb_bit_ip = db.tb_bit_ip
                .Include(x => x.tb_bit_cat_equipo_computo_marca)
                .Include(t => t.tb_bit_cat_equipo_telefono_modelo)
                .Include(t => t.tb_bit_cat_tipo_equipo)
                .Include(t => t.tb_bit_cat_wireless)
                .Include(t => t.tb_bt_cat_equipo_computo_modelo)
                .Include(t => t.tb_bit_cat_switch)
                .Include(t => t.tb_bit_usuario)
                .Where(m => m.pfk_cve_usuario == cveusuario);

            ViewBag.cveusuario = cveusuario;
            
            return View("Index", tb_bit_ip.ToList());
            
        }

        //
        // GET: /tb_bit_ip/Details/5

        public ActionResult Details(string id = null, int? conmutador = null, int? usuario = null)
        {
            id = id.Replace("-", ".");
            //tb_bit_ip tb_bit_ip = db.tb_bit_ip.Find(id,conmutador,usuario);
            tb_bit_ip tb_bit_ip = db.tb_bit_ip.Where(x => x.pk_cve_ip == id && x.pfk_cve_switch == conmutador && x.pfk_cve_usuario == usuario).FirstOrDefault();
            if (tb_bit_ip == null)
            {
                return HttpNotFound();
            }
            return View(tb_bit_ip);
        }

        //
        // GET: /tb_bit_ip/Create

        public ActionResult Create()
        {
            ViewBag.fk_cve_equipo_computo_marca = new SelectList(db.tb_bit_cat_equipo_computo_marca, "pk_cve_equipo_computo_marca", "des_equipo_computo_marca");
            ViewBag.fk_cve_equipo_telefono_modelo = new SelectList(db.tb_bit_cat_equipo_telefono_modelo, "pk_cve_equipo_telefono_modelo", "des_equipo_telefono_modelo");
            ViewBag.fk_cve_tipo_equipo = new SelectList(db.tb_bit_cat_tipo_equipo, "pk_cve_tipo_equipo", "des_tipo_equipo");
            ViewBag.fk_cve_mac_wireless = new SelectList(db.tb_bit_cat_wireless, "pk_cve_wir", "des_wir");
            ViewBag.fk_cve_equipo_computo_modelo = new SelectList(db.tb_bt_cat_equipo_computo_modelo, "pk_cve_equipo_computo_modelo", "des_equipo_computo_modelo");
            ViewBag.pfk_cve_switch = new SelectList(db.tb_bit_cat_switch, "pk_cve_switch", "des_cve_switch");
            ViewBag.pfk_cve_usuario = new SelectList(db.tb_bit_usuario.Where(x => x.fec_baja == null), "pk_cve_usuario", "nom_user_name");
            return View();
        }

        public ActionResult CreateByUser(int cveusuario)
        {
            ViewBag.fk_cve_equipo_computo_marca = new SelectList(db.tb_bit_cat_equipo_computo_marca, "pk_cve_equipo_computo_marca", "des_equipo_computo_marca");
            ViewBag.fk_cve_equipo_telefono_modelo = new SelectList(db.tb_bit_cat_equipo_telefono_modelo, "pk_cve_equipo_telefono_modelo", "des_equipo_telefono_modelo");
            ViewBag.fk_cve_tipo_equipo = new SelectList(db.tb_bit_cat_tipo_equipo, "pk_cve_tipo_equipo", "des_tipo_equipo");
            ViewBag.fk_cve_mac_wireless = new SelectList(db.tb_bit_cat_wireless, "pk_cve_wir", "des_wir");
            ViewBag.fk_cve_equipo_computo_modelo = new SelectList(db.tb_bt_cat_equipo_computo_modelo, "pk_cve_equipo_computo_modelo", "des_equipo_computo_modelo");
            ViewBag.pfk_cve_switch = new SelectList(db.tb_bit_cat_switch, "pk_cve_switch", "des_cve_switch");
            ViewBag.pfk_cve_usuario = new SelectList(db.tb_bit_usuario.Where(x => x.fec_baja == null), "pk_cve_usuario", "nom_user_name", cveusuario);

            return View("Create" );
        }
        //
        // POST: /tb_bit_ip/Create

        [HttpPost]
        public ActionResult Create(tb_bit_ip tb_bit_ip)
        {
            if (ModelState.IsValid)
            {
                db.tb_bit_ip.Add(tb_bit_ip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_cve_equipo_computo_marca = new SelectList(db.tb_bit_cat_equipo_computo_marca, "pk_cve_equipo_computo_marca", "des_equipo_computo_marca", tb_bit_ip.fk_cve_equipo_computo_marca);
            ViewBag.fk_cve_equipo_telefono_modelo = new SelectList(db.tb_bit_cat_equipo_telefono_modelo, "pk_cve_equipo_telefono_modelo", "des_equipo_telefono_modelo", tb_bit_ip.fk_cve_equipo_telefono_modelo);
            ViewBag.fk_cve_tipo_equipo = new SelectList(db.tb_bit_cat_tipo_equipo, "pk_cve_tipo_equipo", "des_tipo_equipo", tb_bit_ip.fk_cve_tipo_equipo);
            ViewBag.fk_cve_mac_wireless = new SelectList(db.tb_bit_cat_wireless, "pk_cve_wir", "des_wir", tb_bit_ip.fk_cve_mac_wireless);
            ViewBag.fk_cve_equipo_computo_modelo = new SelectList(db.tb_bt_cat_equipo_computo_modelo, "pk_cve_equipo_computo_modelo", "des_equipo_computo_modelo", tb_bit_ip.fk_cve_equipo_computo_modelo);
            ViewBag.pfk_cve_switch = new SelectList(db.tb_bit_cat_switch, "pk_cve_switch", "des_cve_switch", tb_bit_ip.pfk_cve_switch);
            ViewBag.pfk_cve_usuario = new SelectList(db.tb_bit_usuario, "pk_cve_usuario", "nom_user_name", tb_bit_ip.pfk_cve_usuario);
            return View(tb_bit_ip);
        }

        //
        // GET: /tb_bit_ip/Edit/5

        public ActionResult Edit(string id = null,int? conmutador = null, int? usuario=null)            
        {
            id = id.Replace("-", ".");

            //tb_bit_ip tb_bit_ip = db.tb_bit_ip.Find(id,conmutador,usuario);
            tb_bit_ip tb_bit_ip = db.tb_bit_ip.Where(x => x.pk_cve_ip == id && x.pfk_cve_switch == conmutador && x.pfk_cve_usuario == usuario).FirstOrDefault();

            System.Web.HttpContext.Current.Session["ip_hist"] = tb_bit_ip;
            if (tb_bit_ip == null)
            {
                return HttpNotFound();
            }

            ViewBag.fk_cve_equipo_computo_marca = new SelectList(db.tb_bit_cat_equipo_computo_marca, "pk_cve_equipo_computo_marca", "des_equipo_computo_marca", tb_bit_ip.fk_cve_equipo_computo_marca);
            ViewBag.fk_cve_equipo_telefono_modelo = new SelectList(db.tb_bit_cat_equipo_telefono_modelo, "pk_cve_equipo_telefono_modelo", "des_equipo_telefono_modelo", tb_bit_ip.fk_cve_equipo_telefono_modelo);
            ViewBag.fk_cve_tipo_equipo = new SelectList(db.tb_bit_cat_tipo_equipo, "pk_cve_tipo_equipo", "des_tipo_equipo", tb_bit_ip.fk_cve_tipo_equipo);
            ViewBag.fk_cve_mac_wireless = new SelectList(db.tb_bit_cat_wireless, "pk_cve_wir", "des_wir", tb_bit_ip.fk_cve_mac_wireless);
            ViewBag.fk_cve_equipo_computo_modelo = new SelectList(db.tb_bt_cat_equipo_computo_modelo, "pk_cve_equipo_computo_modelo", "des_equipo_computo_modelo", tb_bit_ip.fk_cve_equipo_computo_modelo);
            ViewBag.pfk_cve_switch = new SelectList(db.tb_bit_cat_switch, "pk_cve_switch", "des_cve_switch", tb_bit_ip.pfk_cve_switch);
            ViewBag.pfk_cve_usuario = new SelectList(db.tb_bit_usuario, "pk_cve_usuario", "nom_user_name", tb_bit_ip.pfk_cve_usuario);

            return View(tb_bit_ip);
        }

        //
        // POST: /tb_bit_ip/Edit/5

        [HttpPost]
        public ActionResult Edit(tb_bit_ip tb_bit_ip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_bit_ip).State = EntityState.Modified;
                using (TransactionScope ts = new TransactionScope())
                {
                    db.tb_bit_ip_historico.Add(CreateHistorico((tb_bit_ip)System.Web.HttpContext.Current.Session["ip_hist"]));
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.fk_cve_equipo_computo_marca = new SelectList(db.tb_bit_cat_equipo_computo_marca, "pk_cve_equipo_computo_marca", "des_equipo_computo_marca", tb_bit_ip.fk_cve_equipo_computo_marca);
            ViewBag.fk_cve_equipo_telefono_modelo = new SelectList(db.tb_bit_cat_equipo_telefono_modelo, "pk_cve_equipo_telefono_modelo", "des_equipo_telefono_modelo", tb_bit_ip.fk_cve_equipo_telefono_modelo);
            ViewBag.fk_cve_tipo_equipo = new SelectList(db.tb_bit_cat_tipo_equipo, "pk_cve_tipo_equipo", "des_tipo_equipo", tb_bit_ip.fk_cve_tipo_equipo);
            ViewBag.fk_cve_mac_wireless = new SelectList(db.tb_bit_cat_wireless, "pk_cve_wir", "des_wir", tb_bit_ip.fk_cve_mac_wireless);
            ViewBag.fk_cve_equipo_computo_modelo = new SelectList(db.tb_bt_cat_equipo_computo_modelo, "pk_cve_equipo_computo_modelo", "des_equipo_computo_modelo", tb_bit_ip.fk_cve_equipo_computo_modelo);
            ViewBag.pfk_cve_switch = new SelectList(db.tb_bit_cat_switch, "pk_cve_switch", "des_cve_switch", tb_bit_ip.pfk_cve_switch);
            ViewBag.pfk_cve_usuario = new SelectList(db.tb_bit_usuario, "pk_cve_usuario", "nom_user_name", tb_bit_ip.pfk_cve_usuario);
            return View(tb_bit_ip);
        }

        //
        // GET: /tb_bit_ip/Delete/5

        public ActionResult Delete(string id = null, int? conmutador = null, int? usuario = null)
        {
            id = id.Replace("-", ".");
            //tb_bit_ip tb_bit_ip = db.tb_bit_ip.Find(id,conmutador,usuario);
            tb_bit_ip tb_bit_ip = db.tb_bit_ip.Where(x => x.pk_cve_ip == id && x.pfk_cve_switch == conmutador && x.pfk_cve_usuario == usuario).FirstOrDefault();
            if (tb_bit_ip == null)
            {
                return HttpNotFound();
            }
            return View(tb_bit_ip);
        }

        //
        // POST: /tb_bit_ip/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id = null, int? conmutador = null, int? usuario = null)
        {
            id = id.Replace("-", ".");
            tb_bit_ip tb_bit_ip = db.tb_bit_ip.Find(id,conmutador,usuario);
            using (TransactionScope ts = new TransactionScope())
            {
                db.tb_bit_ip_historico.Add(CreateHistorico(tb_bit_ip));
                db.tb_bit_ip.Remove(tb_bit_ip);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private static tb_bit_ip_historico CreateHistorico(tb_bit_ip tb_bit_ip)
        {
            return new tb_bit_ip_historico
            {
                cve_equipo_computo_serie = tb_bit_ip.cve_equipo_computo_serie,
                cve_equipo_telefono_extension = tb_bit_ip.cve_equipo_telefono_extension,
                cve_equipo_telefono_serie = tb_bit_ip.cve_equipo_computo_serie,
                cve_ip = tb_bit_ip.pk_cve_ip,
                cve_mac = tb_bit_ip.cve_mac,
                cve_switch = tb_bit_ip.pfk_cve_switch,
                cve_usuario = tb_bit_ip.pfk_cve_usuario,
                fk_cve_equipo_computo_marca = tb_bit_ip.fk_cve_equipo_computo_marca,
                fk_cve_equipo_computo_modelo = tb_bit_ip.fk_cve_equipo_computo_modelo,
                fk_cve_equipo_telefono_modelo = tb_bit_ip.fk_cve_equipo_telefono_modelo,
                fk_cve_mac_wireless = tb_bit_ip.fk_cve_mac_wireless,
                fk_cve_tipo_equipo = tb_bit_ip.fk_cve_tipo_equipo,
                nom_equipo = tb_bit_ip.nom_equipo,
                nom_resguardo_equipo_computo = tb_bit_ip.nom_resguardo_equipo_computo,
                fecha_cambio = DateTime.Now
            };
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}