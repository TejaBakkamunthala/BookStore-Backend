using BusinessLayerr.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.Review;
using System.Data.SqlClient;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewBL ireviewBL;

        public ReviewController(IReviewBL ireviewBL)
        {
            this.ireviewBL = ireviewBL;
        }


        [HttpPut]
        [Route("GetBookByName")]
        public IActionResult GetBookDetails(ReviewModel reviewModel)
        {
            var result = ireviewBL.GetBookByName(reviewModel);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get  Books  By Title and Author Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get Books By Title and Author Failed" });
            }
        }



        [HttpGet]
        [Route("GetCartDetailssByUserId")]
        public IActionResult GetCartDetailss(int UserId)
        {
            var result=ireviewBL.GetAllCartssByUserId(UserId);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get All Carts  By UserId Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get All Carts By UserId Failed" });
            }
        }

    }
}
