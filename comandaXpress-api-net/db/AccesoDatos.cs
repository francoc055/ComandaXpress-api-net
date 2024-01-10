using comandaXpress_api_net.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace comandaXpress_api_net.db
{
    public class AccesoDatos : IAccesoDatos
    {
        //private MySqlConnection conexion;
        private string server = "localhost";
        private string database = "comanda";
        private string user = "root";
        private string password = "";
        private string cadenaConexion;
      

        //public MySqlConnection getConexion()
        //{
        //    this.cadenaConexion = "Server=" + server + ";Database=" + database + ";User Id=" + user + ";Password=" + password;

        //    if (this.conexion is null)
        //    {
        //        conexion = new MySqlConnection(cadenaConexion);
        //        conexion.Open();
        //    }

        //    return conexion;
        //}

        public IEnumerable<T> QueryGetAll<T>(string query)
        {
            this.cadenaConexion = "Server=" + server + ";Database=" + database + ";User Id=" + user + ";Password=" + password;

            using (IDbConnection dbConnection = new MySqlConnection(cadenaConexion))
            {
                dbConnection.Open();
                return dbConnection.Query<T>(query);
            }
        }

        public T QueryGetById<T>(string query, object obj = null)
        {
            this.cadenaConexion = "Server=" + server + ";Database=" + database + ";User Id=" + user + ";Password=" + password;
            using (IDbConnection dbConnection = new MySqlConnection(cadenaConexion))
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<T>(query, obj);
            }
        }

        public int Query(string query, object obj = null)
        {
            this.cadenaConexion = "Server=" + server + ";Database=" + database + ";User Id=" + user + ";Password=" + password;

            using (IDbConnection dbConnection = new MySqlConnection(cadenaConexion))
            {
                dbConnection.Open();
                return dbConnection.Execute(query, obj);
            }
        }
    }
}
