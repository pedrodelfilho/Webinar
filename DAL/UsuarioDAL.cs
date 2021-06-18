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
        public Usuario BuscarName(string nome)
        {
            Usuario usuario = null;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT * FROM Users WHERE Username = @cod";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cod", nome);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                usuario = new Usuario();
                usuario.Username = dr["Username"].ToString();
                usuario.Email = dr["Email"].ToString();
                usuario.Tipo = dr["Tipo"].ToString();

            }
            conn.Close();
            return usuario;
        }
        public Palestrante BuscarIDPalestrante(int id)
        {
            Palestrante palestrante = null;

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT * FROM Palestrantes WHERE IDPalestrante = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                palestrante = new Palestrante();
                palestrante.IDPalestrante = Convert.ToInt32(dr["IDPalestrante"]);
                palestrante.PalestranteFoto = (byte[])dr["PalestranteFoto"];
                palestrante.PalestranteCidadeUF = dr["PalestranteCidadeUF"].ToString();
                palestrante.PalestranteDtNasc = Convert.ToDateTime(dr["PalestranteDtNasc"]);
                palestrante.PalestranteEspecialidade = dr["PalestranteEspecialidade"].ToString();
                palestrante.PalestranteBioP1 = dr["PalestranteBioP1"].ToString();
                palestrante.PalestranteBioP2 = dr["PalestranteBioP2"].ToString();
                palestrante.PalestranteTwiter = dr["PalestranteTwiter"].ToString();
                palestrante.PalestranteFacebook = dr["PalestranteFacebook"].ToString();
                palestrante.PalestranteGoogle = dr["PalestranteGoogle"].ToString();
                palestrante.PalestranteLinkedin = dr["PalestranteLinkedin"].ToString();
            }
            conn.Close();
            return palestrante;
        }
        public Usuario BuscarEmail(int id)
        {
            Usuario usuario = null;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Users WHERE UserId = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                usuario = new Usuario();
                usuario.Email = dr["Email"].ToString();
                usuario.Username = dr["Username"].ToString();
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
                usuario.Password = dr["Password"].ToString();
                usuario.UserId = Convert.ToInt32(dr["UserId"]);
                usuario.Tipo = dr["Tipo"].ToString();
                usuario.Username = dr["Username"].ToString();
                usuario.Email = dr["Email"].ToString();
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

            string sql = "INSERT INTO Convidados(IDConvidados) Values(@id)";
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

            string sql = "SELECT * FROM Users WHERE UserId = @cod";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cod", cod);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objUsuario = new Usuario();
                objUsuario.Username = dr["Username"].ToString();
                objUsuario.Email = dr["Email"].ToString();
                objUsuario.Tipo = dr["Tipo"].ToString();

            }
            conn.Close();
            return objUsuario;
        }
        public Convidado ObterConvidado(int cod)
        {
            Convidado objConvidado = null;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT * FROM Convidados WHERE IDConvidados = @cod";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cod", cod);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objConvidado = new Convidado();
                try { objConvidado.FotoConvidado = (byte[])dr["FotoConvidado"]; } catch { objConvidado.FotoConvidado = null; }
                try { objConvidado.SexoConvidado = Convert.ToChar(dr["SexoConvidado"]); } catch { objConvidado.SexoConvidado = ' '; }
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
        public Moderador ObterModerador(int id)
        {
            Moderador objModerador = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Moderadores WHERE IDModerador = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objModerador = new Moderador();
                objModerador.IDModerador = Convert.ToInt32(dr["IDModerador"]);
            }
            conn.Close();
            return objModerador;
        }
        public Administrador ObterAdministrador(int id)
        {
            Administrador objAdministrador = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Administradores WHERE IDAdministrador = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objAdministrador = new Administrador();
                objAdministrador.IDAdministrador = Convert.ToInt32(dr["IDAdministrador"]);
            }
            conn.Close();
            return objAdministrador;
        }
        public void InserirConvidadoAtualizado(Convidado objConvidado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "UPDATE Convidados SET ";
            string res = "SexoConvidado = @sexo, ConvidadoBioP1 = @bio, ConvidadoDtNasc = @dt, EscolaridadeConvidado = @nv, ConvidadoCidadeUF = @city, ConvidadoReceberEmail = @recebe WHERE IDConvidados = @id";

            if (objConvidado.FotoConvidado != null)
            {
                sql += "FotoConvidado = @foto, " + res;
            }
            else { sql += res; }

            SqlCommand cmd = new SqlCommand(sql, conn);
            if (objConvidado.FotoConvidado != null)
            {
                cmd.Parameters.AddWithValue("@foto", objConvidado.FotoConvidado);
            }
            
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
        public Palestrante ObterNovoPalestrante(int cod)
        {
            Palestrante objPalestrante = null;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Palestrantes WHERE IDPalestrante = @cod";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cod", cod);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows && dr.Read())
            {
                objPalestrante = new Palestrante();
                objPalestrante.IDPalestrante = Convert.ToInt32(dr["IDPalestrante"]);
            }
            conn.Close();
            return objPalestrante;
        }
        public Palestrante ObterPalestrante(int cod)
        {
            Palestrante objPalestrante = null;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string sql = "SELECT * FROM Palestrantes WHERE IDPalestrante = @cod";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cod", cod);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objPalestrante = new Palestrante();
                try { objPalestrante.PalestranteFoto = (byte[])dr["PalestranteFoto"]; } catch { objPalestrante.PalestranteFoto = null; }
                try { objPalestrante.PalestranteDtNasc = Convert.ToDateTime(dr["PalestranteDtNasc"].ToString()); } catch { objPalestrante.PalestranteDtNasc = default; }
                try { objPalestrante.PalestranteCidadeUF = dr["PalestranteCidadeUF"].ToString(); } catch { objPalestrante.PalestranteCidadeUF = null; }
                try { objPalestrante.PalestranteSexo = Convert.ToChar(dr["PalestranteSexo"]); } catch { objPalestrante.PalestranteSexo = ' '; }
                try { objPalestrante.PalestranteFormacao = dr["PalestranteFormacao"].ToString(); } catch { objPalestrante.PalestranteFormacao = null; }
                try { objPalestrante.PalestranteEspecialidade = dr["PalestranteEspecialidade"].ToString(); } catch { objPalestrante.PalestranteEspecialidade = null; }
                try { objPalestrante.PalestranteBioP1 = dr["PalestranteBioP1"].ToString(); } catch { objPalestrante.PalestranteBioP1 = null; }
                try { objPalestrante.PalestranteBioP2 = dr["PalestranteBioP2"].ToString(); } catch { objPalestrante.PalestranteBioP2 = null; }
                try { objPalestrante.PalestranteReceberEmail = Convert.ToBoolean(dr["PalestranteReceberEmail"]); } catch { objPalestrante.PalestranteReceberEmail = false; }
                try { objPalestrante.PalestranteAutoriza = Convert.ToBoolean(dr["PalestranteAutoriza"]); } catch { objPalestrante.PalestranteAutoriza = false; }
                try { objPalestrante.PalestranteTwiter = dr["PalestranteTwiter"].ToString(); } catch { objPalestrante.PalestranteTwiter = null; }
                try { objPalestrante.PalestranteFacebook = dr["PalestranteFacebook"].ToString(); } catch { objPalestrante.PalestranteFacebook = null; }
                try { objPalestrante.PalestranteGoogle = dr["PalestranteGoogle"].ToString(); } catch { objPalestrante.PalestranteGoogle = null; }
                try { objPalestrante.PalestranteLinkedin = dr["PalestranteLinkedin"].ToString(); } catch { objPalestrante.PalestranteLinkedin = null; }

            }
            conn.Close();
            return objPalestrante;
        }        
        public void NegarPalestrante(int cod)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Palestrantes SET PerfilAprovado = 'false' WHERE IDPalestrante = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", cod);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void AutorizarPalestrante(int cod)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Palestrantes SET PerfilAprovado = 'true' WHERE IDPalestrante = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", cod);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void InserirPalestranteAtualizado(Palestrante objPalestrante)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Palestrantes SET ";
            string res = "PalestranteDtNasc = @dt, PalestranteCidadeUF = @city, PalestranteSexo = @sx, PalestranteFormacao = @fmc, PalestranteEspecialidade = @esp, PalestranteBioP1 = @p1, PalestranteBioP2 = @p2, PerfilAprovado = @apv, PalestranteReceberEmail = @mail, PalestranteAutoriza = @aut, PalestranteTwiter = @twt, PalestranteFacebook = @fac, PalestranteGoogle = @goo, PalestranteLinkedin = @in WHERE IDPalestrante = @id";

            if (objPalestrante.PalestranteFoto != null)
            {
                sql += "PalestranteFoto = @foto, " + res;
            }
            else { sql += res; }

            SqlCommand cmd = new SqlCommand(sql, conn);

            if (objPalestrante.PalestranteFoto != null) 
            {
                cmd.Parameters.AddWithValue("@foto", objPalestrante.PalestranteFoto);
            }
            
            cmd.Parameters.AddWithValue("@dt", objPalestrante.PalestranteDtNasc);
            cmd.Parameters.AddWithValue("@city", objPalestrante.PalestranteCidadeUF);
            cmd.Parameters.AddWithValue("sx", objPalestrante.PalestranteSexo);
            cmd.Parameters.AddWithValue("fmc", objPalestrante.PalestranteFormacao);
            cmd.Parameters.AddWithValue("esp", objPalestrante.PalestranteEspecialidade);
            cmd.Parameters.AddWithValue("@p1", objPalestrante.PalestranteBioP1);
            cmd.Parameters.AddWithValue("@p2", objPalestrante.PalestranteBioP2);
            cmd.Parameters.AddWithValue("@apv", objPalestrante.PerfilAprovado);
            cmd.Parameters.AddWithValue("@mail", objPalestrante.PalestranteReceberEmail);
            cmd.Parameters.AddWithValue("@aut", objPalestrante.PalestranteAutoriza);
            cmd.Parameters.AddWithValue("twt", objPalestrante.PalestranteTwiter);
            cmd.Parameters.AddWithValue("fac", objPalestrante.PalestranteFacebook);
            cmd.Parameters.AddWithValue("@goo", objPalestrante.PalestranteGoogle);
            cmd.Parameters.AddWithValue("@in", objPalestrante.PalestranteLinkedin);
            cmd.Parameters.AddWithValue("@id", objPalestrante.IDPalestrante);
            
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        public DataTable ListarUsuarios()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT UserId, Username, Password, Email, convert(varchar(10), CreatedDate, 103) AS CreatedDate, convert(varchar(10), LastLoginDate, 103) AS LastLoginDate, Tipo FROM Users";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            
            conn.Close();
            return dt;
        }
        public DataTable ListarPalestrantePendente()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT Users.UserId, Users.Username, Users.Email, IDPalestrante, PalestranteFoto, convert(varchar(10), PalestranteDtNasc, 103) AS PalestranteDtNasc, PalestranteCidadeUF, PalestranteSexo, PalestranteFormacao, PalestranteEspecialidade, PalestranteBioP1, PalestranteBioP2, PerfilAprovado, PalestranteReceberEmail, PalestranteAutoriza, PalestranteTwiter, PalestranteFacebook, PalestranteGoogle, PalestranteLinkedin, convert(varchar(10), CreatedDate, 103) AS CreatedDate, convert(varchar(10), LastLoginDate, 103) AS LastLoginDate FROM Palestrantes INNER JOIN Users ON Palestrantes.IDPalestrante=Users.UserId WHERE Palestrantes.PerfilAprovado IS NULL OR Palestrantes.PerfilAprovado = 'false'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }       
        public void ExcluirUsuario(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM Users WHERE UserId = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void ExcluirConvidado(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM Convidados WHERE IDConvidados = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void ExcluirModerador(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM Moderadores WHERE IDModerador = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }   
        public void ExcluirPalestrante(int id)
        {           
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM Palestrantes WHERE IDPalestrante = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void AlterarTipoConta(int id, string destino)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            if (destino == "Convidado")
            {
                string sql1 = "INSERT INTO Convidados(IDConvidados) VALUES(@id)";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();
            }
            if (destino == "Moderador")
            {
                string sql2 = "INSERT INTO Moderadores(IDModerador) VALUES(@id)";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@id", id);
                cmd2.ExecuteNonQuery();
            }
            if (destino == "Palestrante")
            {
                string sql3 = "INSERT INTO Palestrantes(IDPalestrante, PerfilAprovado) VALUES(@id, @s)";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@id", id);
                cmd3.Parameters.AddWithValue("@s", true);
                cmd3.ExecuteNonQuery();
            }
            if (destino == "Administrador")
            {
                string sql4 = "INSERT INTO Administradores(IDAdministrador) VALUES(@id)";
                SqlCommand cmd4 = new SqlCommand(sql4, conn);
                cmd4.Parameters.AddWithValue("@id", id);
                cmd4.ExecuteNonQuery();
            }
        }
        public void TipoDeConta(int id, string tipo)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Users SET Tipo = @tipo WHERE UserId = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@tipo", tipo);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        public void AtualizarNomeUsuario(Usuario objUsuario)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Users SET Username = @name WHERE UserId = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", objUsuario.Username);
            cmd.Parameters.AddWithValue("@id", objUsuario.UserId);
            cmd.ExecuteNonQuery();
        }
        public void AtualizarSenhaUsuario(int id, string pw)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Users SET Password = @pw WHERE UserId = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@pw", pw);
            cmd.ExecuteNonQuery();
        }    
        public DataTable PalestranteRandom()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Top 6 * FROM Palestrantes WHERE PerfilAprovado = 'true' AND PalestranteFoto IS NOT NULL ORDER BY NEWID()";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }   
    }
}
