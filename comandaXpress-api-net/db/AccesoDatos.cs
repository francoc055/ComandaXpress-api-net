using comandaXpress_api_net.Models;
using Dapper;
using Microsoft.Win32;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace comandaXpress_api_net.db
{
    public class AccesoDatos : IAccesoDatos
    {
        readonly string _cadenaConexion;

        public AccesoDatos(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("cadenaConexion");
        }


        public IEnumerable<T> GetAll<T>(string query)
        {
            using (IDbConnection dbConnection = new SqlConnection(_cadenaConexion))
            {
                dbConnection.Open();
                return dbConnection.Query<T>(query);
            }
        }

        public T GetById<T>(string query, object obj = null)
        {
            using (IDbConnection dbConnection = new SqlConnection(_cadenaConexion))
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<T>(query, obj);
            }
        }

        public int Insert(string query, object obj = null)
        {
            using (IDbConnection dbConnection = new SqlConnection(_cadenaConexion))
            {
                dbConnection.Open();
                return dbConnection.QuerySingle<int>(query, obj);
            }
        }

        public int UpdateRemove(string query, object obj = null)
        {
            using (IDbConnection dbConnection = new SqlConnection(_cadenaConexion))
            {
                dbConnection.Open();
                return dbConnection.Execute(query, obj);
            }
        }


        public void MultipleInsert(List<PedidoProducto> lista, int idPedido)
        {
            using (IDbConnection dbConnection = new SqlConnection(_cadenaConexion))
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        foreach (PedidoProducto item in lista)
                        {
                            string consulta = "INSERT INTO pedidos_productos (idPedido, idProducto, cantidad) VALUES (@IdPedido, @IdProducto, @Cantidad)";

                            var parametros = new
                            {
                                IdPedido = idPedido,
                                IdProducto = item.IdProducto,
                                Cantidad = item.Cantidad
                            };


                            dbConnection.Execute(consulta, parametros, transaction);
                        }


                        transaction.Commit();
                    }
                    catch (Exception)
                    {

                        transaction.Rollback();
                        throw; 
                    }
                }
            }
        }
    }
}
