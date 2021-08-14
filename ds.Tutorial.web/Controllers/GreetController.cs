using ds.Tutorial.Model.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ds.Tutorial.web.Controllers
{
    public class GreetController : Controller
    {
        private IGreeting _greeting;

        // DIしたインスタンスはここで取り出し
        public GreetController(IGreeting greeting)
        {
            this._greeting = greeting;
        }

        public IActionResult Index()
        {
            this.ViewBag.Greet = this._greeting.Greet();
            return View();
        }
    }
}
