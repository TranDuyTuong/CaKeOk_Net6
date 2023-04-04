using Library.ServiceAdmin.ServiceAdminInjection.Role;
using Library.ViewModel.Admin.V_Role;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerCakeOk.Controllers
{
    public class RolesController : Controller
    {
        private readonly Iroles _context;
        public RolesController(Iroles context)
        {
            _context = context;
        }

        //get all roles
        [HttpGet]
        public IActionResult PageRole()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var result = _context.GetAllRoles();
            return new JsonResult(result);
        }

        //create roles
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRole(string NameRole, string Symbol, string Discription)
        {
            var request = new CreateRole()
            {
                NameRole = NameRole,
                Symbol = Symbol,
                CreateDate = DateTime.UtcNow.AddHours(7),
                Discription = Discription
            };
            var Result = _context.CreateRole(request);
            return new JsonResult(Result);
        }

        //Detail Roles
        [HttpGet]
        public IActionResult PageDetailRole(Guid Id)
        {
            ViewBag.IdRole = Id;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DetailRole(Guid Id)
        {
            var result = await _context.DetailRole(Id);
            return new JsonResult(result);
        }

    }
}
