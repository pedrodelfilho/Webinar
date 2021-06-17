using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Threading;
using DAL;
using Models;

namespace Webinar
{
    public partial class Player : System.Web.UI.Page
    {
        public static int idPalestra = 0;
        public static int idUser = 0;
        public static int idCert = 0;

        public void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string link = Session["link"].ToString();
                idUser = Convert.ToInt32(Session["idUser"]);

                idPalestra = Convert.ToInt32(Session["idPalestra"]);
                idReprodutor.Attributes.Add("src", link);

                CertificadoDAL cDAL = new CertificadoDAL();
                Certificados c = cDAL.ConsultarCertificado(idUser, idPalestra);

                if(c == null)
                {
                    novoCertificado(idPalestra, idUser);
                }
                else {
                    idCert = c.IDCertificado;
                    CertificadoEmAndamento(idCert); }
            }           
        }

        public void novoCertificado(int idPa, int idUs)
        {
            PalestraDAL pDAL = new PalestraDAL();
            Palestra objPalestra = pDAL.ObterPalestra(idPa);

            int idEv = objPalestra.IDEvento;
            DateTime time = DateTime.Now;

            TimeSpan Alvo = TimeSpan.Parse(objPalestra.PalestraDuracao);

            CertificadoDAL cDAL = new CertificadoDAL();
            if (idEv == 0)
            {
                cDAL.novoCertificado(idUs, idPa, time, Alvo);
            }
            else
            {
                cDAL.novoCertificadoEvent(idUs, idPa, time, idEv, Alvo);
            }            
        }
        public void CertificadoEmAndamento(int idCertificado)
        {
            idCert = idCertificado;
            DateTime time = DateTime.Now;
            CertificadoDAL cDAL = new CertificadoDAL();
            cDAL.CertificadoEmAndamento(idCert, time);
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            CertificadoDAL cDAL = new CertificadoDAL();
            Certificados objCert = cDAL.ConsultarCertificado(idUser, idPalestra);
            DateTime time1 = DateTime.Now;
            idCert = objCert.IDCertificado;
            
            TimeSpan tim = time1.Subtract(objCert.DtFinal);
            TimeSpan time = objCert.Progresso.Add(tim);
            cDAL.CertificadoEmAndamento1(idCert, time);

            Response.Redirect("Default.aspx");            
        }
    }
}