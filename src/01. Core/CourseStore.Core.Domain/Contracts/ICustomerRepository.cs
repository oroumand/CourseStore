using CourseStore.Core.Domain.Entities;

namespace CourseStore.Core.Domain.Contracts
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Customer GetByEmail(string email);
    }
}
