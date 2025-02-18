using Microsoft.AspNetCore.Mvc;
using MvcNetCoreSession.Data;
using MvcNetCoreSession.Models;

namespace MvcNetCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SessionCollection(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //CREAMOS UNA COLECCION DE OBJETOS Mascota
                    List<Mascota> mascotas = new List<Mascota>()
                    {
                        new Mascota { Nombre = "Firulais", Raza = "Pastor Aleman", Edad = 3 },
                        new Mascota { Nombre = "Piolin", Raza = "Canario", Edad = 1 },
                        new Mascota { Nombre = "Nala", Raza = "Puerco", Edad = 21 },
                        new Mascota { Nombre = "Rafi", Raza = "Mono", Edad = 6 },
                        new Mascota { Nombre = "Sebas", Raza = "Canario", Edad = 2 },
                        new Mascota { Nombre = "Garfield", Raza = "Gato", Edad = 2 }
                    };
                    byte[] data =
                        HelperBinarySession.ObjectToByte(mascotas);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"] = "Mascotas almacenadas en Session";
                    return View();
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotas = (List<Mascota>)
                        HelperBinarySession.ByteToObject(data);
                    return View(mascotas);
                }
            }
            return View();
        }

        public IActionResult SessionMascota(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Flounder";
                    mascota.Raza = "Pez";
                    mascota.Edad = 5;
                    //PARA ALMACENAR OBJETOS Mascota DEBEMOS
                    //CONVERTIRLOS A byte[]
                    byte[] data =
                        HelperBinarySession.ObjectToByte(mascota);
                    //ALMACENAMOS EL OBJETO EN SESSION MEDIANTE Set
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Mascota almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS BYTES DE MASCOTA DE SESSION
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    //CONVERTIMOS LOS BYTES RECUPERADOS A OBJETO MASCOTA
                    Mascota mascota = (Mascota)
                        HelperBinarySession.ByteToObject(data);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionSimple(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora",
                        DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"] = "Datos almacenados en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS DATOS DE SESSION
                    ViewData["USUARIO"] = 
                        HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] =
                        HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }
    }
}
