using CustomerRegistration.Domain.Customers;
using Xunit;

namespace CustomerRegistration.Tests.Domain;

public class CustomerTests
{
    // Helper: creates a customer with valid values,
    // so each test only changes the value it wants to test.
    private static Customer CreateCustomer(
        string name = "Anna Andersson",
        string email = "anna@firma.se",
        string phone = "0701234567",
        string orgNumber = "5566778899",
        string password = "secret1",
        Guid? id = null)
    {
        return new Customer(id ?? Guid.NewGuid(), name, email, phone, orgNumber, password);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Constructor_WithEmptyName_ThrowsWithCorrectMessage(string? name)
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Customer(Guid.NewGuid(), name!, "anna@firma.se", "0701234567", "5566778899", "secret1"));

        Assert.Equal("Customer name is required", exception.Message);
    }

    // ---------- AC1: valid input ----------

    [Fact]
    public void Constructor_WithValidValues_CreatesCustomer()
    {
        // Act
        var customer = CreateCustomer();

        // Assert
        Assert.Equal("Anna Andersson", customer.Name);
        Assert.Equal("anna@firma.se", customer.EmailAddress);
        Assert.Equal("0701234567", customer.PhoneNumber);
        Assert.Equal("5566778899", customer.OrganizationNumber);
        Assert.Equal("secret1", customer.Password);
    }

    // ---------- Normalization ----------

    [Fact]
    public void Constructor_NormalizesEmail_ToLowercaseWithoutSpaces()
    {
        var customer = CreateCustomer(email: "  ANNA@Firma.SE  ");

        Assert.Equal("anna@firma.se", customer.EmailAddress);
    }

    [Fact]
    public void Constructor_NormalizesOrganizationNumber_RemovesDashAndSpaces()
    {
        var customer = CreateCustomer(orgNumber: "556677-8899");

        Assert.Equal("5566778899", customer.OrganizationNumber);
    }

    [Fact]
    public void Constructor_NormalizesPhoneNumber_RemovesDashAndSpaces()
    {
        var customer = CreateCustomer(phone: "070-123 45 67");

        Assert.Equal("0701234567", customer.PhoneNumber);
    }

    [Fact]
    public void Constructor_TrimsName()
    {
        var customer = CreateCustomer(name: "  Anna  ");

        Assert.Equal("Anna", customer.Name);
    }

    // ---------- AC3: missing or invalid input ----------

    [Fact]
    public void Constructor_WithEmptyId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CreateCustomer(id: Guid.Empty));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("A")]
    public void Constructor_WithMissingOrTooShortName_ThrowsArgumentException(string name)
    {
        Assert.Throws<ArgumentException>(() => CreateCustomer(name: name));
    }

    [Theory]
    [InlineData("")]
    [InlineData("anna.firma.se")]
    public void Constructor_WithMissingOrInvalidEmail_ThrowsArgumentException(string email)
    {
        Assert.Throws<ArgumentException>(() => CreateCustomer(email: email));
    }

    [Fact]
    public void Constructor_WithEmptyPhoneNumber_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CreateCustomer(phone: ""));
    }

    [Theory]
    [InlineData("")]
    [InlineData("12345678")]
    [InlineData("12345678901")]
    public void Constructor_WithInvalidOrganizationNumber_ThrowsArgumentException(string orgNumber)
    {
        Assert.Throws<ArgumentException>(() => CreateCustomer(orgNumber: orgNumber));
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc12")]
    public void Constructor_WithMissingOrTooShortPassword_ThrowsArgumentException(string password)
    {
        Assert.Throws<ArgumentException>(() => CreateCustomer(password: password));
    }
}