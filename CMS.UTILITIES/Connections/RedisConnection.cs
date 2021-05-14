using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.UTILITIES.Connections
{
    public class RedisConnection
    {
        static RedisConnection()
        {
            RedisConnection.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                string port = ConfigurationManager.AppSettings["EndPointPort"].ToString();
                var configOptions = new ConfigurationOptions();
                configOptions.EndPoints.Add(port);
                configOptions.DefaultDatabase = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultDatabase"].ToString());
                //return ConnectionMultiplexer.Connect("127.0.0.1:6379");
                return ConnectionMultiplexer.Connect(configOptions);
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
