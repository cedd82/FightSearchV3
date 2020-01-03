using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace FightSearch.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileProvider _fileProvider;
        public HomeController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public IActionResult Spa()
        {

            //string filePath = Path.Combine(Directory.GetCurrentDirectory(),"index.html");
            
            //IFileProvider provider = new PhysicalFileProvider(filePath);
            //IFileInfo fileInfo = provider.GetFileInfo(filePath);
            
            string filePath = "index.html";
            IFileInfo fileInfo = _fileProvider.GetFileInfo(filePath);
            Stream readStream = fileInfo.CreateReadStream();
            return File(readStream, "text/html");

            //var file = Path.Combine(Directory.GetCurrentDirectory(),"index.html");
            //return File(file, "text/html");
        }
    }
}
