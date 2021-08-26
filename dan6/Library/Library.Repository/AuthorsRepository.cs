using AutoMapper;
using Library.Common.Filters;
using Library.Common.Pagination;
using Library.Common.Sort;
using Library.Model;
using Library.Model.Common;
using Library.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class AuthorsRepository : RepositoryBase<IAuthor, IAuthorFilter>, IAuthorsRepository
    {
        private IMapper _mapper;
        public AuthorsRepository(SqlConnection connection, IMapper mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }

        public async Task<IAuthor> CreateAsync(IAuthor author)
        {
            author.Id = Guid.NewGuid();
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement(
                "INSERT INTO Author VALUES(@Id, @Name, @Gender)",
                ("@Id", author.Id),
                ("@Name", author.Name),
                ("@Gender", author.Gender)
            );
            await queryBuilder.ExecuteNonQueryAsync();
            return author;
        }

        public async Task<ICollection<IAuthor>> GetAsync(ISort sort = null, IPagination pagination = null, IAuthorFilter filter = null)
        {
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.Select("Author");
            AddSearch(queryBuilder, filter.Search, "Name", "Gender");
            AddFilters(queryBuilder, filter);
            AddSort(queryBuilder, sort, "Name");
            AddPagination(queryBuilder, pagination);
            return await queryBuilder.GetManyAsync(); ;
        }

        public async Task<IAuthor> GetByIdAsync(Guid id)
        {
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.Select("Author").Where("Id = @Id", ("@Id", id));
            return await queryBuilder.GetOneAsync();
        }

        public async Task<IAuthor> UpdateAsync(IAuthor author)
        {
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement(
                "UPDATE Author SET Name = @Name, Gender = @Gender WHERE Id = @Id",
                ("@Id", author.Id),
                ("@Name", author.Name),
                ("@Gender", author.Gender)
            );
            await queryBuilder.ExecuteNonQueryAsync();
            return author;
        }

        public async Task DeleteAsync(Guid id)
        {
            IQueryBuilder<IAuthor> queryBuilder = CreateQueryBuilder();
            queryBuilder.AddStatement("DELETE FROM Author WHERE Id = @Id", ("@Id", id));
            int rowsAffected = await queryBuilder.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new Exception();
            }
        }

        protected override IQueryBuilder<IAuthor> CreateQueryBuilder()
        {
            return new QueryBuilder<IAuthor>(_connection, _mapper.Map<IDataRecord, Author>);
        }
    }
}
