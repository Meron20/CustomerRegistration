using CustomerRegistration.Domain.Customers;
using CustomerRegistration.Infrastructure.Repositories;
using CustomerRegistration.Infrastructure.Stores;
using Xunit;

namespace CustomerRegistration.Tests.Infrastructure;

public class InMemoryCustomerRepositoryTests
{
    private readonly InMemoryCustomerRepository _repository = new();

    // Runs before EACH test: empties the shared list.
    public InMemoryCustomerRepositoryTests()
    {
        InMemoryCustomerStore.Customers.Clear();
    }

    private static Customer CreateCustomer(string email, string orgNumber)
    {
        return new Customer(Guid.NewGuid(), "Anna Andersson", email, "0701234567", orgNumber, "secret1");
    }

    [Fact]
    public void Create_NewCustomer_ReturnsTrueAndSavesCustomer()
    {
        // Arrange
        var customer = CreateCustomer("anna@firma.se", "5566778899");

        // Act
        bool saved = _repository.Create(customer);

        // Assert
        Assert.True(saved);
        Assert.Single(_repository.GetAll());
    }

    [Fact]
    public void Create_SameEmailTwice_ReturnsFalse()
    {
        _repository.Create(CreateCustomer("anna@firma.se", "5566778899"));

        bool saved = _repository.Create(CreateCustomer("anna@firma.se", "1122334455"));

        Assert.False(saved);
        Assert.Single(_repository.GetAll());
    }

    [Fact]
    public void Create_SameOrganizationNumberTwice_ReturnsFalse()
    {
        _repository.Create(CreateCustomer("anna@firma.se", "5566778899"));

        bool saved = _repository.Create(CreateCustomer("erik@firma.se", "5566778899"));

        Assert.False(saved);
        Assert.Single(_repository.GetAll());
    }

    [Fact]
    public void Create_Null_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _repository.Create(null!));
    }
}