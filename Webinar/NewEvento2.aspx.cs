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
            string imageLoc = @"D:\Users\Pedro\Downloads\Model.png";
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Certificado";

            // Create an empty page
            PdfPage page = document.AddPage();
            page.Width = 1200;
            page.Height = 900;

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawImage(gfx, imageLoc, 0, 0, Convert.ToInt32(page.Width), Convert.ToInt32(page.Height));

            // Create a font
            XFont font = new XFont("Tahoma", 32, XFontStyle.BoldItalic);

            // Draw the text
            gfx.DrawString("Certificamos que Pedro Del Antonio FIlho", font, XBrushes.Black, new XRect(0, 300, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawString("concluiu com êxito 7 total horas do curso online", font, XBrushes.Black, new XRect(0, 330, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawString("Desenvolvimento WEB com Python e Django", font, XBrushes.Black, new XRect(0, 360, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawString("em 23 de Outubro de 2020.", font, XBrushes.Black, new XRect(0, 390, page.Width, page.Height), XStringFormats.TopCenter);
            

            // Save the document...
            const string filename = @"D:\Users\Pedro\Desktop\HelloWorld.pdf";
            document.Save(filename);

            // ...and start a viewer.
            Process.Start(filename);
        }
        void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(@"D:\Users\Pedro\Downloads\Model.png");
            gfx.DrawImage(image, x, y, width, height);
        }
    }
}