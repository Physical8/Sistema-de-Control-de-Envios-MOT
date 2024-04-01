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
    public class sedesController : Controller
    {
        private DB_web_v1LEOEntities db = new DB_web_v1LEOEntities();

        // GET: sedes
        public ActionResult Index()
        {
            return View(db.sedes.ToList());
        }

        // GET: sedes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sedes sedes = db.sedes.Find(id);
            if (sedes == null)
            {
                return HttpNotFound();
            }
            return View(sedes);
        }

        // GET: sedes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sedes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_sede,sede")] sedes sedes)
        {
            if (ModelState.IsValid)
            {
                db.sedes.Add(sedes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sedes);
        }

        // GET: sedes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sedes sedes = db.sedes.Find(id);
            if (sedes == null)
            {
                return HttpNotFound();
            }
            return View(sedes);
        }

        // POST: sedes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_sede,sede")] sedes sedes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sedes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sedes);
        }

        // GET: sedes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sedes sedes = db.sedes.Find(id);
            if (sedes == null)
            {
                return HttpNotFound();
            }
            return View(sedes);
        }

        // POST: sedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    sedes sedes = db.sedes.Find(id);
        //    db.sedes.Remove(sedes);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                sedes sedes = db.sedes.Find(id);
                db.sedes.Remove(sedes);
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
