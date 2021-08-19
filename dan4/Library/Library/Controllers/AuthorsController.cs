using Library.Model.Author;
using Library.Model.Common;
using Library.Service;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Library.Controllers
{
    [RoutePrefix("authors")]
    public class AuthorsController : ApiController
    {
        private IAuthorsService _service = new AuthorsService();
        [HttpPost]
        public IHttpActionResult Create([FromBody()] CreateAuthorDto createAuthorDto)
        {
            if (createAuthorDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IAuthor author = _service.Create(createAuthorDto);
            return Content(System.Net.HttpStatusCode.Created, author);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] QueryAuthorsDto queryAuthorsDto)
        {
            ICollection<IAuthor> authors = _service.Get(queryAuthorsDto);
            return Ok(authors);
        }

        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            IAuthor author = _service.GetById(id);
            if (author == null)
            {
                return NotFoundResponse();
            }
            return Ok(author);
        }

        [HttpPut]
        public IHttpActionResult Update(Guid id, [FromBody] UpdateAuthorDto updateAuthorDto)
        {
            if (updateAuthorDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IAuthor author = _service.Update(id, updateAuthorDto);
            if (author == null)
            {
                return NotFoundResponse();
            }
            return Ok(author);

        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            if (_service.Delete(id))
            {
                _service.Delete(id);
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            return NotFoundResponse();
        }

        private IHttpActionResult NotFoundResponse()
        {
            Dictionary<string, string> responseObj = new Dictionary<string, string>();
            responseObj.Add("Message", "Author with provided id is not found!");
            return Content(System.Net.HttpStatusCode.NotFound, responseObj);
        }
    }
}
