
using CustomerRegistration.Domain.Models;

namespace CustomerRegistration.Application.Dtos.Results;

public record CreateCustomerResult
(
    bool Succeeded,
    Customer? Customer,
    string? ErrorMessage
);
