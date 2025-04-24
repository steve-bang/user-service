/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using System.Text.RegularExpressions;

namespace Steve.ManagerHero.UserService.Domain.ValueObjects;

public sealed class EmailAddress : ValueObject
{
    public string Value { get; }
    
    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("Email cannot be empty");
            
        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new InvalidOperationException("Email is invalid");
            
        Value = value.ToLowerInvariant().Trim();
    }

    public static EmailAddress Create(string email) => new(email);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public override string ToString() => Value;
}