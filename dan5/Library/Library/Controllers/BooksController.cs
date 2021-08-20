using Library.Model.Book;
using Library.Model.Common;
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
        private Mapper _mapper = new Mapper();
        [HttpPost]
        public async Task<IHttpActionResult> CreateAsync([FromBody()] CreateBookRest createBookRest)
        {
            if (createBookRest == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IBook book = new Book();
            book.Name = createBookRest.Name;
            book.AuthorId = createBookRest.AuthorId;
            book = await _service.CreateAsync(book);
            return Content(System.Net.HttpStatusCode.Created, _mapper.MapBookDomainToRest(book));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync([FromUri] QueryBooksDto queryBooksDto)
        {
            ICollection<IBook> books = await _service.GetAsync(queryBooksDto);
            return Ok(_mapper.CollectionMapBookDomainToRest(books));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(Guid id)
        {
            IBook book = await _service.GetByIdAsync(id);
            if (book == null)
            {
                return NotFoundResponse();
            }
            return Ok(_mapper.MapBookDomainToRest(book));
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync(Guid id, [FromBody] UpdateBookRest updateBookRest)
        {
            if (updateBookRest == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IBook book = await _service.GetByIdAsync(id);
            if (book == null)
            {
                return NotFoundResponse();
            }
            if (updateBookRest.Name != null)
            {
                book.Name = updateBookRest.Name;
            }
            if (updateBookRest.AuthorId != null)
            {
                book.AuthorId = (Guid)updateBookRest.AuthorId;
            }
            await _service.UpdateAsync(book);
            return Ok(_mapper.MapBookDomainToRest(book));
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

    public class BookRest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AuthorRest Author { get; set; }
    }

    public class CreateBookRest
    {
        public string Name { get; set; }
        public Guid AuthorId { get; set; }
    }

    public class UpdateBookRest
    {
#nullable enable
        public string? Name { get; set; }
        public Guid? AuthorId { get; set; }
#nullable disable
    }

}
