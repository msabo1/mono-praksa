using Library.Model.Author;
using Library.Model.Common;
using Library.Service;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Controllers
{
    [RoutePrefix("authors")]
    public class AuthorsController : ApiController
    {
        private IAuthorsService _service = new AuthorsService();
        [HttpPost]
        public async Task<IHttpActionResult> CreateAsync([FromBody()] CreateAuthorDto createAuthorDto)
        {
            if (createAuthorDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IAuthor author = await _service.CreateAsync(createAuthorDto);
            return Content(System.Net.HttpStatusCode.Created, author);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync([FromUri] QueryAuthorsDto queryAuthorsDto)
        {
            ICollection<IAuthor> authors = await _service.GetAsync(queryAuthorsDto);
            return Ok(authors);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(Guid id)
        {
            IAuthor author = await _service.GetByIdAsync(id);
            if (author == null)
            {
                return NotFoundResponse();
            }
            return Ok(author);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync(Guid id, [FromBody] UpdateAuthorDto updateAuthorDto)
        {
            if (updateAuthorDto == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IAuthor author = await _service.UpdateAsync(id, updateAuthorDto);
            if (author == null)
            {
                return NotFoundResponse();
            }
            return Ok(author);

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
            responseObj.Add("Message", "Author with provided id is not found!");
            return Content(System.Net.HttpStatusCode.NotFound, responseObj);
        }
    }
}
