using BusinessLayerr.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayerr.Book;
using ModelLayerr.User;
using System.Diagnostics.Contracts;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookBL ibookBL;

        public BookController(IBookBL ibookBL)
        {
            this.ibookBL = ibookBL;
        }

        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook(BookModel bookModel)
        {
            var result=ibookBL.AddBook(bookModel);
           
            if (result != null)
            {
                return Ok(new { sucess = true, Message = "Book Add Successfull", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "AddBook Failed" });
            }
        }


        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            var result=ibookBL.GetAllBooks();

            if (result != null)
            {
                return Ok(new {Success=true,Message="Get All Books Successfully",Data=result});
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get All Books  Failed" });
            }
        }


        [HttpPut]
        [Route("UpdateBook")]
        public IActionResult UpdateBook(int BookId,BookModel bookModel)
        {
            var result = ibookBL.UpdateBook(BookId,bookModel);
            if(result != null)
            {
                return Ok(new {Success=true,Message="Data Updated Successfully",Data=result});
            }
            else
            {
                return BadRequest(new { success = false, Message = " Data Updation Failed" });
            }
        }


        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteUser(int BookId)
        {
            try
            {
               bool result=ibookBL.DeleteBook(BookId);
                if (result)
                {
                    return Ok(new { success = true, message = "Book Deleted Successfully ", data = result });
                }
                else
                {
                    return Ok(new { success = false, message = "Book Deletion Failed" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        [HttpGet]
        [Route("GetByBookId")]
        public IActionResult GetByBookId(int BookId)
        {
            var result = ibookBL.GetByBookId(BookId);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get By BookId Successfully", Data = result });
            }
            else
            {
                return BadRequest(new { success = false, Message = "Get By BookId Failed" });
            }
        }

    }
}
