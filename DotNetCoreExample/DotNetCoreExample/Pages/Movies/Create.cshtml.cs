using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DotNetCoreExample.Data;
using DotNetCoreExample.Models;
using System.Drawing;
using Tesseract;

namespace DotNetCoreExample.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly DotNetCoreExample.Data.DotNetCoreExampleContext _context;

        public CreateModel(DotNetCoreExample.Data.DotNetCoreExampleContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //Read image text start

            //Bitmap image = new Bitmap("C:\\Users\\NILAM\\Downloads\\FB_IMG_1678114188022.jpg");
            TesseractEngine engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
            Tesseract.Page page = engine.Process(Pix.LoadFromFile(@"E:\NeilGaimanQuote.png"));//life-quotes-wayne-dyer-1665420549.png"));
            string text = page.GetText();

            //Read image text end
            
            //var client = ImageAnnotatorClient.Create();
            //var image = Google.Cloud.Vision.V1.Image.FromFile("C:\\Users\\NILAM\\Downloads\\FB_IMG_1678114188022.jpg");

            //var response = client.DetectDocumentText(image);
            //var test = response.Text;

            //var ocrEngine = OcrEngine.TryCreateFromLanguage(new Language("en-US"));

            //using (var imageStream = await imageFile.OpenAsync(FileAccessMode.Read))
            //{
            //    var ocrResult = await ocrEngine.RecognizeAsync(imageStream);

            //    return ocrResult.Text;
            //}

            //var cloudOcr = new CloudOcrSdk("<your-app-id>", "<your-app-password>");
            //var task = cloudOcr.ProcessImageAsync(@"C:\path\to\image.jpg");

            //task.Wait();

            //if (task.Result.Status == ProcessingStatus.Completed)
            //{
            //    return task.Result.Result;
            //}
            //else
            //{
            //    throw new Exception("OCR failed.");
            //}
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
