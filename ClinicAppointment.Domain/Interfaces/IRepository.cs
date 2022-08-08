using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppointment.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> InsertItemAsync(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null,
                                               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                               string includeProperties = "",
                                               int? page = null,
                                               int? pageSize = null);
    }
}
