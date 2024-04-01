using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROYECTO_WEBv1.Models;

namespace PROYECTO_WEBv1.Controllers
{
    public class enviosController : Controller
    {
        private DB_web_v1LEOEntities db = new DB_web_v1LEOEntities();

        // GET: envios
        public ActionResult Index()
        {
            var envios = db.envios.Include(e => e.ciudades).Include(e => e.ciudades1).Include(e => e.clientes).Include(e => e.clientes1).Include(e => e.sedes);
            return View(envios.ToList());
        }

        // GET: envios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            envios envios = db.envios.Find(id);
            if (envios == null)
            {
                return HttpNotFound();
            }
            return View(envios);
        }

        // GET: envios/Create
        public ActionResult Create()
        {
            ViewBag.id_ciudad_origen = new SelectList(db.ciudades, "id_ciudad", "ciudad");
            ViewBag.id_ciudad_destino = new SelectList(db.ciudades, "id_ciudad", "ciudad");
            ViewBag.id_emisor = new SelectList(db.clientes, "id_cliente", "identificacion");
            ViewBag.id_receptor = new SelectList(db.clientes, "id_cliente", "identificacion");
            ViewBag.id_sede = new SelectList(db.sedes, "id_sede", "sede");
            return View();
        }

        // POST: envios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_envio,id_sede,id_emisor,id_ciudad_origen,id_receptor,id_ciudad_destino,descripcion_paquete,peso,valor_declarado,largo,alto,ancho,tipo_envio,valor_envio,fecha_creacion,estado_envio")] envios envios)
        {
            if (ModelState.IsValid)
            {
                db.envios.Add(envios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_ciudad_origen = new SelectList(db.ciudades, "id_ciudad", "ciudad", envios.id_ciudad_origen);
            ViewBag.id_ciudad_destino = new SelectList(db.ciudades, "id_ciudad", "ciudad", envios.id_ciudad_destino);
            ViewBag.id_emisor = new SelectList(db.clientes, "id_cliente", "identificacion", envios.id_emisor);
            ViewBag.id_receptor = new SelectList(db.clientes, "id_cliente", "identificacion", envios.id_receptor);
            ViewBag.id_sede = new SelectList(db.sedes, "id_sede", "sede", envios.id_sede);
            return View(envios);
        }

        // GET: envios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            envios envios = db.envios.Find(id);
            if (envios == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_ciudad_origen = new SelectList(db.ciudades, "id_ciudad", "ciudad", envios.id_ciudad_origen);
            ViewBag.id_ciudad_destino = new SelectList(db.ciudades, "id_ciudad", "ciudad", envios.id_ciudad_destino);
            ViewBag.id_emisor = new SelectList(db.clientes, "id_cliente", "identificacion", envios.id_emisor);
            ViewBag.id_receptor = new SelectList(db.clientes, "id_cliente", "identificacion", envios.id_receptor);
            ViewBag.id_sede = new SelectList(db.sedes, "id_sede", "sede", envios.id_sede);
            return View(envios);
        }

        // POST: envios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_envio,id_sede,id_emisor,id_ciudad_origen,id_receptor,id_ciudad_destino,descripcion_paquete,peso,valor_declarado,largo,alto,ancho,tipo_envio,valor_envio,fecha_creacion,estado_envio")] envios envios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(envios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_ciudad_origen = new SelectList(db.ciudades, "id_ciudad", "ciudad", envios.id_ciudad_origen);
            ViewBag.id_ciudad_destino = new SelectList(db.ciudades, "id_ciudad", "ciudad", envios.id_ciudad_destino);
            ViewBag.id_emisor = new SelectList(db.clientes, "id_cliente", "identificacion", envios.id_emisor);
            ViewBag.id_receptor = new SelectList(db.clientes, "id_cliente", "identificacion", envios.id_receptor);
            ViewBag.id_sede = new SelectList(db.sedes, "id_sede", "sede", envios.id_sede);
            return View(envios);
        }

        // GET: envios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            envios envios = db.envios.Find(id);
            if (envios == null)
            {
                return HttpNotFound();
            }
            return View(envios);
        }

        // POST: envios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Buscar el envío por su id
            envios envio = db.envios.Find(id);

            // Verificar si se encontró el envío
            if (envio != null)
            {
                // Borrar las trazabilidades asociadas al envío
                var trazabilidades = db.trazabilidad.Where(t => t.id_envio == id);
                db.trazabilidad.RemoveRange(trazabilidades);

                // Borrar el envío
                db.envios.Remove(envio);

                // Guardar los cambios en la base de datos
                db.SaveChanges();
            }

            // Redireccionar a la página principal (o cualquier otra página que desees)
            return RedirectToAction("Index");
        }
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    envios envios = db.envios.Find(id);
        //    db.envios.Remove(envios);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
