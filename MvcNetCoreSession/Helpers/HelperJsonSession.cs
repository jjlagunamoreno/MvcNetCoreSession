using Newtonsoft.Json;

namespace MvcNetCoreSession.Helpers
{
    public class HelperJsonSession
    {
        //VAMOS A UTILIZAR EL MÉTODO GetString() COMO HERRAMIENTA
        //ALMACENAREMOS OBJETOS CON Serialize de JSON {nombre: "aa", raza: "perro"}
        public static string SerializeObject<T>(T data)
        {
            //CONVERTIMOS EL OBJETO A STRING MEDIANTE Nweton
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        //RECIBIREMOS UN string Y LO CONVERTIREMOS A CUALQUIER OBJETO T
        public static T DeserializeObject<T>(string data)
        {
            //DESERIALIZAMOS EL STRING A CUALQUIER OBJETO
            T objeto = JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }
    }
}
