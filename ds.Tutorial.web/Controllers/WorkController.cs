using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ds.Tutorial.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ds.Tutorial.web.Controllers
{
    public class WorkController : Controller
    {
        //こっちだと動作しない
        //public IActionResult Index()
        //{
        //    Console.WriteLine("画面表示");
        //    return View(new CreatePost());
        //}

        //private readonly IHostingEnvironment hostingEnvironment;
        //public WorkController(IHostingEnvironment environment)
        //{
        //    hostingEnvironment = environment;
        //}

        //[HttpPost]
        //public IActionResult FileUpload(CreatePost model)
        //{
        //    Console.WriteLine("あっぷろーど");

        //    // do other validations on your model as needed
        //    if (model.MyImage != null)
        //    {
        //        var uniqueFileName = GetUniqueFileName(model.MyImage.FileName);
        //        var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
        //        var filePath = Path.Combine(uploads, uniqueFileName);
        //        model.MyImage.CopyTo(new FileStream(filePath, FileMode.Create));
        //        Console.WriteLine("あっぷろーど完了");
        //    }

        //    return RedirectToAction("Index", "Work");
        //}

        //private string GetUniqueFileName(string fileName)
        //{
        //    fileName = Path.GetFileName(fileName);
        //    return Path.GetFileNameWithoutExtension(fileName)
        //              + "_"
        //              + Guid.NewGuid().ToString().Substring(0, 4)
        //              + Path.GetExtension(fileName);
        //}

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> files)
        {
            // 保存先を取得
            string filePath = @"C:\work\";

            Console.WriteLine("あっぷろーど");

            // ファイル保存
            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
                Console.WriteLine("あっぷろーど開始");
                if (formFile.Length <= 0) continue;
                // ローカルに保存
                using (var stream = new FileStream(filePath + formFile.FileName, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            // 画面に返す返却値
            ViewData["uploadResult"] = Ok(new { count = files.Count, size, filePath }).Value.ToString();

            // ViewをIndex画面で返却
            return View("Index");
        }
    }
}