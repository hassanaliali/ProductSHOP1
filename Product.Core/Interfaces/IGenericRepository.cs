using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // الحصول على جميع الكيانات (نسخة غير قابلة للتعديل)
        Task<IReadOnlyList<T>> GetAllAsync();

        // الحصول على كيانات مع تضمين علاقات (Eager Loading)
        Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool disableTracking = true);

        // الحصول على كيان بواسطة المعرف
        Task<T> GetByIdAsync(int id);

        // الحصول على كيان مع شروط وتضمين علاقات
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        // إضافة كيان جديد
        Task AddAsync(T entity);

        // تحديث كيان موجود
        Task UpdateAsync(T entity);

        // حذف كيان بواسطة المعرف
        Task DeleteAsync(int id);

        // حذف كيان مباشر
        Task DeleteAsync(T entity);

        // التحقق من وجود كيان بناء على شرط
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
    //public interface IGenericRepository <T> where T:class
    //{
    //    Task<IReadOnlyList<T>> GetAllAsync();
    //    IEnumerable<T> GetAll();
    //    Task<IEnumerable<T>> GetALLAsync(params Expression<Func<T, bool>>[] includes);
    //    IEnumerable<T> GetAll(params Expression<Func<T, bool>>[] includes);
    //    Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    //    Task<T> GetAsync(int id);
    //    Task AddAsync(T entity);
    //    Task UpdateAsync(int id,T entity);
    //    Task DeleteAsync(int id);
    //}
}
