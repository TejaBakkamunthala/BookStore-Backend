using BusinessLayerr.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.User;
using RepositoryLayerr.Interfaces;


namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserBL iuserBL;


        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;

        }

        [HttpPost]
        [Route("Register")]
        public IActionResult UserRegistration(UserModel userModel)
        {
            var result = iuserBL.UserRegistration(userModel);

            if (result != null)
            {
                return Ok(new { sucess = true, Message = "Registration Successfull", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Registartion Failed" });
            }
        }


        [HttpGet]
        [Route("GetAllUsers")]

        public IActionResult GetAllUsers()
        {
            var result = iuserBL.GetAllUsers();
            if (result != null)
            {
                return Ok(new { success = true, Message = "Get  All Users Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Failed to Get All Users" });
            }
        }


        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {

            var result = iuserBL.UserLogin(userLoginModel);

            if (result != null)
            {
                return Ok(new { sucess = true, Message = "Login Successfull", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Login Failed", });
            }

        }


        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string EmailId)
        {

            var password = iuserBL.ForgotPassword(EmailId);

            if (password != null)
            {
                SendModelLayer send = new SendModelLayer();
                ForgotPasswordModel forgotPasswordModel = iuserBL.ForgotPassword(EmailId);
                send.SendMail(forgotPasswordModel.EmailId, forgotPasswordModel.Token);
                Uri uri = new Uri("rabbitmq:://localhost/FunDooNotesEmailQueue");
                // var endPoint = await bus.GetSendEndpoint(uri);
                // await endPoint.Send(forgotPasswordModel);
                return Ok(new { success = true, message = "Mail sent Successfully", data = password.Token });
            }
            else
            {
                return BadRequest(new { success = false, message = "Email Does not Exist" });
            }
        }

        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string Password)
        {
            string EmailId = User.Claims.FirstOrDefault(x => x.Type == "EmailId").Value;
            var res = iuserBL.ResetPassword(EmailId, Password);
            if (res)
            {
                return Ok(new { success = true, message = "Password Reset is done" });

            }
            else
            {
                return BadRequest("Password is not Updated");
            }
        }


        [HttpPut]
        [Route("UpdateData")]

        public IActionResult UpdateUser(int UserId, UserModel userModel)
        {
            try
            {
                bool result = iuserBL.UpdateUser(UserId, userModel);
                if (result)
                {

                    return Ok(new { success = true, message = "Successfully Data Updated ", data = result });

                }
                else
                {

                    return Ok(new { succes = false, messaage = "Data Updation Failed " });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete]
        [Route("DeleteData")]
        public IActionResult DeleteUser(int UserId)
        {
            try
            {
                bool result = iuserBL.DeleteUser(UserId);
                if (result)
                {
                    return Ok(new { success = true, message = "Data Deleted Successfully ", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Data Deleted Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}