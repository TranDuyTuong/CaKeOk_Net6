using Library.ServiceAdmin.ServiceAdminInjection.Country;
using Library.ViewModel.Admin.V_Country;
using ManagerCakeOk.Models.M_Countrys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerCakeOk.Controllers
{
    public class CountrysController : Controller
    {
        private readonly ICountry _context;
        public CountrysController(ICountry context)
        {
            _context = context;
        }

        //get all country
        [HttpGet]
        public IActionResult PageCountry()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllCountry(int PageSize, int PageIndex, string SeachName)
        {
            var ModalPading = new PadingCountry();
            var result = _context.GetAllCountry();
            //seach name country
            if(SeachName != null)
            {
                var L_CoutrySeach = result.Where(x => x.Name.Contains(SeachName)).ToList();
                ModalPading.L_Country = L_CoutrySeach.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
                ModalPading.Total = L_CoutrySeach.Count();
            }
            else
            {
                ModalPading.L_Country = result.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
                ModalPading.Total = result.Count();
            }
            return new JsonResult(ModalPading);
        }
        
        //create country
        [HttpGet]
        public IActionResult PageCreateCountry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(string Name)
        {
            var request = new CreateCountry()
            {
                Name = Name,
                CreateDate = DateTime.UtcNow.AddHours(7),
                Status = true
            };
            var result = _context.CreateCountry(request);
            return new JsonResult(result);
        }

        //Create country with excel file
        [HttpPost]
        public async Task<IActionResult> CreateCoutryExcel(ImportExcelFileCountry request)
        {
            var ResultModel = new NotificationCountry();
            var L_Country = new List<CreateCountry>();
            IFormFile ExcelFile = request.FileExcel;
            string[] SplitFile = ExcelFile.FileName.Split('.');
            switch (SplitFile[1])
            {
                case "xlsx":
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var stream = new MemoryStream())
                    {
                        await ExcelFile.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row <= rowcount; row++)
                            {
                                L_Country.Add(new CreateCountry
                                {
                                    Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    CreateDate = DateTime.UtcNow.AddHours(7),
                                    Status = true
                                });
                            }
                        }

                    }
                    ResultModel = _context.CreateMupliteCountry(L_Country);
                    break;
                default:
                    ResultModel.Id = 1; // File Request is not File Excel
                    break;
            }
            return new JsonResult(ResultModel);
        }

        //get ten country
        [HttpGet]
        public IActionResult GetTenCountry()
        {
            var result = _context.GetTenCountry();
            return new JsonResult(result);
        }

    }
}
