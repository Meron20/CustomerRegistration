
using CustomerRegistration.Domain.Customers;

namespace CustomerRegistration.Infrastructure.Stores;
internal static class InMemoryCustomerStore
{
    public static List<Customer> Customers { get; set; } = [];
}
