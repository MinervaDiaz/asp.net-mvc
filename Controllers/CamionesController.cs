using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportesMVC.Models;
using TransportesMVC.Models.ViewModels;

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
                if (ModelState.IsValid)
                {
                    //entro a mi contexto de la base de datos
                    using (TransportesEntities db = new TransportesEntities())
                    {
                        var camion = new camiones();
                        camion.id_camion= model.id_camion;
                        camion.matricula=model.matricula;
                        camion.tipo_camion = model.tipo_camion;
                        camion.marca=model.marca;
                        camion.modelo = model.modelo;
                        camion.capacidad=model.capacidad;
                        camion.kilometraje=model.kilometraje;
                        camion.urlFoto = model.urlFoto;
                        camion.disponibilidad=model.disponibilidad;

                        //dentro del mi contexto, en el contexto de camiones se guardan los datos
                        db.camiones.Add( camion );
                        db.SaveChanges();
                    }
                    //si lo anterior se hizo con éxito regreso a la vista de Camiones
                    return Redirect("~/Camiones");
                }
                return View(model);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}