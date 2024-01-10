using MySql.Data.MySqlClient;

namespace comandaXpress_api_net.db
{
    public interface IAccesoDatos
    {
        //public AccesoDatos GetInstancia();

        //public MySqlConnection getConexion();
        public IEnumerable<T> QueryGetAll<T>(string query);

        public T QueryGetById<T>(string query, object obj = null);


        public int Query(string query, object obj = null);




    }
}
