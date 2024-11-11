using Microsoft.Data.SqlClient;
using System.Data;

namespace BlazorCrud.Server.Data
{
    public class Conexion
    {
        private readonly string _connectionString;

        public Conexion(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        public DataSet EjecutarConsulta(string query)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(ds);
            }
            return ds;
        }
    }
}
