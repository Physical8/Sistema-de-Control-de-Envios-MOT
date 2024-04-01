using PROYECTO_WEBv1.Models;
using PROYECTO_WEBv1.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;


namespace PROYECTO_WEBv1.Controllers
{
    [ValidarSesion]
    public class HomeController : Controller
    {
        // Instaciamiento del Modelo de BD
        private DB_web_v1LEOEntities bd = new DB_web_v1LEOEntities();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Envios(int? id_buscado)
        {
            var envio = bd.envios
                .Include(e => e.clientes)
                .Include(e => e.clientes1)
                .Include(e => e.ciudades)
                .Include(e => e.ciudades1)
                .FirstOrDefault(env => env.id_envio == id_buscado);

            if (envio != null)
            {
                return View(envio);
            }
            else
            {
                return View();
            }
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Acceso");
        }


    }
}