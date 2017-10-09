using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iText.Forms;
using iText.Kernel.Pdf;
using iText.Forms.Fields;
using iText.Kernel.Geom;
using System.IO;

using Add_Blank_Signature_Field_To_Pdf.AppCode;

namespace Add_Blank_Signature_Field_To_Pdf
{
    class Program
    {
        static int Main(string[] args)
        {
            int Ret = -1;
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                // Values are available here
                if (File.Exists(options.srcFile))
                {
                    if (options.destFile == null) options.destFile = options.srcFile;

                    Ret = AddSignField(options.srcFile,options.destFile,options.x,options.y,options.width,options.height);
                }
            }
                        
            return Ret;
        }


        private static int AddSignField(String src, String dest,float x,float y,float width,float height)
        {
            int Ret = -1;
            //pour générer un nom de fichier improbable pour faire le swith
            string tempsuffixe = "###$$$¤¤¤¤@@@";
            try
            {
                bool rename = false;
                if (src == dest)
                {
                    rename = true;
                    dest = src + tempsuffixe;
                }

                PdfDocument pdfDoc = new PdfDocument(new PdfReader(src), new PdfWriter(dest));
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
                PdfSignatureFormField signF = PdfFormField.CreateSignature(pdfDoc, new Rectangle(x, y, width, height));
                signF.SetFieldName("SignatureDebtor");
                form.AddField(signF);
                pdfDoc.Close();

                if (rename)
                {
                    if (File.Exists(src))
                    {
                        int cpt = 1;
                        while (File.Exists(src.Replace(".pdf", "_old_" + cpt.ToString() + ".pdf")))
                        {
                            cpt += 1;
                        }
                        File.Move(src, src.Replace(".pdf", "_old_" + cpt.ToString() + ".pdf"));
                    }
                    File.Move(dest, src);
                }
                Ret = 0;
            }
            catch (Exception ex)
            {
                Ret = 1;
                Console.WriteLine("Une erreur est survenue : " + ex.Message);
                throw;
            }

            return Ret;
        }

    }
}
