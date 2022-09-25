using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Security.Cryptography;

namespace Seminar
{
    public abstract class BaseCacheProviderException : Exception { }
    public class SerializeCacheProviderException : BaseCacheProviderException { }
    public class DeserializeCacheProviderException : BaseCacheProviderException { }
    public class ProtectCacheProviderException : BaseCacheProviderException { }
    public class UnprotectCacheProviderException : BaseCacheProviderException { }

    public static class CacheProvider
    {
        static readonly byte[] additionalEntropy = { 4, 2, 5, 8, 0 };

        public static void CacheConnections(List<ConnectionString> connections)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                using var memoryStream = new MemoryStream();
                using var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xmlSerializer.Serialize(xmlTextWriter, connections);

                var protectedData = Protect(memoryStream.ToArray());
                File.WriteAllBytes("data.protected", protectedData);
            }
            catch (Exception)
            {
                Console.WriteLine("Serialize error!");
                throw new SerializeCacheProviderException();
            }
        }

        public static List<ConnectionString> GetConnectionsFromCache()
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                var protectedData = File.ReadAllBytes("data.protected");
                var data = Unprotect(protectedData);
                return (List<ConnectionString>)xmlSerializer.Deserialize(new MemoryStream(data));
            }
            catch (Exception)
            {
                Console.WriteLine("Deserialize error!");
                throw new DeserializeCacheProviderException();
            }
        }

        private static byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (Exception)
            {
                Console.WriteLine("Protection error!");
                throw new ProtectCacheProviderException();
            }
        }

        private static byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, additionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (Exception)
            {
                Console.WriteLine("Unprotection error!");
                throw new UnprotectCacheProviderException();
            }
        }
    }
}
