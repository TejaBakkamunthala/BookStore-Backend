using BusinessLayerr.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.Address;
using ModelLayerr.Admin;
using System.Data.SqlClient;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IAdminBL iadminBL;

        public AdminController(IAdminBL iadminBL)
        {
            this.iadminBL = iadminBL;
        }


        [HttpPost]
        [Route("AddAdmin")]
        public IActionResult AddAdmin(AdminModel adminModel)
        {
            var result=iadminBL.AddAdmin(adminModel);
            if (result != null)
            {
                return Ok(new { sucess = true, Message = "Admin Added Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Admin Added Failed" });
            }
        }



        [HttpPost]
        [Route("Login")]

        public IActionResult AdminLogin(AdminLoginModel adminLoginModel)
        {
            var result=iadminBL.LoginAdmin(adminLoginModel);

            if (result != null)
            {
                return Ok(new { success = true, Message = "Admin Login Successfull", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Admin Login Failed" });
            }
        }

    }

}
