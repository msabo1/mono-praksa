using AutoMapper;
using Library.Controllers;
using Library.Model;
using Library.Model.Common;

namespace Library.Mapper
{
    public class RestProfile : Profile
    {
        public RestProfile()
        {
            CreateMap<IAuthor, AuthorRest>();
            CreateMap<CreateAuthorRest, Author>();
            CreateMap<UpdateAuthorRest, Author>()
                .ForAllMembers(o => o.Condition((dto, author, member) => member != null));

            CreateMap<IBook, BookRest>();
            CreateMap<CreateBookRest, Book>();
            CreateMap<UpdateBookRest, Book>()
                .ForAllMembers(o => o.Condition((dto, book, member) => member != null)); ;
        }
    }
}
