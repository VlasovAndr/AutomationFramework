using Automation.Framework.Common.Models;
using Bogus;

namespace Automation.Framework.Common.Services;

public class DataGeneratorService
{
    private readonly Faker _faker;

    public DataGeneratorService()
    {
        _faker = new Faker();
    }

    public User GenerateRandomUser()
    {
        return new User(
            fullName: _faker.Name.FullName(),
            email: _faker.Internet.Email(),
            currentAddress: _faker.Address.FullAddress(),
            permanentAddress: _faker.Address.FullAddress()
        );
    }
}
