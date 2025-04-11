/*
* Author: Steve Bang
* History:
* - [2024-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Domain.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("Phone number cannot be empty");

        // Basic validation - adjust based on requirements
        if (value.Length < 10)
            throw new InvalidOperationException("Phone number is too short");

        Value = value.Trim();
    }

    public static PhoneNumber Create(string phoneNumber) => new(phoneNumber);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}