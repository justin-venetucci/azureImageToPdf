# GeneratePdf Azure Function

This Azure Function written in C# allows you to generate a PDF document from base64-encoded image data. The PDF canvas will be the exact same size as the input image. It takes a POST request containing the image data and returns the generated PDF as the response.

## Prerequisites

To use this Azure Function, you need the following:

- An Azure account with access to Azure Functions.
- A development environment with the necessary tools to develop and deploy Azure Functions.
- Knowledge of C# and Azure Functions.

## Functionality

The `GeneratePdf` function performs the following steps:

1. Receives a POST request with base64-encoded image data.
2. Reads the image data from the request body.
3. Converts the base64 image data to bytes.
4. Creates a new PDF document.
5. Loads the image into the PDF document.
6. Saves the PDF document to a memory stream.
7. Returns the PDF document as the response.

## Usage

Follow these steps to use the `GeneratePdf` Azure Function:

1. Deploy the Azure Function to your Azure environment using your preferred deployment method.
2. Make a POST request to the function endpoint with raw base64-encoded image data (not JSON)
3. The function will process the request and generate a PDF document from the image data.
4. The generated PDF document will be returned as the response.

## Dependencies

The GeneratePdf Azure Function has the following dependencies:

- Microsoft.AspNetCore.Mvc
- Microsoft.Azure.WebJobs
- Microsoft.Azure.WebJobs.Extensions.Http
- Microsoft.AspNetCore.Http
- Microsoft.Extensions.Logging
- PdfSharpCore.Drawing
- PdfSharpCore.Pdf

Make sure these dependencies are installed and properly referenced in your Azure Function project.

## Limitations
- The function does not perform any validation or error handling related to the input data.
