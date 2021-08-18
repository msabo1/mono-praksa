using Day2.Models.Author;
using Library.DataStorage;
using Library.Models.Author;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Library.Controllers
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
            Author author = AuthorsRepository.Create(createAuthorDto);
            return Content(System.Net.HttpStatusCode.Created, author);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] QueryAuthorsDto queryAuthorsDto)
        {
            ICollection<Author> authors = AuthorsRepository.Get(queryAuthorsDto);
            return Ok(authors);
        }

        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            Author author = AuthorsRepository.GetById(id);
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
            Author author = AuthorsRepository.Update(id, updateAuthorDto);
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
                AuthorsRepository.Delete(id);
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
