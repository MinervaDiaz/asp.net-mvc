using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransportesMVC.Models.ViewModels
{
    public class NuevaRuta
    {
        //Ponemos las validaciones
        public int id_ruta { get; set; }
        [Required]
        public int camion_id { get; set; }
        [Required]
        public Nullable<int> chofer_id { get; set; }
        [Required]
        public int direcciones_origen_id { get; set; }
        [Required]
        public int direcciones_destino_id { get; set; }
        [Required]
        public Nullable<double> distancia { get; set; }
        [Required]
        [DataType(DataType.Date)] //esto es para generar un picker de fecha
        public Nullable<System.DateTime> fecha_salida { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> fecha_llegada_estimada { get; set; }
        public Nullable<System.DateTime> fecha_llegada_real { get; set; }
    }
}