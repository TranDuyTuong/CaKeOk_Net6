using Library.ServiceAdmin.ServiceAdminInjection.Marriage;
using Library.ViewModel.Admin.V_Marriage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerCakeOk.Controllers
{
    public class MarriagesController : Controller
    {
        private readonly Imarriage _context;
        public MarriagesController(Imarriage context)
        {
            _context = context;
        }

        //get all marriage
        [HttpGet]
        public IActionResult PageMarriage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllMarriage()
        {
            var result = _context.GetAllMarriages();
            return new JsonResult(result);
        }

        //create Marriage
        [HttpGet]
        public IActionResult CreateMarriage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMarriage(string Name)
        {
            var result = new CreateMarriage()
            {
                Name = Name,
                Status = true
            };
            var ResultData = _context.CreateMarriage(result);
            return new JsonResult(ResultData);
        }
    }
}
