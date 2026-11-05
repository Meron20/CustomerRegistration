
using CustomerRegistration.Application.Dtos.Requests;
using CustomerRegistration.Application.Dtos.Results;

namespace CustomerRegistration.Application.Service;
public interface ICustomerService
{
    CreateCustomerResult CreateCustomer(CreateCustomerRequest request);
    GetAllCustomersResult GetAllCustomers();
}
