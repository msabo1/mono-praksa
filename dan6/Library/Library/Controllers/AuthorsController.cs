using Library.Model.Author;
using Library.Model.Common;
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
        private IAuthorsService _service;
        private IMapper _mapper;

        public AuthorsController(IAuthorsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateAsync([FromBody()] CreateAuthorRest createAuthorRest)
        {
            if (createAuthorRest == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IAuthor author = new Author();
            author.Name = createAuthorRest.Name;
            author.Gender = createAuthorRest.Name;
            author = await _service.CreateAsync(author);
            return Content(System.Net.HttpStatusCode.Created, _mapper.MapAuthorDomainToRest(author));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync([FromUri] QueryAuthorsDto queryAuthorsDto)
        {
            ICollection<IAuthor> authors = await _service.GetAsync(queryAuthorsDto);
            return Ok(_mapper.CollectionMapAuthorDomainToRest(authors));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByIdAsync(Guid id)
        {
            IAuthor author = await _service.GetByIdAsync(id);
            if (author == null)
            {
                return NotFoundResponse();
            }
            return Ok(_mapper.MapAuthorDomainToRest(author));
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateAsync(Guid id, [FromBody] UpdateAuthorRest updateAuthorRest)
        {
            if (updateAuthorRest == null)
            {
                return BadRequest("Body cannot be empty!");
            }
            IAuthor author = await _service.GetByIdAsync(id);
            if (author == null)
            {
                return NotFoundResponse();
            }
            if (updateAuthorRest.Name != null)
            {
                author.Name = updateAuthorRest.Name;
            }
            if (updateAuthorRest.Gender != null)
            {
                author.Gender = updateAuthorRest.Gender;
            }
            author = await _service.UpdateAsync(author);
            return Ok(_mapper.MapAuthorDomainToRest(author));

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

    public class AuthorRest
    {
        public Guid Id;
        public string Name { get; set; }
        public string Gender { get; set; }
    }
    public class CreateAuthorRest
    {
        public string Name { get; set; }
        public string Gender { get; set; }
    }

    public class UpdateAuthorRest
    {
#nullable enable
        public string? Name { get; set; }
        public string? Gender { get; set; }
#nullable disable
    }
}
