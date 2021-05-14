using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS.UTILITIES.CreatePDF
{
    public class ConvertIntoPDF
    {
        public static string CreateTextPdf(string Date,string RefNo,string PDFBody,string LogoImage,string CreatorSignature,string PDFName)
        {
            try
            {
                var Path = HttpContext.Current.Server.MapPath("~/Upload ProjectData/NoticePDF/");
                var FilePath = Path + PDFName+".pdf";
                Document pdoc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdoc, new FileStream(Path + PDFName + ".pdf", FileMode.Create));
                pdoc.Open();

                System.Drawing.Image PiMage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(LogoImage));
                iTextSharp.text.Image itextimage = iTextSharp.text.Image.GetInstance(PiMage, System.Drawing.Imaging.ImageFormat.Png);
                itextimage.Alignment = Element.ALIGN_CENTER;
                itextimage.ScaleAbsolute(700f , 130f);
                pdoc.Add(itextimage);

                Paragraph paragrapg2 = new Paragraph(RefNo);
                paragrapg2.Alignment = Element.ALIGN_LEFT;
                pdoc.Add(paragrapg2);

                Paragraph paragrapg3 = new Paragraph(Date.Substring(0,10));
                paragrapg3.Alignment = Element.ALIGN_RIGHT;
                pdoc.Add(paragrapg3);

                Paragraph paragrapg4 = new Paragraph(20, PDFName);
                paragrapg4.Alignment = Element.ALIGN_CENTER;
                pdoc.Add(paragrapg4);

                Paragraph paragrapg1 = new Paragraph(PDFBody);
                paragrapg1.Alignment = Element.ALIGN_LEFT;
                pdoc.Add(paragrapg1);

                System.Drawing.Image PiMage2 = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(CreatorSignature));
                iTextSharp.text.Image itextimage2 = iTextSharp.text.Image.GetInstance(PiMage2, System.Drawing.Imaging.ImageFormat.Png);
                itextimage2.Alignment = Element.ALIGN_RIGHT;
                itextimage2.ScaleAbsolute(130f, 30f);
                pdoc.Add(itextimage2);

                Paragraph paragrapg5 = new Paragraph(20, "Controller Of Examination");
                paragrapg5.Alignment = Element.ALIGN_RIGHT;
                pdoc.Add(paragrapg5);


                pdoc.Close();
                return "~/Upload ProjectData/NoticePDF/" + PDFName+".pdf";
            }
            catch (Exception ex)
            {
                LogManager.LogManager.ErrorEntry(ex);
                throw;
            }
        }

        public static string CreateVisualPdf(string PDFHeader, string HeaderImage, string BodyImage, string SignatureImage, string PDFName)
        {
            try
            {
                var Path = HttpContext.Current.Server.MapPath("~/Upload ProjectData/NoticePDF/");
                var FilePath = Path + PDFName + ".pdf";
                Document pdoc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdoc, new FileStream(Path + PDFName + ".pdf", FileMode.Create));
                pdoc.Open();

               /* if((HeaderImage.Split(new char[] { '.' }).ToString().ToLower() == "png"))
                {*/
                    System.Drawing.Image PiMage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(HeaderImage));
                    iTextSharp.text.Image itextimage = iTextSharp.text.Image.GetInstance(PiMage, System.Drawing.Imaging.ImageFormat.Png);
                    itextimage.Alignment = Element.ALIGN_CENTER;
                    itextimage.ScaleAbsolute(550f, 230f);
                    pdoc.Add(itextimage);

                    Paragraph paragrapg3 = new Paragraph(DateTime.Now.Date.ToString());
                    paragrapg3.Alignment = Element.ALIGN_RIGHT;
                    pdoc.Add(paragrapg3);

                    Paragraph paragrapg2 = new Paragraph("NOTICE");
                    paragrapg2.Alignment = Element.ALIGN_CENTER;
                    pdoc.Add(paragrapg2);
                    
                    Paragraph paragrapg1 = new Paragraph(PDFHeader);
                    paragrapg1.Alignment = Element.ALIGN_CENTER;
                    pdoc.Add(paragrapg1);


                    System.Drawing.Image PiMage1 = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(BodyImage));
                    iTextSharp.text.Image itextimage1 = iTextSharp.text.Image.GetInstance(PiMage1, System.Drawing.Imaging.ImageFormat.Png);
                    itextimage1.Alignment = Element.ALIGN_CENTER;
                    pdoc.Add(itextimage1);

                    System.Drawing.Image PiMage2 = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(SignatureImage));
                    iTextSharp.text.Image itextimage2 = iTextSharp.text.Image.GetInstance(PiMage2, System.Drawing.Imaging.ImageFormat.Png);
                    itextimage2.Alignment = Element.ALIGN_CENTER;
                    pdoc.Add(itextimage2);

                    /*Paragraph paragrapg1 = new Paragraph("This Is Paragraph");
                    paragrapg1.Alignment = Element.ALIGN_CENTER;
                    pdoc.Add(paragrapg1);

                    Paragraph paragrapg2 = new Paragraph("This Is Paragraph2");
                    paragrapg2.Alignment = Element.ALIGN_CENTER;
                    pdoc.Add(paragrapg2);*/
                    pdoc.Close();
                    return "~/Upload ProjectData/NoticePDF/" + PDFName;
               /* }
                else if()*/

            }
            catch (Exception ex)
            {
                LogManager.LogManager.ErrorEntry(ex);
                throw;
            }
        }

        public static string CreateExamPaperPdf(HttpPostedFileBase[] data , string PDFName, string SavePath)
        {
            try
            {
                var Path = HttpContext.Current.Server.MapPath(SavePath);
                var FilePath = Path + PDFName + ".pdf";
                Document pdoc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdoc, new FileStream(Path + PDFName + ".pdf", FileMode.Create));
                pdoc.Open();

                foreach(var item in data)
                {
                    string path = ConfigurationManager.AppSettings["TempImageUploadPath"].ToString();
                    string FIleName = string.Concat(path, item.FileName);
                    item.SaveAs(System.Web.HttpContext.Current.Server.MapPath(FIleName));
                    System.Drawing.Image PiMage1 = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(FIleName));
                    iTextSharp.text.Image itextimage1 = iTextSharp.text.Image.GetInstance(PiMage1, System.Drawing.Imaging.ImageFormat.Png);
                    itextimage1.Alignment = Element.ALIGN_CENTER;
                    pdoc.Add(itextimage1);
                }
                pdoc.Close();
                return SavePath + PDFName+".pdf";
            }
            catch (Exception ex)
            {
                LogManager.LogManager.ErrorEntry(ex);
                throw;
            }
        }
    }
}
