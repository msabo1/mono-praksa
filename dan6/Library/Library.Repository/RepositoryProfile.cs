using AutoMapper;
using AutoMapper.Data.Configuration.Conventions;
using Library.Model;
using System.Data;

namespace Library.Repository
{
    public class RepositoryProfile : Profile
    {
        public RepositoryProfile()
        {
            AddMemberConfiguration().AddMember<DataRecordMemberConfiguration>();
            CreateMap<IDataRecord, Author>()
                .ForMember(author => author.Id, opt => opt.MapFrom(reader => reader["Author.Id"]))
                .ForMember(author => author.Name, opt => opt.MapFrom(reader => reader["Author.Name"]))
                .ForMember(author => author.Gender, opt => opt.MapFrom(reader => reader["Author.Gender"]))
                .ForMember(author => author.Books, opt => opt.Ignore())
                .AfterMap((reader, author, ctx) =>
                        {
                            try
                            {
                                if (reader["Book.Id"] != null)
                                {
                                    author.Books.Add(ctx.Mapper.Map<IDataRecord, Book>(reader));
                                }
                            }
                            catch { }
                        });
            CreateMap<IDataRecord, Book>()
                .ForMember(book => book.Id, opt => opt.MapFrom(reader => reader["Book.Id"]))
                .ForMember(book => book.Title, opt => opt.MapFrom(reader => reader["Book.Title"]))
                .ForMember(book => book.AuthorId, opt => opt.MapFrom(reader => reader["Book.AuthorId"]));
        }
    }
}
