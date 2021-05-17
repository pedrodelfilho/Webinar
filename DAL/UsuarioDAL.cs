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

    public class UsuarioDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;

   
        public Usuario VerificarUsuarios(string usr)
        {
            Usuario usuario = null;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT * FROM Users WHERE Email = @usr";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usr", usr);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                usuario = new Usuario();
                usuario.Username = dr["Username"].ToString();
                usuario.Password = dr["Password"].ToString();
                usuario.Email = dr["Email"].ToString();
                usuario.Tipo = dr["Tipo"].ToString();
            }
            conn.Close();
            return usuario;
        }
        public Usuario BuscarID(string mail)
        {
            Usuario usuario = null;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT * FROM Users WHERE Email = @mail";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@mail", mail);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                usuario = new Usuario();
                usuario.UserId = Convert.ToInt32(dr["UserId"]);
            }
            conn.Close();
            return usuario;
        }
        public Usuario BuscarCODE(int id)
        {
            Usuario usuario = null;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT ActivationCode FROM UserActivation WHERE UserId = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                usuario = new Usuario();
                usuario.ActivationCode = dr["ActivationCode"].ToString();
            }
            conn.Close();
            return usuario;
        }
        public void InserirConvidado(Convidado objConvidado)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "INSERT INTO Convidados(UserId) Values(@id)";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", objConvidado.UserId);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public Usuario ObterUsuario(int cod)
        {
            Usuario objUsuario = null;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT Users.Username, Users.Email FROM Users INNER JOIN Convidados ON Users.UserId = @cod AND Convidados.UserId = @cod";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cod", cod);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objUsuario = new Usuario();
                objUsuario.Username = dr["Username"].ToString();
                objUsuario.Email = dr["Email"].ToString();
            }
            conn.Close();
            return objUsuario;
        }
        public Convidado ObterConvidado(int cod)
        {
            Convidado objConvidado = null;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT Convidados.FotoConvidado, Convidados.SexoConvidado, Convidados.EscolaridadeConvidado, Convidados.ConvidadoBioP1, Convidados.ConvidadoDtNasc, Convidados.ConvidadoCidadeUF, Convidados.ConvidadoReceberEmail FROM Convidados INNER JOIN Users ON Users.UserId = @cod AND Convidados.UserId = @cod";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cod", cod);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objConvidado = new Convidado();
                try { objConvidado.FotoConvidado = (byte[])dr["FotoConvidado"]; } catch { objConvidado.FotoConvidado = null; }
                try { objConvidado.SexoConvidado = Convert.ToChar(dr["objConvidado.SexoConvidado"]); } catch { objConvidado.SexoConvidado = ' '; }
                try { objConvidado.EscolaridadeConvidado = dr["EscolaridadeConvidado"].ToString(); } catch { objConvidado.EscolaridadeConvidado = null; }
                try { objConvidado.ConvidadoBioP1 = dr["ConvidadoBioP1"].ToString(); } catch { objConvidado.ConvidadoBioP1 = null; }
                try { objConvidado.EscolaridadeConvidado = dr["EscolaridadeConvidado"].ToString(); } catch { objConvidado.EscolaridadeConvidado = null; }
                try { objConvidado.ConvidadoDtNasc = dr["ConvidadoDtNasc"].ToString(); } catch { objConvidado.ConvidadoDtNasc = "01/01/2000"; }
                try { objConvidado.ConvidadoCidadeUF = dr["ConvidadoCidadeUF"].ToString(); } catch { objConvidado.ConvidadoCidadeUF = null; }
                try { objConvidado.ConvidadoReceberEmail = Convert.ToBoolean(dr["ConvidadoReceberEmail"]);} catch { objConvidado.ConvidadoReceberEmail = false; }
            }
            conn.Close();
            return objConvidado;
        }
        public void InserirConvidadoAtualizado(Convidado objConvidado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "UPDATE Convidados SET FotoConvidado = @foto, SexoConvidado = @sexo, ConvidadoBioP1 = @bio, ConvidadoDtNasc = @dt, EscolaridadeConvidado = @nv, ConvidadoCidadeUF = @city, ConvidadoReceberEmail = @recebe WHERE UserId = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@foto", objConvidado.FotoConvidado);
            cmd.Parameters.AddWithValue("@sexo", objConvidado.SexoConvidado);
            cmd.Parameters.AddWithValue("@bio", objConvidado.ConvidadoBioP1);
            cmd.Parameters.AddWithValue("@dt", objConvidado.ConvidadoDtNasc);
            cmd.Parameters.AddWithValue("@nv", objConvidado.EscolaridadeConvidado);
            cmd.Parameters.AddWithValue("@city", objConvidado.ConvidadoCidadeUF);
            cmd.Parameters.AddWithValue("@recebe", objConvidado.ConvidadoReceberEmail);
            cmd.Parameters.AddWithValue("id", objConvidado.UserId);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
    
    }
}
