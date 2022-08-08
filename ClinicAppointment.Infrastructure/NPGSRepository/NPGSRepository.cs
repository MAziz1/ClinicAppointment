using ClinicAppointment.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Infrastructure.NPGSRepository
{
    public class NPGSRepository<T> : IRepository<T> where T : class
    {
        private readonly AppointmentDbContext _applicationContext;
        private DbSet<T> entities;

        public NPGSRepository(AppointmentDbContext applicationContext)
        {
            _applicationContext = applicationContext;
            entities = applicationContext.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public async Task<T> InsertItemAsync(T entity)
        {
            await entities.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int? page = null, int? pageSize = null)
        {
            IQueryable<T> query = _applicationContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                foreach (
                    var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }
    }
}
