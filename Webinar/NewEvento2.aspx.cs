using System;
using System.Diagnostics;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Webinar
{
    public partial class NewEvento2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected static string ReturnEncodedBase64UTF8(object rawImg)
        {
            string img = "data:image/gif;base64,{0}";
            byte[] toEncodeAsBytes = (byte[])rawImg;
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return String.Format(img, returnValue);
        }

        protected void btnteste_Click(object sender, EventArgs e)
        {
            label.Text = "e";
        }
    }
}