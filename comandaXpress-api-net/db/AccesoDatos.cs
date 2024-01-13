using comandaXpress_api_net.Models;
using Dapper;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace comandaXpress_api_net.db
{
    public class AccesoDatos : IAccesoDatos
    {
        readonly string _cadenaConexion;

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

        public void MultipleInsert<T>(List<T> lista)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_cadenaConexion))
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in lista)
                        {
                            string consulta = "INSERT INTO TuTabla (Columna1, Columna2, ...) VALUES (@Columna1, @Columna2, ...)";

                            // Utiliza el método Execute de Dapper con la transacción
                            dbConnection.Execute(consulta, registro, transaction);
                        }

                        // Confirma la transacción
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // En caso de error, revierte la transacción
                        transaction.Rollback();
                        throw; // Puedes manejar o registrar la excepción según tus necesidades
                    }
                }
            }
        }
    }
}
