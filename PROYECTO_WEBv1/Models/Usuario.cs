using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROYECTO_WEBv1.Models
{
    public class Usuario
    {
        public int id_usuario {  get; set; }
        public string nombre_usuario { get; set; }
        public string contrasenia { get; set; }


        public string ConfirmarClave { get; set; }
    }
}