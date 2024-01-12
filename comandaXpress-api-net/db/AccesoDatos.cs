using comandaXpress_api_net.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace comandaXpress_api_net.db
{
    public class AccesoDatos : IAccesoDatos
    {
        //private string server = "localhost";
        //private string database = "comanda";
        //private string user = "root";
        //private string password = "";
        private readonly string _cadenaConexion;


        public AccesoDatos(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("cadenaConexion");
        }


        public IEnumerable<T> QueryGetAll<T>(string query)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_cadenaConexion))
            {
                dbConnection.Open();
                return dbConnection.Query<T>(query);
            }
        }

        public T QueryGetById<T>(string query, object obj = null)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_cadenaConexion))
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<T>(query, obj);
            }
        }

        public int Query(string query, object obj = null)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_cadenaConexion))
            {
                dbConnection.Open();
                return dbConnection.Execute(query, obj);
            }
        }
    }
}
