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
    public class CertificadoDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;

        public List<Certificados> HistoricoCertificados(int id)
        {
            List<Certificados> listarCertificados = new List<Certificados>();

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "select IDCertificado, Eventos.EventoTitulo, Palestras.PalestraTitulo, Certificados.DtInicio, Certificados.DtFinal, Certificados.Progresso, Certificados.Alvo from Certificados LEFT JOIN Eventos ON Eventos.IDEvento = Certificados.IDEvento LEFT JOIN Palestras ON Palestras.IDPalestra = Certificados.IDPalestra WHERE Certificados.UserId = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            
            SqlDataReader dr = cmd.ExecuteReader();

            TimeSpan myTimeSpan = new TimeSpan();
            TimeSpan myTimeSpan1 = new TimeSpan();

            if (dr.HasRows)
            {
                Certificados cc;
                while (dr.Read())
                {
                    try { myTimeSpan = ((SqlDataReader)dr).GetTimeSpan(dr.GetOrdinal("Progresso")); } catch { myTimeSpan = TimeSpan.Zero; }
                    string b = myTimeSpan.ToString();
                    string[] bb = b.Split(':', '.');
                    int w = Convert.ToInt32(bb[0]);
                    int ww = Convert.ToInt32(bb[1]);
                    int www = Convert.ToInt32(bb[2]);
                    TimeSpan x = new TimeSpan(w, ww, www);
                    try { myTimeSpan1 = ((SqlDataReader)dr).GetTimeSpan(dr.GetOrdinal("Alvo")); } catch { myTimeSpan1 = TimeSpan.Zero; }
                    string c = myTimeSpan1.ToString();
                    string[] ccc = c.Split(':', '.');
                    int y = Convert.ToInt32(ccc[0]);
                    int yy = Convert.ToInt32(ccc[1]);
                    int yyy = Convert.ToInt32(ccc[2]);
                    TimeSpan z = new TimeSpan(y, yy, yyy);
                    var ss = Math.Ceiling(((double)x.Ticks / (double)z.Ticks ) * 100 );
                    cc = new Certificados();
                    string ev = dr["EventoTitulo"].ToString();
                    if (ev == string.Empty)
                    {
                        cc.EventoTitulo = "-";
                    }
                    else
                    {
                        cc.EventoTitulo = dr["EventoTitulo"].ToString();
                    }
                    cc.PalestraTitulo = dr["PalestraTitulo"].ToString();
                    DateTime p = Convert.ToDateTime(dr["DtInicio"]);
                    cc.Data1 = p.ToString("dd/MM/yyyy");
                    DateTime r = Convert.ToDateTime(dr["DtFinal"]);
                    cc.Data2 = r.ToString("dd/MM/yyyy");
                    if (ss < 0)
                    {
                        cc.Porcentagem = "0 %";
                    }
                    else if(ss > 100)
                    {
                        cc.Porcentagem = "100 %";
                    }
                    else
                    {
                        if (ss > 79)
                        {
                            int idd = Convert.ToInt32(dr["IDCertificado"]);
                            CertificadoDAL cDAL = new CertificadoDAL();
                            cDAL.CertificadoConcluido(idd);
                        }                          

                        cc.Porcentagem = ss.ToString() + " %";
                    }

                    listarCertificados.Add(cc);
                }
            }
            conn.Close();

            return listarCertificados;
        }
        public DataTable ListarCertificados(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT Certificados.IDCertificado, Eventos.EventoTitulo, Palestras.PalestraTitulo, convert(varchar(10), Certificados.DtInicio, 103) AS DtInicio, convert(varchar(10), Certificados.DtFinal, 103) AS DtFinal, Certificados.Finalizado FROM Certificados " +
                         "LEFT JOIN Eventos ON Certificados.IDEvento = Eventos.IDEvento LEFT JOIN Palestras ON Certificados.IDPalestra = Palestras.IDPalestra WHERE Certificados.UserId = @id AND Certificados.Finalizado = 'true'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public Certificados ConsultarCertificado(int idUser, int idPalestra)
        {
            Certificados objCert = null;
            TimeSpan myTimeSpan = new TimeSpan();
            TimeSpan myTimeSpan1 = new TimeSpan();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT IDCertificado, UserId, IDPalestra, IDEvento, DtInicio, DtFinal, Finalizado, Progresso, Alvo FROM Certificados WHERE UserId = @usr AND IDPalestra = @pal";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usr", idUser);
            cmd.Parameters.AddWithValue("@pal", idPalestra);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows && dr.Read())
            {
                try { myTimeSpan = ((SqlDataReader)dr).GetTimeSpan(dr.GetOrdinal("Progresso")); } catch { myTimeSpan = TimeSpan.Zero; }
                try { myTimeSpan1 = ((SqlDataReader)dr).GetTimeSpan(dr.GetOrdinal("Alvo")); } catch { myTimeSpan1 = TimeSpan.Zero; }
                objCert = new Certificados();
                objCert.IDCertificado = Convert.ToInt32(dr["IDCertificado"]);
                objCert.UserId = Convert.ToInt32(dr["UserId"]);
                objCert.IDPalestra = Convert.ToInt32(dr["IDPalestra"]);
                try { objCert.IDEvento = Convert.ToInt32(dr["IDEvento"]); } catch { objCert.IDEvento = 0; }
                objCert.DtInicio = Convert.ToDateTime(dr["DtInicio"]);
                objCert.DtFinal = Convert.ToDateTime(dr["DtFinal"]);
                objCert.Finalizado = Convert.ToBoolean(dr["Finalizado"]);
                objCert.Progresso = new TimeSpan(myTimeSpan.Ticks);
                objCert.Alvo = new TimeSpan(myTimeSpan1.Ticks);
            }
            conn.Close();
            return objCert;
        }
        public void novoCertificadoEvent(int idUser, int idPalestra, DateTime time, int idEvent, TimeSpan aa)
        {
            TimeSpan a = TimeSpan.Zero;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "INSERT INTO Certificados(UserId, IDPalestra, IDEvento, DtInicio, DtFinal, Finalizado, Progresso, Alvo) VALUES(@usr,@pal,@event,@dt,@dt,@f,@p,@a)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usr", idUser);
            cmd.Parameters.AddWithValue("@pal", idPalestra);
            cmd.Parameters.AddWithValue("@event", idEvent);
            cmd.Parameters.AddWithValue("@dt", time);
            cmd.Parameters.AddWithValue("@f", false);
            cmd.Parameters.AddWithValue("@p", a);
            cmd.Parameters.AddWithValue("@a", aa);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void novoCertificado(int idUser, int idPalestra, DateTime time, TimeSpan aa)
        {
            TimeSpan a = TimeSpan.Zero;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "INSERT INTO Certificados(UserId, IDPalestra, DtInicio, DtFinal, Finalizado, Progresso, Alvo) VALUES(@usr,@pal,@dt,@dt,@f,@p,@a)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usr", idUser);
            cmd.Parameters.AddWithValue("@pal", idPalestra);
            cmd.Parameters.AddWithValue("@dt", time);
            cmd.Parameters.AddWithValue("@f", false);
            cmd.Parameters.AddWithValue("@p", a);
            cmd.Parameters.AddWithValue("@a", aa);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void CertificadoEmAndamento(int idCert, DateTime time)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "UPDATE Certificados SET DtFinal = @time WHERE IDCertificado = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@id", idCert);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void CertificadoEmAndamento1(int idCert, TimeSpan time)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "UPDATE Certificados SET Progresso = @time WHERE IDCertificado = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@time", time);
            cmd.Parameters.AddWithValue("@id", idCert);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void CertificadoConcluido(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "UPDATE Certificados SET Finalizado = 'true' WHERE IDCertificado = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public DataTable GetBackgroundCertificado()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Maintenance";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            conn.Close();
            return dt;
        }
        public Certificados ObterCertificado(int idCert)
        {
            Certificados ObterCert = null;
            TimeSpan myTimeSpan = new TimeSpan();
            TimeSpan myTimeSpan1 = new TimeSpan();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT IDCertificado, UserId, IDPalestra, IDEvento, DtInicio, DtFinal, Finalizado, Progresso, Alvo FROM Certificados WHERE IDCertificado = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", idCert);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows && dr.Read())
            {
                try { myTimeSpan = ((SqlDataReader)dr).GetTimeSpan(dr.GetOrdinal("Progresso")); } catch { myTimeSpan = TimeSpan.Zero; }
                try { myTimeSpan1 = ((SqlDataReader)dr).GetTimeSpan(dr.GetOrdinal("Alvo")); } catch { myTimeSpan1 = TimeSpan.Zero; }
                ObterCert = new Certificados();
                ObterCert.IDCertificado = Convert.ToInt32(dr["IDCertificado"]);
                ObterCert.UserId = Convert.ToInt32(dr["UserId"]);
                ObterCert.IDPalestra = Convert.ToInt32(dr["IDPalestra"]);
                ObterCert.DtInicio = Convert.ToDateTime(dr["DtInicio"]);
                ObterCert.DtFinal = Convert.ToDateTime(dr["DtFinal"]);
                ObterCert.Finalizado = Convert.ToBoolean(dr["Finalizado"]);
                ObterCert.Progresso = new TimeSpan(myTimeSpan.Ticks);
                ObterCert.Alvo = new TimeSpan(myTimeSpan1.Ticks);
            }
            conn.Close();
            return ObterCert;
        }
        public void AlterarBackgroundCertificado(Maintenance certificado)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Maintenance SET BackgroundCert = @img";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@img", certificado.BackgroundCert);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
