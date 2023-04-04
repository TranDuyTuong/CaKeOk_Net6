using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerCakeOk.Models.M_Account
{
    public class CreateStaff_M
    {
        public IFormFile ImageStaff { get; set; }
        public string CreateStaff { get; set; }
    }
}
