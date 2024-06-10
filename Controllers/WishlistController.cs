using BusinessLayerr.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWhislistBL iwishlistBL;

        public WishlistController(IWhislistBL iwishlistBL)
        {
            this.iwishlistBL = iwishlistBL;
        }


        [HttpPost]
        [Route("AddWishlist")]
        public IActionResult AddWishlist(int UserId,int BookId)
        {
            var result=iwishlistBL.AddWishlist(UserId, BookId);

            if (result != null)
            {
                return Ok(new { sucess = true, Message = "Add Whislist Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Add Whislist Failed" });
            }
        }


        [HttpGet]
        [Route("GetWishlistByUserId")]
        public IActionResult GetWishlistByUserId(int UserId)
        {
            var result=iwishlistBL.GetAllWishlistByUserId(UserId);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get All Wishlists  By UserId Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get All Wishlists By UserId Failed" });
            }
        }

        [HttpGet]
        [Route("GetWishlist")]
        public IActionResult GetWishlists()
        {
            var result = iwishlistBL.GetAllWishlists();
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get All Wishlists  Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get All Wishlists  Failed" });
            }
        }

        [HttpDelete]
        [Route("DeleteWishlist")]
        public IActionResult DeleteWishlist(int Wishlist)
        {
            try
            {
                bool result=iwishlistBL.DeleteWishlist(Wishlist);

                if (result)
                {
                    return Ok(new { success = true, message = "Delete Wishlist Successfully ", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Delete Wishlist Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }
}
