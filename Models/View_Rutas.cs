//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransportesMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class View_Rutas
    {
        public int id_ruta { get; set; }
        public int camion_id { get; set; }
        public string matricula { get; set; }
        public Nullable<int> chofer_id { get; set; }
        public string chofer { get; set; }
        public int direcciones_origen_id { get; set; }
        public string direcciones_origen { get; set; }
        public int direcciones_destino_id { get; set; }
        public string direcciones_destino { get; set; }
        public Nullable<double> distancia { get; set; }
        public Nullable<System.DateTime> fecha_salida { get; set; }
        public Nullable<System.DateTime> fecha_llegada_estimada { get; set; }
        public Nullable<System.DateTime> fecha_llegada_real { get; set; }
    }
}