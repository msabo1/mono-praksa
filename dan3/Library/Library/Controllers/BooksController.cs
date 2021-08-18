using Day2.Models.Book;
using Day2.Repositories;
using Library.Models.Book;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Day2.Controllers
{
    [RoutePrefix("books")]
    public class BooksController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Create([FromBody()] CreateBookDto createBookDto)
        {
            if (createBookDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            Book book = BooksRepository.Create(createBookDto);
            return Content(System.Net.HttpStatusCode.Created, book);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] QueryBooksDto queryBooksDto)
        {
            ICollection<Book> books = BooksRepository.Get(queryBooksDto);
            return Ok(books);
        }

        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            Book book = BooksRepository.GetById(id);
            if (book == null)
            {
                return NotFoundResponse();
            }
            return Ok(book);
        }

        [HttpPut]
        public IHttpActionResult Update(Guid id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (updateBookDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            Book book = BooksRepository.Update(id, updateBookDto);
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
                BooksRepository.Delete(id);
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
