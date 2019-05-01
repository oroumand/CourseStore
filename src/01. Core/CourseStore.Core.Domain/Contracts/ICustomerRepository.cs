using CourseStore.Core.Domain.Entities;
using CourseStore.Core.Domain.ValueObjects;

namespace CourseStore.Core.Domain.Contracts
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Customer GetByEmail(Email email);
    }
}
