using Library.ServiceAdmin.ServiceAdminInjection.Account;
using Library.Utilities;
using Library.ViewModel.Admin.V_Account;
using ManagerCakeOk.Models.M_Account;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerCakeOk.Controllers
{
    public class ManagerAccountController : Controller
    {
        private readonly IAccount _context;
        public ManagerAccountController(IAccount context)
        {
            _context = context;
        }
        //page staff or chef
        [HttpGet]
        public IActionResult PageStaffOrChef()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllStaffOrChef()
        {
            var result = _context.GetAllStaffOrChef();
            return new JsonResult(result);
        }

        //create staff or chef
        [HttpPost]
        public IActionResult CreateStaffOrChef(string Name)
        {
            var result = _context.CreateStaffOrChef(Name, true);
            return new JsonResult(result);
        }

        //page account
        [HttpGet]
        public IActionResult PageAccount()
        {
            return View();
        }

        //Get All Account Staff
        [HttpGet]
        public IActionResult GetAllAccountStaff(int Index, int Size)
        {
            var Pading = new PadingAccount();
            var result = _context.GetAllAccount();
            Pading.L_Account = result.Skip((Index - 1) * Size).Take(Size).ToList();
            Pading.Total = result.Count();
            return new JsonResult(Pading);
        }

        //Create account staff
        [HttpGet]
        public IActionResult PageCreateAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccountGet()
        {
            var result = _context.GetInfoCreateAccount();
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountGet(CreateStaff_M request)
        {
            //conver file to byte
            ConverFileToByte converFile = new ConverFileToByte();
            byte[] FileConver = await converFile.ConverFromFileToByte(request.ImageStaff);
            string[] Splip = request.ImageStaff.FileName.Split('.');
            //add data into model
            CreateAccount account = new CreateAccount();
            account = JsonConvert.DeserializeObject<CreateAccount>(request.CreateStaff);
            account.CreateDate = DateTime.UtcNow.AddHours(7);
            account.ContentFile = FileConver;
            account.FileName = Splip[0];
            account.TypeImage = Splip[1];
            account.MimeType = request.ImageStaff.ContentType;
            var result = await _context.CreateAccount(account);
            return new JsonResult(result);
        }

        /// <summary>Detail Infomation Account Get </summary>
        [HttpGet]
        public IActionResult DetailAccountGet(Guid IdAccount)
        {
            ViewBag.IdAccount = IdAccount;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DetailAccountPost(Guid IdAccount)
        {
            var Result = await _context.DetailAccount(IdAccount);
            return new JsonResult(Result);
        }
    }
}
