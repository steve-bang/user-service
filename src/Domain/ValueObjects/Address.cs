/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.ValueObjects;

public sealed class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string CountryCode { get; }

    public Address(
        string street,
        string city,
        string state,
        string zipCode,
        string countryCode)
    {
        Street = street ?? throw new InvalidOperationException("Street cannot be empty");
        City = city ?? throw new InvalidOperationException("City cannot be empty");
        State = state ?? throw new InvalidOperationException("State cannot be empty");
        ZipCode = zipCode ?? throw new InvalidOperationException("Zip code cannot be empty");
        CountryCode = countryCode ?? throw new InvalidOperationException("Country code cannot be empty");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return ZipCode;
        yield return CountryCode;
    }

    public override string ToString() =>
        $"{Street}, {City}, {State} {ZipCode}, {CountryCode}";
}