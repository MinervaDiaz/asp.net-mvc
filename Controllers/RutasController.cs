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

        //FORMA DE LISTAR DE MANERA MANUAL O PERSONALIZADA
        public ActionResult IndexViewPersonalizado()
        {
            List<ListViewRuta> lista = new List<ListViewRuta>();
            //conexion de un sólo uso
            using (TransportesEntities context = new TransportesEntities())
            {
                //vamos a usar elcontexto, ya no la VISTA
                foreach(var ruta in context.rutas)
                {
                    ListViewRuta aux = new ListViewRuta();
                    //lista de atributos que no están referenciadas
                    aux.id_ruta = ruta.id_Ruta;
                    aux.distancia = ruta.distancia;
                    aux.fecha_salida = ruta.fecha_salida;
                    aux.fecha_llegada_estimada = aux.fecha_llegada_estimada;
                    aux.fecha_llegada_real = aux.fecha_llegada_real;

                    //campos referenciados
                    aux.camion_id = ruta.camion_id;
                    aux.matricula = (from cam in context.camiones where cam.id_camion == ruta.camion_id select cam.matricula).FirstOrDefault();
                    aux.chofer_id = ruta.chofer_id;
                    //chofer es un objeto aux porque existe unic en el foreach
                    choferes chofer = (from chof in context.choferes where chof.id_chofer == ruta.chofer_id select chof).FirstOrDefault();
                    aux.chofer = chofer.nombre + " " + chofer.apellido_Paterno + " " + chofer.apellido_Materno;

                    aux.direcciones_origen_id = ruta.direcciones_origen_id;
                    direcciones DO = (from dio in context.direcciones where dio.id_direccion == ruta.direcciones_origen_id select dio).FirstOrDefault();

                    //subconsulta de linq, lo que está etree () está aislado, son variables temp existen durante la query 
                    aux.direcciones_destino_id = ruta.direcciones_destino_id;
                    direcciones DD = (from dio in context.direcciones where dio.id_direccion == ruta.direcciones_destino_id select dio).FirstOrDefault();

                    aux.direcciones_origen = "Calle "+ DO.calle + " # " + DO.numero + " Col. " + DO.colonia + " CP." + DO.CP;
                    aux.direcciones_destino = "Calle "+ DO.calle + " # " + DO.numero + " Col. " + DO.colonia + " CP." + DO.CP;

                    lista.Add(aux);
                }
            }
            return View(lista);
        }
    }
}