
using CustomerRegistration.Domain.Customers;

namespace CustomerRegistration.Application.Dtos.Results;

public record CreateCustomerResult
(
    bool Succeeded,
    Customer? Customer,
    string? ErrorMessage
);
