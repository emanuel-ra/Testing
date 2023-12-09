using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new()
            {
                Id = 1,
                Name = "John Doe",
                Email ="john.doe@example.com",
                Address = new Address()
                {
                    City= "New York",
                    Street= "123 Main Street",
                    ZipCode= "10001"
                }
            },
            new()
            {
                Id = 2,
                Name = "Jane Smith",
                Email ="jane.smith@example.com",
                Address = new Address()
                {
                    City= "Los Angeles",
                    Street= "456 Oak Avenue",
                    ZipCode= "90001"
                }
            } ,
            new()
            {
                Id = 3,
                Name = "Bob Johnson",
                Email ="bob.johnson@example.com",
                Address = new Address()
                {
                    City= "Chicago",
                    Street= "789 Elm Boulevard",
                    ZipCode= "60601"
                }
            }
        };
    }
}
