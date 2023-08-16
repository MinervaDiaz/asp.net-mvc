using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using TransportesMVC.Models;
//TODOS LOS USING SON REFERENCIAS POR HACER REFERENCIA A OTRO LADO
using TransportesMVC.Models.ViewModels;
using static TransportesMVC.Models.Enum;

namespace TransportesMVC.Controllers
{
    public class CamionesController : Controller
    {
        // GET: Camiones
        public ActionResult Index()
        {
            List<CamionesLista> List = new List<CamionesLista>();
            using (TransportesEntities db = new TransportesEntities())
            {
                List = (from c in db.camiones
                        select new CamionesLista
                        {
                            id_camion = c.id_camion,
                            matricula = c.matricula,
                            tipo_camion = c.tipo_camion,
                            marca = c.marca,
                            modelo = c.modelo,
                            capacidad = c.capacidad,
                            kilometraje = c.kilometraje,
                            urlFoto = c.urlFoto,
                            disponibilidad = c.disponibilidad
                        }
                      ).ToList();
            }
            //en el return pasamos el modelo, osea la lista
            return View(List);
        }

        //este método es para cuando queremos acceder a la vista del nuevo camion
        public ActionResult Nuevo_Camion()
        {
            return View();
        }
        [HttpPost]
        //Polimorfismo de los métodos de arriba y abajo, enel método de abajo regresamos la peticion de POST, arriba solo es GET
        public ActionResult Nuevo_Camion(NuevoCamion model)
        {
            try
            {
                //validamos si el modelo es válido: esto es un helper para validar datos
                //modelstate es por parte de razor
                if (ModelState.IsValid)
                {
                    //entro a mi contexto de la base de datos
                    using (TransportesEntities db = new TransportesEntities())
                    {
                        var camion = new camiones();
                        camion.id_camion = model.id_camion;
                        camion.matricula = model.matricula;
                        camion.tipo_camion = model.tipo_camion;
                        camion.marca = model.marca;
                        camion.modelo = model.modelo;
                        camion.capacidad = model.capacidad;
                        camion.kilometraje = model.kilometraje;
                        camion.urlFoto = model.urlFoto;
                        camion.disponibilidad = model.disponibilidad;

                        //dentro del mi contexto, en el contexto de camiones se guardan los datos
                        db.camiones.Add(camion);
                        db.SaveChanges();
                        Alert("Registro guardado con éxito", NotificationType.success);
                    }

                    //si lo anterior se hizo con éxito regreso a la vista de Camiones
                    return Redirect("~/Camiones");
                }
                //SI REDIRECCIONO AL VIEW MODEL ES PORQUE FALLÓ UNA VALIDACIÓN
                Alert("Verificar la información", NotificationType.warning);
                return View(model);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Alert("Ha ocurrido un error: " + ex.Message, NotificationType.error);
                return View(model);
            }
        }

        //HELPER, STRING INCRUSTA TXT DE JAVA SCRIPT PARA EL CONTROLLER, Y CUANDO LLEGUE A LA VISTA LO INFIERA, POR MEDIO DEL VIEWDATA SE TIENE CONTROL DE ESO

        //le pasamos el mensaje y el tipo de notificación
        public void Alert(string message, NotificationType notificacionType)
        {
            var msg = "<script language='javascript'>Swal.fire('" +
                notificacionType.ToString().ToUpper() + "','" + message + "','" +
                notificacionType + "')" + "</script>";

            //asignamos la propiedad notification y el mensaje
            TempData["notification"] = msg;
        }

        //EDITAR
        public ActionResult Editar_Camion(int id)
        {
            //llamamos al contexto, voy a trabajar con el contexto
            camiones camiones = new camiones();
            using (TransportesEntities db = new TransportesEntities())
            {
                camiones = db.camiones.Where(x => x.id_camion == id).FirstOrDefault();
            }
            ViewBag.Title = "Editar Camion n°" + camiones.id_camion;
            return View(camiones);
        }

        //DELETE
        [HttpGet]
        public ActionResult Eliminar_Camion( int id)
        {
            camiones camion = new camiones();
            try
            {
                using(TransportesEntities db= new TransportesEntities())
                {
                    camion = db.camiones.Where(x=>x.id_camion==id).FirstOrDefault();
                    db.camiones.Remove(camion);
                    db.SaveChanges();
                }
                Alert("Registro exitoso" , NotificationType.success);
                return Redirect("~/Camiones");
            }
            catch (Exception ex)
            {
                Alert("Error: "+ex.Message, NotificationType.error);
                return Redirect("~/Camiones");
            }
        }

        //Polimorfismo de los métodos de arriba y abajo, enel método de abajo regresamos la peticion de POST, arriba solo es GET
        [HttpPost]
        public ActionResult Editar_Camion(camiones model)
        {
            try
            {
                //validamos si el modelo es válido: esto es un helper para validar datos
                //modelstate es por parte de razor
                if (ModelState.IsValid)
                {
                    //entro a mi contexto de la base de datos
                    using (TransportesEntities db = new TransportesEntities())
                    {
                        var camion = new camiones();
                        camion.id_camion = model.id_camion;
                        camion.matricula = model.matricula;
                        camion.tipo_camion = model.tipo_camion;
                        camion.marca = model.marca;
                        camion.modelo = model.modelo;
                        camion.capacidad = model.capacidad;
                        camion.kilometraje = model.kilometraje;
                        camion.urlFoto = model.urlFoto;
                        camion.disponibilidad = model.disponibilidad;

                        //dentro del mi contexto, en el contexto de camiones se guardan los datos
                        db.Entry(camion).State = System.Data.Entity.EntityState.Modified;
                        //db.camiones.Add(camion);
                        db.SaveChanges();
                        Alert("Registro guardado con éxito", NotificationType.success);
                    }

                    //si lo anterior se hizo con éxito regreso a la vista de Camiones
                    return Redirect("~/Camiones");
                }
                //SI REDIRECCIONO AL VIEW MODEL ES PORQUE FALLÓ UNA VALIDACIÓN
                Alert("Verificar la información", NotificationType.warning);
                return View(model);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Alert("Ha ocurrido un error: " + ex.Message, NotificationType.error);
                return View(model);
            }
        }
    
    }
}