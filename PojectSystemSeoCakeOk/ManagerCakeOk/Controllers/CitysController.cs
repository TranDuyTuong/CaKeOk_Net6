using Library.ServiceAdmin.ServiceAdminInjection.City;
using Library.ViewModel.Admin.V_City;
using ManagerCakeOk.Models.M_Citys;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class CitysController : Controller
    {
        private readonly ICitys context;
        public CitysController(ICitys _context)
        {
            context = _context;
        }

        //page view city
        [HttpGet]
        public IActionResult PageCity()
        {
            return View();
        }

        //page Create City
        [HttpGet]
        public IActionResult PageCreateCity()
        {
            return View();
        }

        //create city
        [HttpPost]
        public IActionResult CreateCity(string NameCity)
        {
            var C_City = new CreateCity();
            C_City.NameCity = NameCity;
            C_City.Status = true;
            var Result = context.CreateCity(C_City);
            return new JsonResult(Result);
        }

        //create Muplite Citys
        [HttpPost]
        public async Task<IActionResult> CreateMupliteCity(ImportExcelFileCity request)
        {
            var ResultModel = new NotificationCity();
            var L_City = new List<CreateCity>();
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
                                L_City.Add(new CreateCity
                                {
                                    NameCity = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    Status = true
                                });
                            }
                        }

                    }
                    ResultModel = context.CreateMupliteCity(L_City);
                    break;
                default:
                    ResultModel.Id = 1; // File Request is not File Excel
                    break;
            }
            return new JsonResult(ResultModel);
        }

        //Edit City
        [HttpGet]
        public async Task<IActionResult> PageEditMusic(int Id)
        {
            var Result = await context.EditCityGet(Id);
            return new JsonResult(Result);
        }

        [HttpPost]
        public async Task<IActionResult> EditMuisc(int Id, string Name)
        {
            var result = await context.EditCityPost(Id, Name);
            return new JsonResult(result);
        }

        //get ten city new
        [HttpGet]
        public IActionResult GetTenCityNew()
        {
            var Result = context.GetTenCityNew();
            return new JsonResult(Result);
        }

        //Get All Citys Not Pading
        [HttpGet]
        public IActionResult JsonGetAllCity()
        {
            var Result = context.GetAllCitys().Where(x=>x.Status == true);
            return new JsonResult(Result);
        }

        //Get All Citys
        [HttpGet]
        public IActionResult GetAllCitys(int Index, int Size, string Seach, int Sort)
        {
            var Pading = new PaingCitys();
            var Result = context.GetAllCitys();
            if(Seach != null)
            {
                //seach Name City
                var L_NameCity = Result.Where(x => x.NameCity.Contains(Seach)).ToList();
                if(L_NameCity.Count != 0)
                {
                    Pading.Total = L_NameCity.Count();
                    Pading.L_PaingCitys = L_NameCity.Skip((Index - 1) * Size).Take(Size).ToList();
                }
                else
                {
                    //Seach ID City
                    var L_IDCity = Result.Where(x => x.Id == Convert.ToInt32(Seach)).ToList();
                    Pading.Total = L_IDCity.Count();
                    Pading.L_PaingCitys = L_IDCity.Skip((Index - 1) * Size).Take(Size).ToList();
                }

            }
            else
            {
                Pading.Total = Result.Count();
                List<GetAllCitys> L_Sort = new List<GetAllCitys>();
                switch (Sort)
                {
                    case 1:
                        L_Sort = Result.OrderByDescending(x => x.Id).ToList();
                        Pading.L_PaingCitys = L_Sort.Skip((Index - 1) * Size).Take(Size).ToList();
                        break;
                    case -1:
                        L_Sort = Result.OrderBy(x => x.Id).ToList();
                        Pading.L_PaingCitys = L_Sort.Skip((Index - 1) * Size).Take(Size).ToList();
                        break;
                    case 2:
                        L_Sort = Result.OrderByDescending(x => x.NameCity).ToList();
                        Pading.L_PaingCitys = L_Sort.Skip((Index - 1) * Size).Take(Size).ToList();
                        break;
                    case -2:
                        L_Sort = Result.OrderBy(x => x.NameCity).ToList();
                        Pading.L_PaingCitys = L_Sort.Skip((Index - 1) * Size).Take(Size).ToList();
                        break;
                    default:
                        Pading.L_PaingCitys = Result.Skip((Index - 1) * Size).Take(Size).ToList();
                        break;
                }
            }
            return new JsonResult(Pading);
        }

        //Update Status Citys
        [HttpGet]
        public async Task<IActionResult> ChangeStatusCityGet(int Id)
        {
            var Request = await context.ChnageStatusCityGet(Id);
            return new JsonResult(Request);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatusCity(int Id, int IdStatus)
        {
            bool Status;
            if (IdStatus == 1)
            {
                Status = true;
            }
            else
            {
                Status = false;
            }
            var Result = await context.ChangeStatusPost(Id, Status);
            return new JsonResult(Result);
        }

        //Detail Citys
        [HttpGet]
        public IActionResult PageDetailCity(int Id)
        {
            ViewBag.IdCity = Id;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DetailCity(int Id, int PageSize, int PageIndex)
        {
            var result = await context.DetailCity(Id);
            var PadingModal = new DetailCity();
            PadingModal.Id = result.Id;
            PadingModal.Name = result.Name;
            PadingModal.Status = result.Status;
            PadingModal.TotalDistrict = result.L_District.Count;
            PadingModal.L_District = result.L_District.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            return new JsonResult(PadingModal);
        }
    }
}
