using Library.Model.Common;
using Library.Model.Common.Book;
using Library.Service;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Library.Controllers
{
    [RoutePrefix("books")]
    public class BooksController : ApiController
    {
        private IBooksService _service = new BooksService();
        [HttpPost]
        public IHttpActionResult Create([FromBody()] ICreateBookDto createBookDto)
        {
            if (createBookDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IBook book = _service.Create(createBookDto);
            return Content(System.Net.HttpStatusCode.Created, book);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] IQueryBooksDto queryBooksDto)
        {
            ICollection<IBook> books = _service.Get(queryBooksDto);
            return Ok(books);
        }

        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            IBook book = _service.GetById(id);
            if (book == null)
            {
                return NotFoundResponse();
            }
            return Ok(book);
        }

        [HttpPut]
        public IHttpActionResult Update(Guid id, [FromBody] IUpdateBookDto updateBookDto)
        {
            if (updateBookDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IBook book = _service.Update(id, updateBookDto);
            if (book == null)
            {
                return NotFoundResponse();
            }
            return Ok(book);

        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch
            {
                return NotFoundResponse();
            }
        }

        private IHttpActionResult NotFoundResponse()
        {
            Dictionary<string, string> responseObj = new Dictionary<string, string>();
            responseObj.Add("Message", "Book with provided id is not found!");
            return Content(System.Net.HttpStatusCode.NotFound, responseObj);
        }
    }
}
