using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BlogApp.Model.Base;
using System.Data.Common;
using System.Data;
using System.Security.Principal;

namespace BlogApp.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private DbSet<T> _objectSet;
        private bool _disposed;
        protected readonly BlogAppDbContext context;

        protected virtual DbSet<T> Entities => _objectSet ??= context.Set<T>();

        public Repository(BlogAppDbContext context)
        {
            this.context = context;
            _objectSet = this.context.Set<T>();
        }

        public IQueryable<T> ListQueryable => Entities;

        public IQueryable<T> ListQueryableNoTracking => Entities.AsNoTracking();

        BlogAppDbContext IRepository<T>.GetDbContext => context;

        public async Task<DbOperationResult> Delete(T entity)
        {
            try
            {
                Entities.Remove(entity);
                await context.SaveChangesAsync();
                return new DbOperationResult(true, "Veri Silindi");
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }

        public async Task<DbOperationResult> Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                return new DbOperationResult(false, "Liste boþ gönderilemez");

            try
            {
                Entities.RemoveRange(entities);
                await context.SaveChangesAsync();
                return new DbOperationResult(true, "Veriler Silindi");
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }

        public async Task<T> GetById(object id)
        {
            return await _objectSet.FindAsync(id);
        }

        public IEnumerable<T> GetSql(string sql)
        {
            return Entities.FromSqlRaw(sql);
        }

        public IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes)
        {
            return Entities.IncludeMultiple(includes);
        }

        public async Task<DbOperationResult> Insert(T entity)
        {
            if (entity == null)
                return new DbOperationResult(false, "Boþ veri kaydedilemez");

            try
            {
                Entities.Add(entity);
                await context.SaveChangesAsync();
                return new DbOperationResult(true, "Veri Eklendi");
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }

        public async Task<DbOperationResult> Insert(IEnumerable<T> entities)
        {
            if (entities == null)
                return new DbOperationResult(false, "Boþ veri kaydedilemez");

            try
            {
                Entities.AddRange(entities);
                await context.SaveChangesAsync();
                return new DbOperationResult(true, "Veriler Eklendi");
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }

        public async Task<DbOperationResult> Update(T entity)
        {
            if (entity == null)
                return new DbOperationResult(false, "Boþ veri güncellenemez");

            try
            {
                Entities.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return new DbOperationResult(true, "Veri Güncellendi");
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
        }

        public async Task<DbOperationResult> Update(IEnumerable<T> entities)
        {
            if (entities == null)
                return new DbOperationResult(false, "Boþ veri güncellenemez");

            try
            {
                await context.SaveChangesAsync();
                return new DbOperationResult(true, "Veriler Güncellendi");
            }
            catch (Exception ex)
            {
                return new DbOperationResult(false, ex.Message);
            }
            
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Detach(T entity)
        {
            context.Entry(entity).State = EntityState.Detached;
        }
    }
}
public static class Extensions
{
    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
    {
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }
        return query;
    }

    public static DataTable DataTable(this DbContext context, string sqlQuery, params DbParameter[] parameters)
    {
        DataTable dataTable = new DataTable();
        DbConnection connection = context.Database.GetDbConnection();
        DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connection);
        using (var cmd = dbFactory.CreateCommand())
        {
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlQuery;
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }
            using (DbDataAdapter adapter = dbFactory.CreateDataAdapter())
            {
                adapter.SelectCommand = cmd;
                adapter.Fill(dataTable);
            }
        }
        return dataTable;
    }
}
