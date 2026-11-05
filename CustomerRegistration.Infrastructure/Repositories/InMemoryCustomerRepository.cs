
using CustomerRegistration.Domain.Customers;
using CustomerRegistration.Infrastructure.Stores;

namespace CustomerRegistration.Infrastructure.Repositories;
public class InMemoryCustomerRepository : ICustomerRepository
{
    public bool Create(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        bool alreadyExists = InMemoryCustomerStore.Customers.Any(c => c.EmailAddress == customer.EmailAddress || c.OrganizationNumber == customer.OrganizationNumber);

        if (alreadyExists)
            return false;

        InMemoryCustomerStore.Customers.Add(customer);
        return true;
    }

    public IReadOnlyList<Customer> GetAll()
    {
        var customers = InMemoryCustomerStore.Customers;

        return customers;

    }

}
