namespace CustomerRegistration.Domain.Customers;
public interface ICustomerRepository
{
    bool Create(Customer customer);
    IReadOnlyList<Customer> GetAll();
}
