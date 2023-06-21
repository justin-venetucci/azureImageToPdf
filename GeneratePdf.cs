using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

public static class GeneratePdf
{
    [FunctionName("GeneratePdf")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("GeneratePdf function processed a request.");

        // Read the base64 image data from the request body
        string imageData = new StreamReader(req.Body).ReadToEnd();
        
        log.LogInformation(imageData);

        // Convert the base64 image data to bytes
        byte[] imageBytes = System.Convert.FromBase64String(imageData);

        // Load the image into a PDF document
        PdfDocument document = new PdfDocument();
        XImage image = XImage.FromStream(() => new MemoryStream(imageBytes));
        PdfPage page = document.AddPage();
        page.Width = image.PixelWidth;
        page.Height = image.PixelHeight;
        XGraphics gfx = XGraphics.FromPdfPage(page);
        gfx.DrawImage(image, 0, 0);

        // Create a memory stream to store the PDF document
        MemoryStream stream = new MemoryStream();
        document.Save(stream);
        stream.Position = 0;

        // Return the PDF as the response
        return new FileStreamResult(stream, "application/pdf");
    }
}