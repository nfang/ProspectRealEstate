using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspectRealEstate.Web.Models
{
    interface IRepository<T>
    {
        IQueryable<T> All { get; }
        T Find(int id);
        void InsertOrUpdate(T entity);
        void Delete(int id);
        void SubmitChanges();
    }
}
