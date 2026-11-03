
namespace CustomerRegistration.Application.Dtos.Requests;

public record CreateCustomerRequest
(
    string Name,
    string EmailAddress,
    string PhoneNumber,
    string OrganizationNumber,
    string Password
);
