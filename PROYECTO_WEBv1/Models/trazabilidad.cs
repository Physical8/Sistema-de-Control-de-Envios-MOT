//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PROYECTO_WEBv1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class trazabilidad
    {
        public int id_trazabilidad { get; set; }
        public int id_envio { get; set; }
        public string punto_control { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
    
        public virtual envios envios { get; set; }
    }
}
