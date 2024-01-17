using comandaXpress_api_net.Models;
using MySql.Data.MySqlClient;

namespace comandaXpress_api_net.db
{
    public interface IAccesoDatos
    {

        public IEnumerable<T> GetAll<T>(string query);
        public T GetById<T>(string query, object obj = null);
        public int Insert(string query, object obj = null);
        public int UpdateRemove(string query, object obj = null);
        public void MultipleInsert(List<PedidoProducto> lista, int idPedido);



    }
}
