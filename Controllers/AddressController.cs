using BusinessLayerr.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.Address;
using ModelLayerr.Book;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL iaddressBL;

        public AddressController(IAddressBL iaddressBL)
        {
            this.iaddressBL= iaddressBL;
        }

        [HttpPost]
        [Route("AddAddress")]
        public IActionResult AddAddress(AddressModel addressModel)
        {
          var result= iaddressBL.AddAddress(addressModel);
            if (result != null)
            {
                return Ok(new { sucess = true, Message = "Address Added Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Address Added Failed" });
            }
        }


        [HttpGet]
        [Route("GetAllAddress")]
        public IActionResult GetAllAddress()
        {
            var result=iaddressBL.GetAllAddress();
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get All Address Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get All Address  Failed" });
            }
        }


        [HttpGet]
        [Route("GetAddressByUserId")]
        public IActionResult GetAddressDetailsByUserId(int UserId)
        {
            var result=iaddressBL.GetAllAddressByUserId(UserId);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get All Address  By UserId Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get All Address By UserId Failed" });
            }
        }

        [HttpGet]
        [Route("GetAddressByAddressId")]
        public IActionResult GetAddressDetailsByAddressId(int AddressId)
        {
            var result=iaddressBL.GetAddressByAddressId(AddressId);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get Address  By AddressId Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get  Address By AddressId Failed" });
            }
        }


        [HttpPut]
        [Route("UpdateAddress")]
        public IActionResult UpdateAddress(int AddressId, AddressModel addressModel)
        {
            var result = iaddressBL.UpdateAddress(AddressId,addressModel);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Address Updated Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = " Address Updation Failed" });
            }
        }


        [HttpDelete]
        [Route("DeleteAddress")]
        public IActionResult DeleteUser(int AddressId)
        {
            try
            {
                bool result=iaddressBL.DeleteAddress(AddressId);
                if (result)
                {
                    return Ok(new { success = true, message = "Address Deleted Successfully ", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Address Deletion Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
