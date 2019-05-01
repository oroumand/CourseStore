using CourseStore.Core.Domain.Contracts;
using CourseStore.Core.Domain.Entities;
using CourseStore.Infrastructures.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourseStore.Infrastructures.DataAccess.Repositories
{
    public class CustomerRepository : BaseRepository<Customer, CourseStoreContext>,ICustomerRepository
    {
        public CustomerRepository(CourseStoreContext dbContext) : base(dbContext)
        {
        }

        public Customer GetByEmail(string email)
        {
            return _dbContext.Customers.Include(c=>c.PurchasedCourses).
                ThenInclude(c=>c.Course).FirstOrDefault(c => c.Email.Value == email);
        }
        public override Customer GetById(long id)
        {
            return _dbContext.Customers.Include(c => c.PurchasedCourses).
               ThenInclude(c => c.Course).FirstOrDefault(c => c.Id == id);
        }
    }
}
