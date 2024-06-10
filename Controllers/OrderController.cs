using BusinessLayerr.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.Book;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderBL iorderBL;

        public OrderController(IOrderBL iorderBL)
        {
           this.iorderBL = iorderBL;
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public IActionResult PlaceOrder(int CartId)
        {
          var result=iorderBL.PlaceOrder(CartId);
            if (result != null)
            {
                return Ok(new { sucess = true, Message = "Order Placed Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Order Placed Failed" });
            }
        }


        [HttpGet]
        [Route("GetOrderDetailsByUserId")]
        public IActionResult GetOrderDetails(int UserId)
        {
            var result=iorderBL.GetAllOrdersByUserId(UserId);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get All Orders  By UserId Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get All Orders By UserId Failed" });
            }
        }



        [HttpGet]
        [Route("GetOrderDetails")]
        public IActionResult GetOrderDetailss()
        {
            var result = iorderBL.GetAllOrders();
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get All Orders Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get All Orders Failed" });
            }
        }


        [HttpDelete]
        [Route("CancelOrder")]
        public IActionResult CancelOrder(int OrderId)
        {
            try
            {
                bool result=iorderBL.CancelOrder(OrderId);
                if (result)
                {
                    return Ok(new { success = true, message = "Order Cancelled Successfully ", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Order Cancel Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }
}
