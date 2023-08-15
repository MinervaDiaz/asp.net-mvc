using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransportesMVC.Models
{
    public class NuevoCamion
    {

        public int id_camion { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "Matricula")]
        public string matricula { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "Tipo camion")]
        public string tipo_camion { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "Marca")]
        public string marca { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "Modelo")]
        public string modelo { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "Capacidad")]
        public int capacidad { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "Kilometraje")]
        public double kilometraje { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "urlFoto")]
        [DataType(DataType.ImageUrl)] //haciendo uso de un HTML HELPER mvc permite poner eso: para poner un input de tipo FILE
        public string urlFoto { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "disponibilidad")]
        public bool disponibilidad { get; set; }
    }
}