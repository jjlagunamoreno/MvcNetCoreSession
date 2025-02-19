using Microsoft.AspNetCore.Mvc;
using MvcNetCoreSession.Extensions;
using MvcNetCoreSession.Helpers;
using MvcNetCoreSession.Models;

namespace MvcNetCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        HelperSessionContextAccessor helper;

        public EjemploSessionController(HelperSessionContextAccessor helper)
        {
            this.helper = helper;
        }

        public IActionResult Index()
        {
            List<Mascota> mascotas = this.helper.GetMascotasSession();
            return View(mascotas);
        }

        public IActionResult SessionMascotaCollection(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota {Nombre = "Patricio", Raza="Estrella de mar", Edad=18},
                        new Mascota {Nombre = "Pepe", Raza="Perro", Edad=8},
                        new Mascota {Nombre = "Mario", Raza="Pitufo", Edad=1},
                        new Mascota {Nombre = "Claudia", Raza="Dragón", Edad=138}
                    };
                    HttpContext.Session.SetObject("MASCOTAS", mascotas);
                    ViewData["MENSAJE"] = "Coleccion Mascotas almacenada";
                    return View();
                }
                else if (accion.ToLower() == "mostrar")
                {
                    List<Mascota> mascotas =
                        HttpContext.Session.GetObject<List<Mascota>>("MASCOTAS");
                    return View(mascotas);
                }
            }
            return View();
        }
        public IActionResult SessionMascotaObject(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Olaf",
                        Raza = "Muñeco",
                        Edad = 19
                    };
                    HttpContext.Session.SetObject("MASCOTAOBJECT", mascota);
                    ViewData["MENSAJE"] = "Mascota como Object almacenada";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    Mascota mascota =
                        HttpContext.Session.GetObject<Mascota>("MASCOTAOBJECT");
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionMascotaJson(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Tobby",
                        Raza = "Pastor Aleman",
                        Edad = 1
                    };
                    //UTILIZAMOS EL HELPER PARA CONVERTIR EL OBJETO A STRING
                    string jsonMascota =
                        HelperJsonSession.SerializeObject<Mascota>(mascota);
                    //ALMACENAMOS EL STRING EN SESSION
                    HttpContext.Session.SetString("MASCOTA", jsonMascota);
                    ViewData["MENSAJE"] = "Mascota JSON almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS EL STRING JSON DE MASCOTA EN SESSION
                    string json = HttpContext.Session.GetString("MASCOTA");
                    //CONVERTIMOS A STRING A MASCOTA
                    Mascota mascota = 
                        HelperJsonSession.DeserializeObject<Mascota>(json);
                    ViewData["MASCOTA"] = mascota;

                }
            }
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
