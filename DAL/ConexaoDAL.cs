using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class ConexaoDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;
       
        public void InserirConexao(Conexao objConexao)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO ConEmpresarial(NmConEmpresarial, LogoConEmpresarial) VALUES(@nome, @logo)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", objConexao.NmConEmpresarial);
            cmd.Parameters.AddWithValue("@logo", objConexao.LogoConEmpresarial);
            cmd.ExecuteNonQuery();
            conn.Close();
;        }
        public DataTable ListarConexoes()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT * FROM ConEmpresarial";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public void RemoverConexao(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM ConEmpresarial WHERE IDConEmpresarial = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
