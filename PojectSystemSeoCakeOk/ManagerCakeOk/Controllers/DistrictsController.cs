using Library.ServiceAdmin.ServiceAdminInjection.City;
using Library.ServiceAdmin.ServiceAdminInjection.District;
using Library.ViewModel.Admin.V_District;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerCakeOk.Controllers
{
    public class DistrictsController : Controller
    {
        private readonly IDistrict _context;
        public DistrictsController(IDistrict context)
        {
            _context = context;
        }

        //get all District
        [HttpGet]
        public IActionResult PageDistrict()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllDistrict(int PageSize, int PageIndex)
        {
            var result = _context.GetAllDistrict();
            var PadingModal = new PaingDistricts();
            PadingModal.Total = result.Count();
            PadingModal.L_District = result.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            return new JsonResult(PadingModal);
        }

        //Create District
        [HttpGet]
        public IActionResult CreateDistrict()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistrict(int IdCity, string NameDistrict)
        {
            var Request = new CreateDistrict()
            {
                IdCity = IdCity,
                Name = NameDistrict,
                Status = true
            };
            var Result = await _context.CreateDitrist(Request);
            return new JsonResult(Result);
        }

        //Top 10 District new create
        [HttpGet]
        public IActionResult TenDistirctCreate()
        {
            var result = _context.GetTenDistrict();
            return new JsonResult(result);

        }

        //Edit district
        [HttpGet]
        public async Task<IActionResult> EditDitrictGet(int Id)
        {
            var result = await _context.GetEditDitrict(Id);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditDistrict(int IdDistrict, int IdCity, string NameDistrict)
        {
            var result = new EditDistrict()
            {
                IdDitrict = IdDistrict,
                IdCity = IdCity,
                NameDistrict = NameDistrict
            };
            var Result = await _context.PostEditDistrict(result);
            return new JsonResult(Result);
        }

        //Edit Status district
        [HttpGet]
        public async Task<IActionResult> EditStatusDistrict(int Id)
        {
            var result = await _context.EditStatusDistrict(Id);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatusDistrictPost(int Id, int StatusID)
        {
            bool Status;
            if(StatusID == 1)
            {
                Status = true;
            }
            else
            {
                Status = false;
            }
            var result = await _context.PostEditStatusDistrict(Id, Status);
            return new JsonResult(result);
        }

        //Call all distric by this Id city
        [HttpGet]
        public IActionResult GetAllDistricById(int IdCity)
        {
            var Result = _context.GetAllDistrictByIdCity(IdCity);
            return new JsonResult(Result);
        }
    }
}
