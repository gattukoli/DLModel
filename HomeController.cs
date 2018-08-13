using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult y_ui_dataentry_form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult y_ui_dataentry_form(string batchid)
        {
            string path = @"c:\temp\test.png";
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            string imageOutputURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

            ViewBag.IndexImage = imageOutputURL;
            return View();
        }

        private string SaveUploadedFiles(string servicename, ICollection<IFormFile> files)
        {
            string imageInputURL = "";
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        System.IO.File.WriteAllBytes(@".\Images\" + servicename + @"\" + file.FileName, fileBytes);
                        string imageBase64InputData = Convert.ToBase64String(fileBytes);
                        imageInputURL = string.Format("data:image/jpg;base64,{0}", imageBase64InputData);
                    }
                }
            }

            return imageInputURL;
        }

        public IActionResult y_ui_pdf_conversion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult y_ui_pdf_conversion(ICollection<IFormFile> files)
        {
            ViewBag.InputImage = SaveUploadedFiles("ConvertToPDF", files);            

            //Task.WaitAll(Task.Delay(10000));                        

            string path = @"c:\temp\test.pdf";
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            string imageOutputURL = string.Format("data:application/pdf;base64,{0}", imageBase64Data);

            
            ViewBag.OutputImage = imageOutputURL;
            return View();
        }


        public IActionResult TetPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TetPage(ICollection<IFormFile> files)
        {
            string imageInputURL = "";
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        System.IO.File.WriteAllBytes(@".\Images\" + file.FileName, fileBytes);                                                
                        string imageBase64InputData = Convert.ToBase64String(fileBytes);
                        imageInputURL = string.Format("data:image/jpg;base64,{0}", imageBase64InputData);
                    }
                }
            }

            //Task.WaitAll(Task.Delay(10000));                        

            string path = @"c:\temp\test.pdf";
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            string imageOutputURL = string.Format("data:application/pdf;base64,{0}", imageBase64Data);

            ViewBag.InputImage = imageInputURL;
            ViewBag.OutputImage = imageOutputURL;
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
