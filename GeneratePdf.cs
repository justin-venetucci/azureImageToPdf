using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using  Util;

public static class GeneratePdf
{
    [FunctionName("GeneratePdf")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("GeneratePdf function processed a request.");
        // Set pixel multiple variable which will be used to set page.Width and Height
        //For some reason 0.75 is the magic number that gets rid of the generated whitespace
        double pixelMultiple = 0.75;
        
        // Read the base64 image data from the request body
        string inputString = new StreamReader(req.Body).ReadToEnd();
        
        //log.LogInformation(inputString);

         string imageData = Util.Utils.RemoveDataTag(inputString);

        // Convert the base64 image data to bytes
        byte[] imageBytes = System.Convert.FromBase64String(imageData);

        // Load the image into a PDF document
        PdfDocument document = new PdfDocument();
        XImage image = XImage.FromStream(() => new MemoryStream(imageBytes));
        PdfPage page = document.AddPage();
        page.Width = image.PixelWidth*pixelMultiple;
        page.Height = image.PixelHeight*pixelMultiple;

        XGraphics gfx = XGraphics.FromPdfPage(page);
        gfx.DrawImage(image, 0, 0);

        // Create a memory stream to store the PDF document
        MemoryStream stream = new MemoryStream();
        document.Save(stream);
        stream.Position = 0;

        // Return the PDF as the response
        return new FileStreamResult(stream, "application/pdf");
    }

};