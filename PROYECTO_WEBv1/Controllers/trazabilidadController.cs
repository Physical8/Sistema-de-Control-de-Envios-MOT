using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROYECTO_WEBv1.Models;

namespace PROYECTO_WEBv1.Controllers
{
    public class trazabilidadController : Controller
    {
        private DB_web_v1LEOEntities db = new DB_web_v1LEOEntities();

        // GET: trazabilidad
        //public ActionResult Index()
        //{
        //    var trazabilidad = db.trazabilidad.Include(t => t.envios);
        //    return View(trazabilidad.ToList());
        //}

        public ActionResult Index(int? id_envio)
        {
            if (id_envio == null)
            {
                // Manejo cuando no se proporciona id_envio
                var trazabilidadtotal = db.trazabilidad.Include(t => t.envios);
                return View(trazabilidadtotal.ToList()); ;
            }

            var trazabilidad = db.trazabilidad
                .Where(t => t.id_envio == id_envio)
                .Include(t => t.envios);

            return View(trazabilidad.ToList());
        }

        // GET: trazabilidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trazabilidad trazabilidad = db.trazabilidad.Find(id);
            if (trazabilidad == null)
            {
                return HttpNotFound();
            }
            return View(trazabilidad);
        }

        // GET: trazabilidad/Create


        public ActionResult Create()
        {
            ViewBag.id_envio = new SelectList(db.envios, "id_envio", "descripcion_paquete");
            return View();
        }

        // POST: trazabilidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_trazabilidad,id_envio,punto_control,fecha_registro")] trazabilidad trazabilidad)
        {
            if (ModelState.IsValid)
            {
                db.trazabilidad.Add(trazabilidad);
                db.SaveChanges();
                return RedirectToAction("Index", "envios");
            }

            ViewBag.id_envio = new SelectList(db.envios, "id_envio", "descripcion_paquete", trazabilidad.id_envio);
            return View(trazabilidad);
        }

        // GET: trazabilidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trazabilidad trazabilidad = db.trazabilidad.Find(id);
            if (trazabilidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_envio = new SelectList(db.envios, "id_envio", "descripcion_paquete", trazabilidad.id_envio);
            return View(trazabilidad);
        }

        // POST: trazabilidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_trazabilidad,id_envio,punto_control,fecha_registro")] trazabilidad trazabilidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trazabilidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_envio = new SelectList(db.envios, "id_envio", "descripcion_paquete", trazabilidad.id_envio);
            return View(trazabilidad);
        }

        // GET: trazabilidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trazabilidad trazabilidad = db.trazabilidad.Find(id);
            if (trazabilidad == null)
            {
                return HttpNotFound();
            }
            return View(trazabilidad);
        }

        // POST: trazabilidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    trazabilidad trazabilidad = db.trazabilidad.Find(id);
        //    db.trazabilidad.Remove(trazabilidad);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                trazabilidad trazabilidad = db.trazabilidad.Find(id);
                db.trazabilidad.Remove(trazabilidad);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "envios");
            }
            catch (Exception ex)
            {
                // Manejar el error y mostrar un mensaje de alerta
                string errorMessage = "No es posible realizar la operación. Detalles del error: " + ex.Message;
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            // Esta acción mostrará el mensaje de error almacenado en TempData
            return View();
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