
using CustomerRegistration.Domain.Customers;

namespace CustomerRegistration.Infrastructure.Stores;
public static class InMemoryCustomerStore
{
    public static List<Customer> Customers { get; set; } = [];
}
