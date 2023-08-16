using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportesMVC.Models;
using TransportesMVC.Models.ViewModels;
using static TransportesMVC.Models.Enum;

namespace TransportesMVC.Controllers
{
    public class RutasController : Controller
    {
        //Para rutear por defecto la url de de rutas/
        public ActionResult Index()
        {
            return Redirect("~/Rutas/IndexView");
        }

        // VISTA DESDE SQL (recomendable usar en estos proyectos, cuando se hace mucha consulta)
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
                             direcciones_destino_id = VR.direcciones_destino_id,
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

        //LISTA PERSONALIZADA (recomendable usar en el front cuando necesite un objeto personalizado, si se hace en el server le pega más por el rendimiento : tarda más)
        public ActionResult IndexViewPersonalizado()
        {
            List<ListViewRuta> lista = new List<ListViewRuta>();
            //conexion de un sólo uso
            using (TransportesEntities context = new TransportesEntities())
            {
                //vamos a usar elcontexto, ya no la VISTA
                foreach (var ruta in context.rutas)
                {
                    ListViewRuta aux = new ListViewRuta();
                    //lista de atributos que no están referenciadas
                    aux.id_ruta = ruta.id_Ruta;
                    aux.distancia = ruta.distancia;
                    aux.fecha_salida = ruta.fecha_salida;
                    aux.fecha_llegada_estimada = ruta.fecha_llegada_estimada;
                    aux.fecha_llegada_real = ruta.fecha_llegada_real;

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

                    aux.direcciones_origen = "Calle " + DO.calle + " # " + DO.numero + " Col. " + DO.colonia + " CP." + DO.CP;
                    aux.direcciones_destino = "Calle " + DO.calle + " # " + DO.numero + " Col. " + DO.colonia + " CP." + DO.CP;

                    lista.Add(aux);
                }
            }
            return View(lista);
        }

        //INNER JOIN en Linq (cuando lo consulto una sola vez, o cuando no tenemos access a la BD)
        public ActionResult IndexViewLinQ()
        {
            List<ListViewRuta> lista = new List<ListViewRuta>();
            using (TransportesEntities context = new TransportesEntities())
            {
                lista = (from r in context.rutas
                         join cam in context.camiones on r.camion_id equals cam.id_camion
                         join chof in context.choferes on r.chofer_id equals chof.id_chofer
                         join DO in context.direcciones on r.direcciones_origen_id equals DO.id_direccion
                         join DD in context.direcciones on r.direcciones_destino_id equals DD.id_direccion
                         //selecciono un nuevo object list view rutas
                         select new ListViewRuta
                         {
                             id_ruta = r.id_Ruta,
                             camion_id = r.camion_id,
                             matricula = cam.matricula,
                             chofer_id = r.chofer_id,
                             chofer = chof.nombre + " " + chof.apellido_Paterno + " " + chof.apellido_Materno,
                             direcciones_origen_id = r.direcciones_origen_id,
                             direcciones_destino_id = r.direcciones_destino_id,
                             direcciones_origen = "Calle " + DO.calle + " # " + DO.numero + " Col. " + DO.colonia + " CP." + DO.CP,
                             direcciones_destino = "Calle " + DD.calle + " # " + DD.numero + " Col. " + DD.colonia + " CP." + DD.CP,
                             distancia = r.distancia,
                             fecha_salida = r.fecha_salida,
                             fecha_llegada_estimada = r.fecha_llegada_estimada,
                             fecha_llegada_real = r.fecha_llegada_real
                         }
                         ).ToList();
            }
            return View(lista);
        }

        //sobrecarga: dos metodos iguales, uno para la vista otro para las validaciones
        public ActionResult Nueva_Ruta()
        {
            //aqui agregamos los ddl : crear primero el modelo CamionesDDL
            List<CamionesDDL> listacam = new List<CamionesDDL>();
            listacam.Insert(0, new CamionesDDL { id_camion = 0, matricula = "Seleccione un camión" });
            using (TransportesEntities context = new TransportesEntities())
            {
                
                foreach(var c in context.camiones)
                {
                    //por cada camion se crea un auxiliar y se añade a la lista
                    CamionesDDL aux = new CamionesDDL();
                    aux.id_camion = c.id_camion;
                    aux.matricula = c.matricula;
                    listacam.Add(aux);
                }
            }
            ViewBag.ListaCamiones = listacam;
            return View();
        }


        [HttpPost]
        public ActionResult Nueva_Ruta(NuevaRuta model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    using (TransportesEntities context = new TransportesEntities())
                    {
                        var ruta = new rutas();
                        ruta.camion_id = model.camion_id;
                        ruta.distancia = model.distancia;
                        ruta.fecha_salida = model.fecha_salida;
                        ruta.fecha_llegada_estimada = model.fecha_llegada_estimada;
                        ruta.fecha_llegada_real = model.fecha_llegada_real;
                        ruta.chofer_id=model.chofer_id;
                        ruta.direcciones_origen_id = model.direcciones_origen_id;
                        ruta.direcciones_destino_id = model.direcciones_destino_id;

                        context.rutas.Add(ruta);
                        context.SaveChanges();
                        Alert("Registro guardado con éxito", NotificationType.success);

                    } return Redirect("~/Rutas/IndexViewLinQ");
                }
                Alert("Verifique la información", NotificationType.warning);
                return View(model);
            }
            catch (Exception e)
            {
                Alert("Error: ", NotificationType.error);
                return View(model);
            }
        }

        public void Alert(string message, NotificationType notificacionType)
        {
            var msg = "<script language='javascript'>Swal.fire('" +
                notificacionType.ToString().ToUpper() + "','" + message + "','" +
                notificacionType + "')" + "</script>";

            //asignamos la propiedad notification y el mensaje
            TempData["notification"] = msg;
        }
    }

}