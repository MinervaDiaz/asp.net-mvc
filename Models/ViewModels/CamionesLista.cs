using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportesMVC.Models.ViewModels
{
    public class CamionesLista
    {

        public int id_camion { get; set; }
        public string matricula { get; set; }
        public string tipo_camion { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public int capacidad { get; set; }
        public double kilometraje { get; set; }
        public string urlFoto { get; set; }
        public bool disponibilidad { get; set; }

    }
}