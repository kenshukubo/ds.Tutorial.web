using ds.Tutorial.web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ds.Tutorial.web.Controllers
{
    public class ParamsMappingTestcontroller : Controller
    {
        public IActionResult GetId(int id)
        {
            return Content($"Action params mapping id : {id}");
        }

        public IActionResult GetPerson(Person person)
        {
            return Json(new { message = "Action params mapping", data = person });
        }
    }
}
