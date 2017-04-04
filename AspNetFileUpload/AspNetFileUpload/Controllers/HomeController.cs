using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;

namespace AspNetFileUpload.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _hostingEnv;

        public HomeController(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
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

        public IActionResult Error()
        {
            return View();
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(ICollection<FormFile> uploadedFiles)
        {
            try
            {
                var uploadFolderPath = Path.Combine(_hostingEnv.WebRootPath, "upload");
                foreach (var file in uploadedFiles)
                {
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploadFolderPath, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }
                ViewBag.Message = "All file uploaded sucessfully";
            }
            catch (System.Exception exception)
            {
                ViewBag.Message ="Error Occured:" + exception.Message;
                //Write code here to handle the exception properly.
            }
        }
    }
}
