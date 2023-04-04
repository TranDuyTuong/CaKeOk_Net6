using Library.ServiceAdmin.ServiceAdminInjection.Gender;
using Library.ViewModel.Admin.V_Gender;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerCakeOk.Controllers
{
    public class GenderController : Controller
    {
        private readonly IGender _context;
        public GenderController(IGender context)
        {
            _context = context;    
        }

        //get all gender
        [HttpGet]
        public IActionResult PageGender()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllGender()
        {
            var result = _context.GetAllGender();
            return new JsonResult(result);
        }

        //create gender
        [HttpGet]
        public IActionResult CreateGenerGet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateGenderPost(string Name)
        {
            var request = new CreateGender()
            {
                Name = Name,
                Status = true
            };
            var result = _context.CreateGender(request);
            return new JsonResult(result);
        }
    }
}
