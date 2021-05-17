using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class HomeDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["AggregateBD"].ConnectionString;

        public Home PreencherHome()
        {
            Home objHome = null;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Home WHERE IDHome = (SELECT MAX(IDHome) FROM Home)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objHome = new Home();
                objHome.Titulo = dr["Titulo"].ToString();
                objHome.TituloDestaque = dr["TituloDestaque"].ToString();
                objHome.SubTitulo = dr["SubTitulo"].ToString();
                objHome.LinkIntro = dr["LinkIntro"].ToString();
                objHome.QuemSomos = dr["QuemSomos"].ToString();
                objHome.Quando = dr["Quando"].ToString();
                objHome.Onde = dr["Onde"].ToString();
                objHome.Pergunta1 = dr["Pergunta1"].ToString();
                objHome.Responsta1 = dr["Resposta1"].ToString();
                objHome.Pergunta2 = dr["Pergunta2"].ToString();
                objHome.Responsta2 = dr["Resposta2"].ToString();
                objHome.Pergunta3 = dr["Pergunta3"].ToString();
                objHome.Responsta3 = dr["Resposta3"].ToString();
                objHome.Pergunta4 = dr["Pergunta4"].ToString();
                objHome.Responsta4 = dr["Resposta4"].ToString();
                objHome.Pergunta5 = dr["Pergunta5"].ToString();
                objHome.Responsta5 = dr["Resposta5"].ToString();
                objHome.Endereco = dr["Endereco"].ToString();
                objHome.Telefone = dr["Telefone"].ToString();
                objHome.Email = dr["Email"].ToString();
                objHome.EmailADM = dr["EmailADM"].ToString();
                
            }
            conn.Close();
            return objHome;
        }
        public void AplicarHome(Home objHome)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Home(UserId, Titulo, TituloDestaque, SubTitulo, LinkIntro, QuemSomos, Quando, Onde, Pergunta1, Resposta1, Pergunta2, Resposta2, Pergunta3, Resposta3, Pergunta4, Resposta4, Pergunta5, Resposta5, Endereco, Telefone, Email, EmailADM) VALUES(@id, @t, @td, @st, @l, @qs, @q, @o, @p1, @r1, @p2, @r2, @p3, @r3, @p4, @r4, @p5, @r5, @e, @tel, @mail, @mailADM)";
            
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", objHome.UserId);
            cmd.Parameters.AddWithValue("@t", objHome.Titulo);
            cmd.Parameters.AddWithValue("@td", objHome.TituloDestaque);
            cmd.Parameters.AddWithValue("@st", objHome.SubTitulo);
            cmd.Parameters.AddWithValue("@l", objHome.LinkIntro);
            cmd.Parameters.AddWithValue("@qs", objHome.QuemSomos);
            cmd.Parameters.AddWithValue("@q", objHome.Quando);
            cmd.Parameters.AddWithValue("@o", objHome.Onde);
            cmd.Parameters.AddWithValue("@p1", objHome.Pergunta1);
            cmd.Parameters.AddWithValue("@r1", objHome.Responsta1);
            cmd.Parameters.AddWithValue("@p2", objHome.Pergunta2);
            cmd.Parameters.AddWithValue("@r2", objHome.Responsta2);
            cmd.Parameters.AddWithValue("@p3", objHome.Pergunta3);
            cmd.Parameters.AddWithValue("@r3", objHome.Responsta3);
            cmd.Parameters.AddWithValue("@p4", objHome.Pergunta4);
            cmd.Parameters.AddWithValue("@r4", objHome.Responsta4);
            cmd.Parameters.AddWithValue("@p5", objHome.Pergunta5);
            cmd.Parameters.AddWithValue("@r5", objHome.Responsta5);
            cmd.Parameters.AddWithValue("@e", objHome.Endereco);
            cmd.Parameters.AddWithValue("@tel", objHome.Telefone);
            cmd.Parameters.AddWithValue("@mail", objHome.Email);
            cmd.Parameters.AddWithValue("@mailADM", objHome.EmailADM);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
            
    }
}


