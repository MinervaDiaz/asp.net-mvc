using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportesMVC.Models;
using TransportesMVC.Models.ViewModels;

namespace TransportesMVC.Controllers
{
    public class RutasController : Controller
    {
        // GET: Rutas
        public ActionResult IndexView()
        {
            List<ListViewRuta> lista = new List<ListViewRuta>();
            using (TransportesEntities context = new TransportesEntities())
            {
                lista = (from VR in context.View_Rutas
                         select new ListViewRuta
                         {
                             id_ruta = VR.id_ruta,
                             matricula = VR.matricula,
                             camion_id = VR.camion_id,
                             chofer_id = VR.chofer_id,
                             chofer = VR.chofer,
                             direcciones_origen_id = VR.direcciones_origen_id,
                             direcciones_origen = VR.direcciones_origen,
                             direcciones_destino_id= VR.direcciones_destino_id,
                             direcciones_destino = VR.direcciones_destino,
                             distancia = VR.distancia,
                             fecha_salida = VR.fecha_salida,
                             fecha_llegada_estimada = VR.fecha_llegada_estimada,
                             fecha_llegada_real = VR.fecha_llegada_real
                         }
                ).ToList();
            }
            return View(lista);
        }
    }
}