namespace Seminar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString1 = new ConnectionString
            {
                Host = "localhost",
                DbName = "Db1",
                UserName = "User1",
                Password = "12345"
            };

            var connectionString2 = new ConnectionString
            {
                Host = "localhost",
                DbName = "Db2",
                UserName = "User2",
                Password = "54321"
            };

            var connections = new List<ConnectionString>
            {
                connectionString1,
                connectionString2
            };

            CacheProvider.CacheConnections(connections);

            connections = CacheProvider.GetConnectionsFromCache();

            foreach (var connection in connections)
                Console.WriteLine(connection);

            Console.ReadKey(true);
        }
    }
}