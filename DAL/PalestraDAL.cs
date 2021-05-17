using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using Models;

namespace DAL
{
    public class PalestraDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;
        public void InserirPalestra(Palestra objPalestra)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Palestras(IDPalestrante,PalestraCriador,PalestraLink,PalestraDtCriacao,PalestraCapa,PalestraCategoria,PalestraTitulo,PalestraSubTitulo,PalestraSinopseP1,PalestraSinopseP2,PalestraSinopseP3,PalestraSinopseP4,PalestraDuracao,PalestraData,PalestraAutoriza) VALUES(@idPal,@criador,@link,@dtcriar,@capa,@cat,@titulo,@sub,@p1,@p2,@p3,@p4,@time,@dt,@autoriza)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idPal", objPalestra.IDPalestrante);
            cmd.Parameters.AddWithValue("@criador", objPalestra.PalestraCriador);
            cmd.Parameters.AddWithValue("@link", objPalestra.PalestraLink);
            cmd.Parameters.AddWithValue("@dtcriar", objPalestra.PalestraDtCriacao);
            cmd.Parameters.AddWithValue("@capa", objPalestra.PalestraCapa);
            cmd.Parameters.AddWithValue("@cat", objPalestra.PalestraCategoria);
            cmd.Parameters.AddWithValue("@titulo", objPalestra.PalestraTitulo);
            cmd.Parameters.AddWithValue("@sub", objPalestra.PalestraSubTitulo);
            cmd.Parameters.AddWithValue("@p1", objPalestra.PalestraSinopseP1);
            cmd.Parameters.AddWithValue("@p2", objPalestra.PalestraSinopseP2);
            cmd.Parameters.AddWithValue("@p3", objPalestra.PalestraSinopseP3);
            cmd.Parameters.AddWithValue("@p4", objPalestra.PalestraSinopseP4);
            cmd.Parameters.AddWithValue("@time", objPalestra.PalestraDuracao);
            cmd.Parameters.AddWithValue("@dt", objPalestra.PalestraData);
            cmd.Parameters.AddWithValue("@autoriza", objPalestra.PalestraAutoriza);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public List<Palestra> PalestraPendente()
        {
            List<Palestra> listaPalestras = new List<Palestra>();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT IDPalestra, IDPalestrante, convert(varchar(10), PalestraDtCriacao, 103) + ' ' + convert(varchar(8), PalestraDtCriacao, 14) AS PalestraDtCriacao, PalestraAutoriza FROM Palestras WHERE PalestraAprovada IS NULL OR PalestraAprovada = 'false'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                Palestra objPalestra;
                while (dr.Read())
                {
                    objPalestra = new Palestra();
                    objPalestra.IDPalestra = Convert.ToInt32(dr["IDPalestra"]);
                    objPalestra.IDPalestrante = Convert.ToInt32(dr["IDPalestrante"]);
                    objPalestra.PalestraDtCriacao = Convert.ToDateTime(dr["PalestraDtCriacao"]);
                    objPalestra.PalestraAutoriza = Convert.ToBoolean(dr["PalestraAutoriza"]);
                    //if (objPalestra.PalestraAutoriza != true) { objPalestra.PalestraAutoriza = false; }
                }
            }
            conn.Close();
            return listaPalestras;
        }
        public Palestra ObterPalestra(int id)
        {
            Palestra objPalestra = null;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT IDPalestra, IDPalestrante, PalestraCriador, PalestraLink, convert(varchar(10), PalestraDtCriacao, 103) AS PalestraDtCriacao, PalestraCapa, PalestraCategoria, PalestraTitulo, PalestraSubTitulo, PalestraSinopseP1, PalestraSinopseP2, PalestraSinopseP3, PalestraSinopseP4, PalestraDuracao, convert(varchar(10), PalestraData, 103) AS PalestraData, PalestraAutoriza FROM Palestras WHERE IDPalestra = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objPalestra = new Palestra();
                objPalestra.IDPalestra = Convert.ToInt32(dr["IDPalestra"]);
                objPalestra.IDPalestrante = Convert.ToInt32(dr["IDPalestrante"]);
                objPalestra.PalestraCriador = Convert.ToInt32(dr["PalestraCriador"]);
                objPalestra.PalestraLink = dr["PalestraLink"].ToString();
                objPalestra.PalestraDtCriacao = Convert.ToDateTime(dr["PalestraDtCriacao"]);
                objPalestra.PalestraCapa = (byte[])dr["PalestraCapa"];
                objPalestra.PalestraCategoria = dr["PalestraCategoria"].ToString();
                objPalestra.PalestraTitulo = dr["PalestraTitulo"].ToString();
                objPalestra.PalestraSubTitulo = dr["PalestraSubTitulo"].ToString();
                objPalestra.PalestraSinopseP1 = dr["PalestraSinopseP1"].ToString();
                try { objPalestra.PalestraSinopseP2 = dr["PalestraSinopseP2"].ToString(); } catch { objPalestra.PalestraSinopseP4 = null; }
                try { objPalestra.PalestraSinopseP3 = dr["PalestraSinopseP3"].ToString(); } catch { objPalestra.PalestraSinopseP4 = null; }
                try { objPalestra.PalestraSinopseP4 = dr["PalestraSinopseP4"].ToString(); } catch { objPalestra.PalestraSinopseP4 = null; }
                objPalestra.PalestraDuracao = dr["PalestraDuracao"].ToString();
                objPalestra.PalestraData = Convert.ToDateTime(dr["PalestraData"]);
                objPalestra.PalestraAutoriza = Convert.ToBoolean(dr["PalestraAutoriza"]);
            }
            conn.Close();
            return objPalestra;
        }
        public void AutorizarPalestra(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Palestras SET PalestraAprovada = 'true' WHERE IDPalestra = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void NegarPalestra(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Palestras SET PalestraAprovada = 'false' WHERE IDPalestra = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public DataTable ListarPalestras()
        {            
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "select Palestras.IDPalestra, p.Username as Palestrante, c.Username as Criador, PalestraDtCriacao, PalestraLink, PalestraAutoriza, PalestraAprovada from Palestras join Users p on Palestras.IDPalestrante = p.UserId join Users c on Palestras.PalestraCriador = c.UserId";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            
            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public Palestra VerificarPalestrante(int id)
        {
            Palestra objPalestra = null;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Palestras WHERE IDPalestrante = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows && dr.Read())
            {
                objPalestra = new Palestra();
                objPalestra.IDPalestrante = Convert.ToInt32(dr["IDPalestrante"]);
            }
            conn.Close();
            return objPalestra;
        }
        public void ExcluirPalestra(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM PAlestras WHERE IDPalestra = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
