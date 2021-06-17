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

            string sql = "INSERT INTO Palestras(IDPalestrante,PalestraCriador,PalestraLink,PalestraDtCriacao,PalestraCapa,PalestraCategoria,PalestraTitulo,PalestraSubTitulo,PalestraSinopseP1,PalestraSinopseP2,PalestraSinopseP3,PalestraSinopseP4,PalestraDuracao,PalestraData,PalestraAprovada, PalestraAutoriza) VALUES(@idPal,@criador,@link,@dtcriar,@capa,@cat,@titulo,@sub,@p1,@p2,@p3,@p4,@time,@dt,@pa, @autoriza)";
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
            cmd.Parameters.AddWithValue("@pa", objPalestra.PalestraAprovada);
            cmd.Parameters.AddWithValue("@autoriza", objPalestra.PalestraAutoriza);
            cmd.ExecuteNonQuery();
            conn.Close();
        }     
        public DataTable ListarPalestrasPendetes()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Palestras.IDPalestra, Users.Username, convert(varchar(10), Palestras.PalestraDtCriacao, 103) AS PalestraDtCriacao, Palestras.PalestraAutoriza FROM Palestras LEFT JOIN Users ON Palestras.IDPalestrante = Users.UserId WHERE Palestras.PalestraAprovada = 'false'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public Palestra ObterPalestra(int id)
        {
            Palestra objPalestra = null;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT IDPalestra, IDPalestrante, PalestraCriador, PalestraLink, PalestraCapa, PalestraCategoria, PalestraTitulo, PalestraSubTitulo, PalestraSinopseP1, PalestraSinopseP2, PalestraSinopseP3, PalestraSinopseP4, PalestraDuracao, PalestraData, PalestraAutoriza, IDEvento, Acervo FROM Palestras WHERE IDPalestra = @id";
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
                try { objPalestra.IDEvento = Convert.ToInt32(dr["IDEvento"]); } catch { objPalestra.IDEvento = 0; }
                objPalestra.Acervo = Convert.ToBoolean(dr["Acervo"]);
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
        public DataTable PreencherSelect()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT PalestraTitulo FROM Palestras";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public DataTable AdicionarPalestraEmEvento(string titulo)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT Users.Username, Palestrantes.PalestranteFoto FROM Palestras LEFT JOIN Users ON Users.UserId = Palestras.IDPalestrante LEFT JOIN Palestrantes ON Palestrantes.IDPalestrante = Palestras.IDPalestrante WHERE Palestras.PalestraTitulo = @titulo";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@titulo", titulo);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public void AdicionarNoEvento(DateTime data, int id,  string titulo)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Palestras SET PalestraData = @data, IDEvento = @id WHERE PalestraTitulo = @titulo";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@titulo", titulo);
            cmd.Parameters.AddWithValue("@data", data);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public DataTable ListarPalestrasHome()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT * FROM Palestras WHERE Acervo = 'true'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public DataTable ObterPalestraEmEvento(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT Users.Username, Palestrantes.PalestranteFoto, Palestras.PalestraTitulo, Palestras.PalestraData, Palestras.IDPalestra FROM Palestras LEFT JOIN Users ON users.UserId = Palestras.IDPalestrante LEFT JOIN Palestrantes ON Palestrantes.IDPalestrante = Palestras.IDPalestrante WHERE IDEvento = @id ORDER BY PalestraData ASC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public DataTable ContarPalestras(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT CONVERT(VARCHAR(10), PalestraData, 103), COUNT(*) AS Quantidade FROM Palestras WHERE IDEvento = @id GROUP BY CONVERT(VARCHAR(10), PalestraData, 103)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public DataTable PreencherHomePalestraAcervo()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT * FROM Palestras WHERE Acervo = 'true' ORDER BY NEWID()";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public void AtualizarPalestra(Palestra objPalestra)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Palestras SET PalestraAprovada = 'true', IDPalestrante = @idPal, PalestraCriador = @criador, PalestraLink = @link, PalestraDtCriacao = @dtcriar, PalestraCapa = @capa, PalestraCategoria = @cat, PalestraTitulo = @titulo, PalestraSubTitulo = @sub, PalestraSinopseP1 = @p1, PalestraSinopseP2 = @p2, PalestraSinopseP3 = @p3, PalestraSinopseP4 = @p4, PalestraDuracao = @time, PalestraData = @dt, PalestraAutoriza = @autoriza WHERE IDPalestra = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", objPalestra.IDPalestra);
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
            cmd.Parameters.AddWithValue("@pa", objPalestra.PalestraAprovada);
            cmd.Parameters.AddWithValue("@autoriza", objPalestra.PalestraAutoriza);
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }
    }
}

            