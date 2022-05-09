using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ITSolution.Framework.Core.Common.BaseClasses.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class ItsSerializer
    {
        /// <summary>
        /// Deserializa um objeto
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        [Obsolete("Utilize JsonSerializer ou XmlSerializer")]
        public static T DeserializeObject<T>(byte[] bytes) where T : class
        {
            if (bytes != null)
            {
                Object obj = new Object();
                var binaryFormatter = new BinaryFormatter();
                using (MemoryStream memoryStream = new MemoryStream())
                {

                    memoryStream.Write(bytes, 0, bytes.Length);
                    memoryStream.Position = 0;
                    obj = binaryFormatter.Deserialize(memoryStream);
                    return obj as T;
                }
            }
            else
                return null;
        }

        [Obsolete("Utilize JsonSerializer ou XmlSerializer")]
        public static Object DeserializeObjectO(byte[] bytes)
        {
            if (bytes != null)
            {
                Object obj = new Object();
                var binaryFormatter = new BinaryFormatter();
                using (MemoryStream memoryStream = new MemoryStream())
                {

                    memoryStream.Write(bytes, 0, bytes.Length);
                    memoryStream.Position = 0;
                    obj = binaryFormatter.Deserialize(memoryStream);
                    return obj;
                }
            }
            else
                return null;
        }
        [Obsolete("Utilize JsonSerializer ou XmlSerializer")]
        public static byte[] SerializeObject(Object obj)
        {
            if (obj != null)
            {
                var binaryFormatter = new BinaryFormatter();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, obj);

                    return memoryStream.GetBuffer();
                }
            }
            else
                return null;

        }
    }
}
