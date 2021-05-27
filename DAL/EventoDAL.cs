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
            conn.Close();

            string sql = "INSERT INTO Eventos(IDAdm,EventoTitulo,EventoSubTitulo,EventoSinopseP1,EventoSinopseP2,EventoDtIni,EventoDtTer,EventoCapa) VALUES(@adm,@t,@st,@p1,@p2,@dtini,@dtter,@capa)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@adm", objEvento.IDAdm);
            cmd.Parameters.AddWithValue("@t", objEvento.EventoTitulo);
            cmd.Parameters.AddWithValue("@st", objEvento.EventoSubTitulo);
            cmd.Parameters.AddWithValue("@p1", objEvento.EventoSinopseP1);
            cmd.Parameters.AddWithValue("@p2", objEvento.EventoSinopseP2);
            cmd.Parameters.AddWithValue("@dtini", objEvento.EventoDtIni);
            cmd.Parameters.AddWithValue("@dtter", objEvento.EventoDtTer);
            cmd.Parameters.AddWithValue("@capa", objEvento.EventoCapa);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public DataTable ListarEventos()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Eventos.IDEvento, Users.Username, Eventos.EventoTitulo, Eventos.EventoSubTitulo, convert(varchar(10), Eventos.EventoDtIni, 103), convert(varchar(10), Eventos.EventoDtTer, 103) FROM Eventos LEFT JOIN Users ON Eventos.IDAdm = Users.UserId";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }

    }
}
