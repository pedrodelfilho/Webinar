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
    public class CidadeDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;
        public List<Cidade> ListarCidades(string uf)
        {
            List<Cidade> listarCidades = new List<Cidade>();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT NomeCidades From Cidades WHERE Estado = @uf";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uf", uf);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                Cidade objCidade;
                while (dr.Read())
                {
                    objCidade = new Cidade();
                    objCidade.IDCidade = Convert.ToInt32(dr["IDCidade"]);
                    objCidade.Estado = dr["Estado"].ToString();
                    objCidade.UF = dr["UF"].ToString();
                    objCidade.NomeCidade = dr["NomeCidade"].ToString();
                }
            }            
            conn.Close();
            return listarCidades;
        }
    }
}
