using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dapper;
using System.Data;
using System.Text;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Http.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Galeria.Infraestructure.Repositories.Generic
{
    public class BaseRepository<T> where T : class
    {
        /// <summary>
        /// The context
        /// </summary>
        protected readonly ApplicationDbContext Context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(T entity)
        {
            var createdAtProperty = typeof(T).GetProperty("CreatedAt");
            if (createdAtProperty != null && createdAtProperty.PropertyType == typeof(DateTime))
            {
                var currentValue = createdAtProperty.GetValue(entity);
                if (currentValue == null || (DateTime)currentValue == default)
                {
                    createdAtProperty.SetValue(entity, DateTime.UtcNow);
                }
            }

            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();

            return (int)typeof(T).GetProperty("Id")?.GetValue(entity);
        }


        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the asynchronous (soft delete).
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<int> RemoveAsync(T entity)
        {
            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                typeof(T).GetProperty("IsDeleted")?.SetValue(entity, true);
                Context.Set<T>().Update(entity);
            }
            else
            {
                Context.Set<T>().Remove(entity);
            }
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the asynchronous by id (soft delete).
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<int> RemoveAsync(int id)
        {
            var entity = await Context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                if (typeof(T).GetProperty("IsDeleted") != null)
                {
                    typeof(T).GetProperty("IsDeleted")?.SetValue(entity, true);
                    Context.Set<T>().Update(entity);
                }
                else
                {
                    Context.Set<T>().Remove(entity);
                }
                return await Context.SaveChangesAsync();
            }
            return 0;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = Context.Set<T>();

            var isDeletable = typeof(T).GetProperty("IsDeleted") != null;

            if (isDeletable)
            {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
        public async Task<(List<T> Items, int Total)> GetAllFilterAsync(
            int? page = null, int? limit = null,
            string? orderBy = null, string? orderDirection = "asc",
            DateTime? startDate = null, DateTime? endDate = null,
            string? filterField = null, string? filterValue = null,
            string? relationField = null, int? relationId = null)
        {
            var tableName = typeof(T).GetCustomAttribute<TableAttribute>()?.Name ?? typeof(T).Name;

            var sql = new StringBuilder($"SELECT * FROM {tableName} WHERE IsDeleted = 0 ");

            var parameters = new DynamicParameters();

            if (startDate.HasValue)
            {
                sql.Append(" AND CreatedAt >= @StartDate");
                parameters.Add("StartDate", startDate.Value);
            }
            if (endDate.HasValue)
            {
                sql.Append(" AND CreatedAt <= @EndDate");
                parameters.Add("EndDate", endDate.Value);
            }

            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                sql.Append($" AND {filterField} LIKE @FilterValue");
                parameters.Add("FilterValue", $"%{filterValue}%");
            }

            if (!string.IsNullOrEmpty(relationField) && relationId.HasValue)
            {
                sql.Append($" AND {relationField} = @RelationId");
                parameters.Add("RelationId", relationId.Value);
            }

            var countSql = new StringBuilder($"SELECT COUNT(*) FROM {tableName} WHERE IsDeleted = 0 ");

            if (startDate.HasValue)
            {
                countSql.Append(" AND CreatedAt >= @StartDate");
            }
            if (endDate.HasValue)
            {
                countSql.Append(" AND CreatedAt <= @EndDate");
            }
            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                countSql.Append($" AND {filterField} LIKE @FilterValue");
            }
            if (!string.IsNullOrEmpty(relationField) && relationId.HasValue)
            {
                countSql.Append($" AND {relationField} = @RelationId");
            }

            var total = await Context.Database.GetDbConnection().ExecuteScalarAsync<int>(countSql.ToString(), parameters);

            if (!string.IsNullOrEmpty(orderBy))
            {
                sql.Append($" ORDER BY {orderBy} {orderDirection}");
            }

            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "CreatedAt";
                sql.Append($" ORDER BY {orderBy} {orderDirection}");
            }

            if (page.HasValue && limit.HasValue)
            {
                int offset = (page.Value - 1) * limit.Value;
                sql.Append(" OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY");
                parameters.Add("Offset", offset);
                parameters.Add("Limit", limit.Value);
            }

            var items = await Context.Database.GetDbConnection().QueryAsync<T>(sql.ToString(), parameters);
            return (items.ToList(), total);
        }


        /// <summary>
        /// Gets the single asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual async Task<T?> GetSingleAsync(Expression<Func<T, bool>>? filter = null)
        {
            var query = Context.Set<T>().AsQueryable();

            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        }
}
