using ds.Tutorial.Model;
using ds.Tutorial.Model.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ds.Tutorial.web.Controllers
{
    public class EfCoreController : Controller
    {
        private TutorialRepository _repository;

        public EfCoreController(TutorialRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Create(UserEntity user)
        {
            var message = _repository.Create(user) > 0 ? "ok" : "ng";
            return Json(new { Message = message, User = user });
        }

        public IActionResult Update(UserEntity user)
        {
            var message = _repository.Update(user) > 0 ? "ok" : "ng";
            return Json(new { Message = message, User = user });
        }

        public IActionResult Delete(int id)
        {
            var message = _repository.Delete(id) > 0 ? "ok" : "ng";
            return Json(new { Message = message });
        }

        public IActionResult GetOneById(int id)
        {
            var user = _repository.GetOneById(id);
            return Json(new { User = user });
        }

        public IActionResult GetAllByAge(int age)
        {
            var users = _repository.GetAllByAge(age);
            return Json(new { User = users });
        }

        public IActionResult GetNamesByAge(int age)
        {
            var names = _repository.GetNamesByAge(age);
            return Json(new { Names = names });
        }

        public IActionResult GetAllPaging(int pageSize, int page)
        {
            var users = _repository.GetAllPaging(pageSize, page);
            return Json(new { Users = users });
        }

        public IActionResult FixAge()
        {
            var count = _repository.FixAge();
            return Json(new { FixCount = count });
        }
    }
}
