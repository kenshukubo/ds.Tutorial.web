using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ds.Tutorial.web.Controllers
{
    public class WorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> files)
        {
            // 保存先を取得
            string filePath = @"C:\work\";

            // ファイル保存
            long size = files.Sum(f => f.Length);
            foreach (var formFile in files)
            {
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