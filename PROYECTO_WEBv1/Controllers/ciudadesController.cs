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
    public class ciudadesController : Controller
    {
        private DB_web_v1LEOEntities db = new DB_web_v1LEOEntities();

        // GET: ciudades
        public ActionResult Index()
        {
            return View(db.ciudades.ToList());
        }

        // GET: ciudades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ciudades ciudades = db.ciudades.Find(id);
            if (ciudades == null)
            {
                return HttpNotFound();
            }
            return View(ciudades);
        }

        // GET: ciudades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ciudades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ciudad,ciudad")] ciudades ciudades)
        {
            if (ModelState.IsValid)
            {
                db.ciudades.Add(ciudades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ciudades);
        }

        // GET: ciudades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ciudades ciudades = db.ciudades.Find(id);
            if (ciudades == null)
            {
                return HttpNotFound();
            }
            return View(ciudades);
        }

        // POST: ciudades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ciudad,ciudad")] ciudades ciudades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciudades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ciudades);
        }

        // GET: ciudades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ciudades ciudades = db.ciudades.Find(id);
            if (ciudades == null)
            {
                return HttpNotFound();
            }
            return View(ciudades);
        }

        // POST: ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ciudades ciudades = db.ciudades.Find(id);
        //    db.ciudades.Remove(ciudades);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ciudades ciudades = db.ciudades.Find(id);
                db.ciudades.Remove(ciudades);
                db.SaveChanges();
                return RedirectToAction("Index");
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
