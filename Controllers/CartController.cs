using BusinessLayerr.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.Book;
using ModelLayerr.Cart;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL icartBL;

        public CartController(ICartBL icartBL)
        {
            this.icartBL = icartBL;
        }


        [HttpPost]
        [Route("AddCart")]
        public IActionResult AddBook(CartModel cartModel)
        {
                    var result = icartBL.AddCart(cartModel);

                    if (result != null)
                    {
                        return Ok(new { sucess = true, Message = "Cart Add Successfull", Data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, Message = "Cart Add Failed" });
                    }
                }




        [HttpGet]
        [Route("GetCartDetailsByUserId")]
        public IActionResult GetCartDetails(int UserId)
        {
            var result=icartBL.GetAllCarts(UserId);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get All Carts  By UserId Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get All Carts By UserId Failed" });
            }
        }


        [HttpPut]
        [Route("UpdateCart")]
        public IActionResult UpdateCart(int CartId, int Qunatity)
        {
            if (CartId != null)
            {
                var result = icartBL.UpdateCart(CartId, Qunatity);
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "Cart Updated Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { success = false, Message = " Cart Updation Failed" });
                }
            }
            else
            {
                return Ok(new { success = true, Message = "you don't have any items in cart based on your provided CartId" });

            }
        }

        [HttpDelete]
        [Route("DeleteCart")]
        public IActionResult DeleteUser(int CartId)
        {
            try
            {
                bool result=icartBL.DeleteCart(CartId);
                if (result)
                {
                    return Ok(new { success = true, message = "Cart Deleted Successfully ", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Cart Deletion Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
