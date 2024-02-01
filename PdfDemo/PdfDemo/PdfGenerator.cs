using FontResolver;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace PdfDemo
{
    public class PdfGenerator
    {
        private Document _document;

        public PdfGenerator()
        {
            _document = new Document();
            CustomFontResolver customFontResolver = new CustomFontResolver();
            GlobalFontSettings.FontResolver = customFontResolver;
        }

        public void ProcessPdf()
        {
            // Create a MigraDoc section for the document
            Section documentSection = _document.AddSection();
            Header(documentSection);
            Body(documentSection);
            Footer(documentSection);

            // Save the combined document to a PDF file
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();
            pdfRenderer.Document = _document;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save("C:\\MyRepos\\PdfApp\\pdf\\GodOfWar.pdf");
        }

        public void Header(Section section)
        {
            // Header content using MigraDoc
            Paragraph headerParagraph = section.Headers.Primary.AddParagraph();
            headerParagraph.Format.Alignment = ParagraphAlignment.Center;

            // Add logo in the header
            string logoPath = "C:\\MyRepos\\PdfDemo\\PdfDemo\\img\\GodOfWar.jpg";
            MigraDoc.DocumentObjectModel.Shapes.Image logoImage = headerParagraph.AddImage(logoPath);
            logoImage.LockAspectRatio = true;
            logoImage.Height = "2cm"; // Adjust the size as needed

            // Add header text
            headerParagraph.AddLineBreak();
            headerParagraph.AddFormattedText("God of War Document", TextFormat.Bold);
        }

        public void Body(Section section)
        {

            Paragraph bodyParagraph = section.AddParagraph();
            for (int i = 0; i < 5; i++)
            {
                bodyParagraph.AddLineBreak();
            }

            bodyParagraph.AddText("Welcome to the God of War universe!");
            bodyParagraph.AddText("Experience the epic journey of Kratos and Atreus as they navigate the realms, face powerful gods, and uncover the truth about Kratos' past.");

            bodyParagraph.AddLineBreak();
            bodyParagraph.AddLineBreak();

            bodyParagraph.AddText("Key Features:");
            bodyParagraph.AddLineBreak();
            bodyParagraph.AddLineBreak();
            bodyParagraph.AddText("1. Engaging Story: Immerse yourself in a rich narrative filled with Greek mythology and personal struggles.");
            bodyParagraph.AddLineBreak();
            bodyParagraph.AddLineBreak();
            bodyParagraph.AddText("2. Stunning Graphics: Explore breathtaking landscapes and intricately designed characters in a visually stunning world.");
            bodyParagraph.AddLineBreak();
            bodyParagraph.AddLineBreak();
            bodyParagraph.AddText("3. Epic Battles: Unleash the power of Kratos' iconic Leviathan Axe and engage in intense, strategic combat against mythical foes.");
            bodyParagraph.AddLineBreak();
            bodyParagraph.AddLineBreak();
            bodyParagraph.AddText("4. Emotional Journey: Witness the evolving relationship between Kratos and Atreus, adding depth to the characters and the overall experience.");

            // Add more information as needed...

            // Add some space
            for (int i = 0; i < 5; i++)
            {
                bodyParagraph.AddLineBreak();
            }

            bodyParagraph.AddText("Don't miss out on the adventure of a lifetime. Join Kratos and Atreus in God of War!");

            // Add some space
            for (int i = 0; i < 5; i++)
            {
                bodyParagraph.AddLineBreak();
            }
        }

        public void Footer(Section section)
        {
            // Footer content using MigraDoc
            Paragraph footerParagraph = section.Footers.Primary.AddParagraph();
            // Generate QR code using QRCoder
            string websiteUrl = "https://www.playstation.com/en-us/god-of-war/";
            byte[] qrCodeBytes = GenerateQrCode(websiteUrl);

            // Add QR code image to the document
            MigraDoc.DocumentObjectModel.Shapes.Image qrCodeImage = section.AddImage(MigraDocFilenameFromByteArray(qrCodeBytes));
            qrCodeImage.LockAspectRatio = true;
            qrCodeImage.Width = "3cm";

            // Center the image within the available width
            qrCodeImage.Left = MigraDoc.DocumentObjectModel.Shapes.ShapePosition.Center;
            footerParagraph.Format.Alignment = ParagraphAlignment.Center;
            footerParagraph.AddText("© 2024 God of War. All rights reserved.");
        }

        private byte[] GenerateQrCode(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // Increase the size parameter (second parameter in GetGraphic) to make the QR code bigger
            Bitmap qrCodeImage = qrCode.GetGraphic(40, System.Drawing.Color.Black, System.Drawing.Color.White, (Bitmap)Bitmap.FromFile("C:\\MyRepos\\PdfDemo\\PdfDemo\\logo\\GodOfWarLogo.jpg"));

            using (MemoryStream stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }


        private string MigraDocFilenameFromByteArray(byte[] byteArray)
        {
            return "base64:" + Convert.ToBase64String(byteArray);
        }
    }
}
