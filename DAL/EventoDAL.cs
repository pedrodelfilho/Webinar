using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace DAL
{
    public class EventoDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;

        public void InserirEvento(Evento objEvento)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Eventos(IDAdm,EventoTitulo,EventoSubTitulo,EventoSinopseP1,EventoSinopseP2,EventoDtIni,EventoDtTer,EventoCapa, ModResponsavel) VALUES(@adm,@t,@st,@p1,@p2,@dtini,@dtter,@capa, @mod)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@adm", objEvento.IDAdm);
            cmd.Parameters.AddWithValue("@t", objEvento.EventoTitulo);
            cmd.Parameters.AddWithValue("@st", objEvento.EventoSubTitulo);
            cmd.Parameters.AddWithValue("@p1", objEvento.EventoSinopseP1);
            cmd.Parameters.AddWithValue("@p2", objEvento.EventoSinopseP2);
            cmd.Parameters.AddWithValue("@dtini", objEvento.EventoDtIni);
            cmd.Parameters.AddWithValue("@dtter", objEvento.EventoDtTer);
            cmd.Parameters.AddWithValue("@capa", objEvento.EventoCapa);
            cmd.Parameters.AddWithValue("@mod", objEvento.ModResponsavel);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public DataTable ListarEventos()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Eventos.IDEvento, Users.Username, Eventos.EventoTitulo, Eventos.EventoSubTitulo, convert(varchar(10), Eventos.EventoDtIni, 103) AS EventoDtIni, convert(varchar(10), Eventos.EventoDtTer, 103) AS EventoDtTer, Eventos.ModResponsavel FROM Eventos LEFT JOIN Users ON Eventos.IDAdm = Users.UserId";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public DataTable PalestraTitulo()
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
        public Evento ObterEvento(string titulo)
        {
            Evento evento = null;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Eventos WHERE EventoTitulo = @titulo";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@titulo", titulo);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                evento = new Evento();
                evento.EventoTitulo = dr["EventoTitulo"].ToString();
                evento.IDEvento = Convert.ToInt32(dr["IDEvento"]);
                evento.EventoSinopseP1 = dr["EventoSinopseP1"].ToString();
                evento.EventoSinopseP2 = dr["eventoSinopseP2"].ToString();
                evento.EventoDtIni = Convert.ToDateTime(dr["EventoDtIni"]);
                evento.EventoDtTer = Convert.ToDateTime(dr["EventoDtTer"]);
                evento.EventoCapa = (byte[])dr["EventoCapa"];
                evento.EventoSubTitulo = dr["EventoSubTitulo"].ToString();
            }
            conn.Close();
            return evento;
        }    
        public DataTable EventosHome()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Eventos";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }       
        public void SendLinkEvento(int idReq, int idEvent, int idPales)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO SendLinkEvento(IDRequerente, IDEvento, IDPalestra) Values(@idReq, @idEvent, @idPales)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idReq", idReq);
            cmd.Parameters.AddWithValue("@idEvent", idEvent);
            cmd.Parameters.AddWithValue("@idpales", idPales);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public DataTable VerifySendLinkEvento(int idReq, int idEvent, int idPales)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT * FROM SendLinkEvento WHERE IDRequerente = @idReq AND IDEvento = @idEvent AND IDPalestra = @idPales";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idReq", idReq);
            cmd.Parameters.AddWithValue("@idEvent", idEvent);
            cmd.Parameters.AddWithValue("@idpales", idPales);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public DataTable PreencherHomeEventoAcervo()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT * FROM Eventos WHERE Acervo = true";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
    }
}
