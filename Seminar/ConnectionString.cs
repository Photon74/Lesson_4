using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar
{
    public class ConnectionString
    {
        public string Host { get; set; }

        public string DbName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return $"Host={Host};Catalog={DbName};User Id={UserName};Password={Password};";
        }
    }
}
