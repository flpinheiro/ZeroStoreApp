using System.Text.RegularExpressions;

namespace ZeroStoreApp.Domain.ValueObjects;

public record Email : IEquatable<Email>
{
    private readonly string _value;

    private Email(string value)
    {
        _value = value;
    }

    public string Value => _value;

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email cannot be empty or whitespace.", nameof(value));
        }

        if (!IsValidEmail(value))
        {
            throw new ArgumentException("Invalid email format.", nameof(value));
        }

        return new Email(value);
    }

    private static bool IsValidEmail(string email) 
        => EmailValidation.EmailValidator.Validate(email);

    public override string ToString() => _value;

    public virtual bool Equals(Email? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(_value, other._value, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode() => _value.GetHashCode();

    // Example of implicit conversion (use with caution - can make code harder to reason about)
    /// <summary>
    /// implicit conversion (use with caution - can make code harder to reason about)
    /// </summary>
    /// <param name="email"></param>
    public static implicit operator string(Email email) => email.Value;

    // Example of explicit conversion
    /// <summary>
    /// explicit conversion
    /// </summary>
    /// <param name="emailString"></param>
    public static explicit operator Email(string emailString) => Email.Create(emailString);
}

