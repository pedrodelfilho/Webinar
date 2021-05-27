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
    }
}
