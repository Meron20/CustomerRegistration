
using CustomerRegistration.Domain.Customers;

namespace CustomerRegistration.Application.Dtos.Results;

public record GetAllCustomersResult
(
    bool Succeeded,
    IReadOnlyList<Customer> Customers,
    string? ErrorMessage
);
