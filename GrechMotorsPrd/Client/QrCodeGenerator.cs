using iTextSharp.text;
using iTextSharp.text.pdf;
using QRCoder;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp;
using ImageSharp = SixLabors.ImageSharp.Image;
using iTextImage = iTextSharp.text.Image;


public class QrCodeGenerator
{
    public KeyValuePair<string, ImageSharp> GenerateQrCode(string data)
    {
        try
        {
            // Generate QR code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);

            // Convert byte array to ImageSharp.Image
            ImageSharp qrCodeImage;
            using (var ms = new MemoryStream(qrCodeAsPngByteArr))
            {
                qrCodeImage = ImageSharp.Load(ms);
            }

            return new KeyValuePair<string, ImageSharp>(data, qrCodeImage);
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"An error occurred while generating the QR code for data '{data}': {ex}");
            return default;
        }
    }

    public Queue<string> ImagesToDataUrls(Queue<KeyValuePair<string, ImageSharp>> qrCodeImages)
    {
        try
        {
            var dataUrls = new Queue<string>();

            while (qrCodeImages.Count > 0)
            {
                var qrCode = qrCodeImages.Dequeue();
                var image = qrCode.Value;
                using (var ms = new MemoryStream())
                {
                    image.SaveAsPng(ms);
                    var base64Data = Convert.ToBase64String(ms.ToArray());
                    dataUrls.Enqueue($"data:image/png;base64,{base64Data}");
                }
            }

            return dataUrls;
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"An error occurred while converting QR code images to data URLs: {ex}");
            return default;
        }
    }


    public string SaveQrCodesToPdf(Queue<KeyValuePair<string, ImageSharp>> qrCodes, string fileName)
    {
        try
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                var pdfDoc = new iTextSharp.text.Document();
                var pdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();

                while (qrCodes.Count > 0)
                {
                    var qrCode = qrCodes.Dequeue();

                    // Convert ImageSharp.Image to iTextSharp.text.Image
                    using (var ms = new MemoryStream())
                    {
                        qrCode.Value.SaveAsPng(ms);
                        ms.Position = 0;
                        var pdfImage = iTextSharp.text.Image.GetInstance(ms.ToArray());

                        // Add the image to the PDF document
                        pdfDoc.Add(pdfImage);

                        // Add the data as text to the PDF document
                        var paragraph = new iTextSharp.text.Paragraph(qrCode.Key);
                        pdfDoc.Add(paragraph);
                    }
                
                }

                pdfDoc.Close();
            }

            // Return the file name
            return fileName;
        }
        catch (Exception ex)
        {
            // Log the exception and continue with the next QR code
            Console.WriteLine($"An error occurred while adding a QR code to the PDF: {ex}");
            return default;
        }
    }
}