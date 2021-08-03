using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace WebApi.Extensions
{
    public static class EntityFrameworkDbSetExtensions
    {
        public static IQueryable<TEntity> MultiInclude<TEntity>(this DbSet<TEntity> source, Type entityInclude) where TEntity : class
        {
            var query = source.AsQueryable();

            foreach (var prop in typeof(TEntity).GetProperties())
            {
                if (prop.PropertyType.IsSubclassOf(entityInclude))
                {
                    query = source.Include(prop.Name.ToString());
                }
            }
            return query;
        }
    }
}
