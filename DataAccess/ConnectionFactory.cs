using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConnectionFactory : IDbConnectionFactory
    {

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            //string ConnectionString = ConfigurationManager.ConnectionStrings[nameOrConnectionString];
            string ConnectionString = BlackJack.DataAccess.Properties.Resources.ResourceManager.GetString(nameOrConnectionString);
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var conn = factory.CreateConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            return conn;
        }
    }
}
