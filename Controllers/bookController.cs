using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bookController : ControllerBase
    {

        private readonly IBookRepo<Book> bookRepo;
        public bookController(IBookRepo<Book> _bookRepo)
        {
            bookRepo = _bookRepo;
        }

        [HttpGet]
        public JsonResult Books()
        {
            IEnumerable<Book> data = bookRepo.GetAll();
            return new JsonResult(data);
        }


        [HttpPost("create")]
        public JsonResult AddBook([FromBody]Book book)
        {
            try
            {
                if (book == null)
                {
                    return new JsonResult(BadRequest("Employee is null."));
                }
               return new JsonResult( bookRepo.Add(book));
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("{id}")]

        public JsonResult Book(int id)
        {
            //Book data = bookRepo.Get(id);
            return new JsonResult(bookRepo.Get(id));
        }

        
        
        [HttpPut("{id}/[action]")]
        public JsonResult update(int id,[FromBody]Book book)
        {
            //Book data = bookRepo.UpdateById(id);
            return new JsonResult(bookRepo.UpdateById(id,book));
        }

    }
}
