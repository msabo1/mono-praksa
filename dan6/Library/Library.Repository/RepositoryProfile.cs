using AutoMapper;
using AutoMapper.Data.Configuration.Conventions;
using Library.Model.Author;
using Library.Model.Book;
using System.Data;

namespace Library.Repository
{
    public class RepositoryProfile : Profile
    {
        public RepositoryProfile()
        {
            AddMemberConfiguration().AddMember<DataRecordMemberConfiguration>();
            CreateMap<IDataRecord, Author>();
            CreateMap<IDataRecord, Book>()
                .ForMember(
                    book => book.Author,
                    opt => opt.MapFrom((reader, book, i, ctx) => ctx.Mapper.Map<IDataRecord, Author>(reader))
                );
        }
    }
}
