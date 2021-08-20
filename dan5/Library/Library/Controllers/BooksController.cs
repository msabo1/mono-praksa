using Library.Model.Common;
using Library.Model.Common.Book;
using Library.Service;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Controllers
{
    [RoutePrefix("books")]
    public class BooksController : ApiController
    {
        private IBooksService _service = new BooksService();
        [HttpPost]
        public async Task<IHttpActionResult> CreateAsync([FromBody()] ICreateBookDto createBookDto)
        {
            if (createBookDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IBook book = await _service.CreateAsync(createBookDto);
            return Content(System.Net.HttpStatusCode.Created, book);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync([FromUri] IQueryBooksDto queryBooksDto)
        {
            ICollection<IBook> books = await _service.GetAsync(queryBooksDto);
            return Ok(books);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(Guid id)
        {
            IBook book = await _service.GetByIdAsync(id);
            if (book == null)
            {
                return NotFoundResponse();
            }
            return Ok(book);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync(Guid id, [FromBody] IUpdateBookDto updateBookDto)
        {
            if (updateBookDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IBook book = await _service.UpdateAsync(id, updateBookDto);
            if (book == null)
            {
                return NotFoundResponse();
            }
            return Ok(book);

        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            if (await _service.DeleteAsync(id))
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            return NotFoundResponse();
        }

        private IHttpActionResult NotFoundResponse()
        {
            Dictionary<string, string> responseObj = new Dictionary<string, string>();
            responseObj.Add("Message", "Book with provided id is not found!");
            return Content(System.Net.HttpStatusCode.NotFound, responseObj);
        }
    }
}
