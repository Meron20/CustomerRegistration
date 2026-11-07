
namespace CustomerRegistration.Domain.Customers;

public class Customer(Guid id, string name, string emailAddress, string phoneNumber, string organizationNumber, string password)
{
    public Guid Id { get; private set; } = ValidateId(id);
    public string Name { get; private set; } = ValidateAndNormalizeName(name);
    public string EmailAddress { get; private set; } = ValidateAndNormalizeEmailAddress(emailAddress);
    public string PhoneNumber { get; private set; } = ValidateAndNormalizePhoneNumber(phoneNumber);
    public string OrganizationNumber { get; private set; } = ValidateAndNormalizeOrganizationNumber(organizationNumber);
    public string Password { get; private set; } = ValidateAndNormalizePassword(password);

    private static Guid ValidateId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Customer id is required");

        return id;
    }

    private static string ValidateAndNormalizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Customer name is required");

        if (name.Trim().Length < 2)
            throw new ArgumentException("Customer name must be a valid name and contain at least 2 letters");

        return name.Trim();
    }
    private static string ValidateAndNormalizeEmailAddress(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
            throw new ArgumentException("Email address is required");

        if (!emailAddress.Contains('@'))
            throw new ArgumentException("Email must be a valid email address");

        return emailAddress.Trim().ToLower();
    }

    private static string ValidateAndNormalizePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required");

        return phoneNumber.Trim().Replace("-", "").Replace(" ", "");
    }

    private static string ValidateAndNormalizeOrganizationNumber(string organizationNumber)
    {
        if (string.IsNullOrWhiteSpace(organizationNumber))
            throw new ArgumentException("Organization number is required");

     var normalized = organizationNumber.Trim().Replace("-", "").Replace(" ", "");

     if (normalized.Length != 10)
         throw new ArgumentException("Organization number must contain 10 digits.");

        return normalized;

    }
    private static string ValidateAndNormalizePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is required");

        if (password.Length < 6)
            throw new ArgumentException("Password must be at least 6 characters.");

        return password;
    }


}


