using Day2.DataStorage;
using Day2.Models.Author;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Day2.Controllers
{
    [RoutePrefix("authors")]
    public class AuthorsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Create([FromBody()] CreateAuthorDto createAuthorDto)
        {
            if (createAuthorDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            Author author = AuthorsStorage.Create(createAuthorDto);
            return Content(System.Net.HttpStatusCode.Created, author);
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            ICollection<Author> authors = AuthorsStorage.GetAll();
            return Ok(authors);
        }

        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            Author author = AuthorsStorage.GetById(id);
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
            Author author = AuthorsStorage.Update(id, updateAuthorDto);
            if (author == null)
            {
                return NotFoundResponse();
            }
            return Ok(author);

        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                AuthorsStorage.Delete(id);
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
            responseObj.Add("Message", "Author with provided id is not found!");
            return Content(System.Net.HttpStatusCode.NotFound, responseObj);
        }
    }


}
