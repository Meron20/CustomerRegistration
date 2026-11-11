using CustomerRegistration.Application.Dtos.Requests;
using CustomerRegistration.Application.Service;
using CustomerRegistration.Infrastructure.Repositories;
using CustomerRegistration.Infrastructure.Stores;
using Xunit;

namespace CustomerRegistration.Tests.Application;

public class CustomerServiceTests
{
    private readonly CustomerService _service;
    private readonly InMemoryCustomerRepository _repository;

    // Runs before EACH test: empties the list and builds the real parts.
    public CustomerServiceTests()
    {
        InMemoryCustomerStore.Customers.Clear();
        _repository = new InMemoryCustomerRepository();
        _service = new CustomerService(_repository);
    }

    private static CreateCustomerRequest ValidRequest(
        string email = "anna@firma.se",
        string orgNumber = "556677-8899")
    {
        return new CreateCustomerRequest("Anna Andersson", email, "070-123 45 67", orgNumber, "secret1");
    }

    // ---------- AC1: main flow ----------

    [Fact]
    public void CreateCustomer_WithValidRequest_SucceedsAndSavesCustomer()
    {
        // Act
        var result = _service.CreateCustomer(ValidRequest());

        // Assert
        Assert.True(result.Succeeded);
        Assert.NotNull(result.Customer);
        Assert.Null(result.ErrorMessage);
        Assert.Single(_repository.GetAll());
    }

    // ---------- AC3: invalid input ----------

    [Fact]
    public void CreateCustomer_WithMissingName_FailsAndSavesNothing()
    {
        var request = new CreateCustomerRequest("", "anna@firma.se", "0701234567", "5566778899", "secret1");

        var result = _service.CreateCustomer(request);

        Assert.False(result.Succeeded);
        Assert.Null(result.Customer);
        Assert.False(string.IsNullOrWhiteSpace(result.ErrorMessage));
        Assert.Empty(_repository.GetAll());
    }

    // ---------- AC2: customer already exists ----------

    [Fact]
    public void CreateCustomer_WithSameEmailTwice_SecondFails()
    {
        _service.CreateCustomer(ValidRequest(email: "anna@firma.se", orgNumber: "5566778899"));

        // Same email, but in UPPERCASE: still the same customer after normalization
        var result = _service.CreateCustomer(ValidRequest(email: "ANNA@firma.se", orgNumber: "1122334455"));

        Assert.False(result.Succeeded);
        Assert.Null(result.Customer);
        Assert.False(string.IsNullOrWhiteSpace(result.ErrorMessage));
        Assert.Single(_repository.GetAll());
    }

    [Fact]
    public void CreateCustomer_WithSameOrganizationNumberTwice_SecondFails()
    {
        _service.CreateCustomer(ValidRequest(email: "anna@firma.se", orgNumber: "556677-8899"));

        // Same orgnr, written without the dash: still the same after normalization
        var result = _service.CreateCustomer(ValidRequest(email: "erik@firma.se", orgNumber: "5566778899"));

        Assert.False(result.Succeeded);
        Assert.Single(_repository.GetAll());
    }

    // ---------- Null request ----------

    [Fact]
    public void CreateCustomer_WithNullRequest_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _service.CreateCustomer(null!));
    }
}