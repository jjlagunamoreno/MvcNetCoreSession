using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MvcNetCoreSession.Data
{
    public class HelperBinarySession
    {
        //VAMOS A CREAR DOS METODOS static
        //PORQUE NO NECESITAMOS REALIZAR NEW PARA
        //UTILIZAR LOS METODOS DE CONVERSION QUE CREAREMOS
        //EN ESTA CLASE
        //CONVERTIREMOS UN OBJETO A BYTE[]
        public static byte[] ObjectToByte(Object objeto)
        {
            BinaryFormatter formateador =
                new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formateador.Serialize(stream, objeto);
                return stream.ToArray();
            }
        }
        //CONVERSOR DE BYTE[] A OBJETO
        public static  Object ByteToObject(byte[] data)
        {
            BinaryFormatter formateador =
                new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                Object objeto = (Object)
                    formateador.Deserialize(stream);
                return objeto;
            }
        }
    }
}
