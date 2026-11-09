using CustomerRegistration.Application.Dtos.Requests;
using CustomerRegistration.Application.Dtos.Results;
using CustomerRegistration.Domain.Customers;

namespace CustomerRegistration.Application.Service;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public CreateCustomerResult CreateCustomer(CreateCustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Customer customer;
        try {

            var id = Guid.NewGuid();

            customer = new Customer
                (id,
                request.Name, 
                request.EmailAddress,
                request.PhoneNumber, 
                request.OrganizationNumber, 
                request.Password );
        }
        catch (ArgumentException ex)
        {
            return new CreateCustomerResult(false, null, ex.Message);
        }

        bool saved = customerRepository.Create(customer);

        return saved
            ? new CreateCustomerResult(true, customer, null)
            : new CreateCustomerResult(false, null, "Email address or organization number is already in use.");
    }

    public GetAllCustomersResult GetAllCustomers()
    {
        var customers = customerRepository.GetAll();
        return new GetAllCustomersResult(true, customers, null);
    }
}
