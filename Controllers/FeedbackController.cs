using BusinessLayerr.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.FeedbackModel;
using ModelLayerr.User;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL ifeedbackBL;

        public FeedbackController(IFeedbackBL ifeedbackBL)
        {
            this.ifeedbackBL = ifeedbackBL;
        }

        [HttpPost]
        [Route("GiveFeedback")]
        public IActionResult GiveFeedback(FeedbackModel feedBackModel)
        {
         var result=ifeedbackBL.GiveFeedback(feedBackModel);
            if (result != null)
            {
                return Ok(new { sucess = true, Message = "Give Feedback Successfull", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Give Feedback Failed" });
            }
        }


        [HttpGet]
        [Route("GetAllFeedbacks")]

        public IActionResult GetAllUsers()
        {
           var result=ifeedbackBL.GetAllFeedbacks();
            if (result != null)
            {
                return Ok(new { success = true, Message = "Get  All Feedbacks Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Failed to Get All Feedbacks" });
            }
        }


        [HttpGet]
        [Route("GetFeedbacksByBookId")]

        public IActionResult GetFeedbacksByBookId(int BookId)
        {
            var result = ifeedbackBL.GetFeedbacksByBookId(BookId);
            if (result != null)
            {
                return Ok(new { success = true, Message = "Get Feedbacks By BookId Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Failed to Get  Feedbacks By Book Id" });
            }
        }

        [HttpPut]
        [Route("UpdateFeedback")]
        public IActionResult UpdateFeedback(int FeedbackId,FeedbackModel feedbackModel)
        {
         var result=ifeedbackBL.UpdateFeedback(FeedbackId, feedbackModel);

            if (result != null)
            {
                return Ok(new {success=true,Message="Feedback Updated Successfully",data=result});
            }
            else
            {
                return BadRequest(new { success = false, Message = "Feedback Updation Failed" });
            }
        }


        [HttpDelete]
        [Route("DeleteFeedback")]
        public IActionResult DeleteFeedback(int FeedbackId)
        {
            var result=ifeedbackBL.DeleteFeedback(FeedbackId);

            if (result != null)
            {
                return Ok(new { success = true, Message = "Feedback Deleted Successfully", data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Feedback Deletion Failed or feedback id is not present " });
            }
        }


    }
}
