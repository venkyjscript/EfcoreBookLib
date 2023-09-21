using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BookLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bookController : ControllerBase
    {
        private readonly ILogger<bookController> _logger;
        private readonly IBookRepo<Book> bookRepo;
        public bookController(IBookRepo<Book> _bookRepo,ILogger<bookController> logger)
        {
            bookRepo = _bookRepo;
            _logger = logger;
        }

        

        [HttpGet]
       
        public JsonResult getBooks()
        {
            IEnumerable<Book> data;

            try
			{
                _logger.LogInformation("Entering api call");
                data = bookRepo.GetAll();
                //object m = null;
                //string s = m.ToString();
                _logger.LogInformation("returned data" + System.Text.Json.JsonSerializer.Serialize(data));
                return new JsonResult(data);
            }
			catch (Exception ex)
			{

                _logger.LogError(ex,ex.Message);
                return new JsonResult(ex.Message);
			}
           
        }


        [HttpPost("create")]
        public JsonResult AddBook([FromBody]Book book)
        {
            try
            {
                
                
                if (book == null)
                {
                    return new JsonResult(BadRequest("Books is null."));
                }
               return new JsonResult( bookRepo.Add(book));
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("mymeth")]//'/api/book/mymeth to hit here'this works same as above method but explicitely we have to mention request type hence better use httpGet
        [HttpPost]
        public void aaaa(int x)
        {
            
            Ok("success");
        }

        [HttpPost,ActionName("mymeth2")]
        public void aaaa2(int x)
        {
            Ok("success");
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


        [HttpDelete("[action]/{id}")]
        public async Task<object> delete(int id)
        {
            var result = await Task.FromResult(bookRepo.Delete(id));
            return result;
        }

    }
}
