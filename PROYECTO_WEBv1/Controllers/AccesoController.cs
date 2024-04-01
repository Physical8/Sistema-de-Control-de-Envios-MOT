using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PROYECTO_WEBv1.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services.Description;

namespace PROYECTO_WEBv1.Controllers
{
    public class AccesoController : Controller
    {

        static string cadena = "Data Source=LEOMAJE\\SQLEXPRESS;Initial Catalog=DB_web_v1;User Id=sa;Password=26101722a;";
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;

            if(oUsuario.contrasenia == oUsuario.ConfirmarClave)
            {
                oUsuario.contrasenia = oUsuario.contrasenia;
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn= new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("nombre_usuario", oUsuario.nombre_usuario);
                cmd.Parameters.AddWithValue("contrasenia", oUsuario.contrasenia);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            ViewData["Mensaje"] = mensaje;
            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("nombre_usuario", oUsuario.nombre_usuario);
                cmd.Parameters.AddWithValue("contrasenia", oUsuario.contrasenia);
                
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                oUsuario.id_usuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if(oUsuario.id_usuario != 0)
            {
                Session["usuario"] = oUsuario;
                return RedirectToAction("Index", "Home");
            }
            else {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }

            
        }
    }
}