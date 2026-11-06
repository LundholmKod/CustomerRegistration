using CustomerRegistration.Repositories;
using CustomerRegistration.Services;
using Xunit;

namespace CustomerRegistration.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void RegisterCustomer_ValidDetails_SavesCustomer()
        {
            // Arrange: Förbered repository och service.
            CustomerRepository repository = new CustomerRepository();
            CustomerService service = new CustomerService(repository);

            // Act: Registrera en kund.
            string result = service.RegisterCustomer(
                "Anna Andersson",
                "556123-4567",
                "ANNA@example.se",
                "0701234567");

            // Assert: Kontrollera resultat och sparade uppgifter.
            Assert.Equal("Kundkontot har skapats.", result);

            var customers = repository.GetAll();

            Assert.Single(customers);
            Assert.Equal("Anna Andersson", customers[0].Name);
            Assert.Equal("556123-4567", customers[0].OrganizationNumber);
            Assert.Equal("anna@example.se", customers[0].Email);
            Assert.Equal("0701234567", customers[0].PhoneNumber);
        }

        [Theory]
        [InlineData("", "556123-4567", "anna@example.se", "0701234567")]
        [InlineData("Anna", "", "anna@example.se", "0701234567")]
        [InlineData("Anna", "556123-4567", "", "0701234567")]
        [InlineData("Anna", "556123-4567", "anna@example.se", "")]
        [InlineData("   ", "556123-4567", "anna@example.se", "0701234567")]
        public void RegisterCustomer_MissingDetails_DoesNotSaveCustomer(
            string name,
            string organizationNumber,
            string email,
            string phoneNumber)
        {
            CustomerRepository repository = new CustomerRepository();
            CustomerService service = new CustomerService(repository);

            string result = service.RegisterCustomer(
                name,
                organizationNumber,
                email,
                phoneNumber);

            Assert.Equal("Alla fyra uppgifter måste vara ifyllda.", result);
            Assert.Empty(repository.GetAll());
        }

        [Theory]
        [InlineData("annaexample.se")]
        [InlineData("anna@@example.se")]
        [InlineData("@example.se")]
        [InlineData(".anna@example.se")]
        [InlineData("anna.@example.se")]
        [InlineData("anna..andersson@example.se")]
        [InlineData("anna!@example.se")]
        [InlineData("anna@example")]
        [InlineData("anna@example..se")]
        [InlineData("anna@-example.se")]
        [InlineData("anna@example-.se")]
        [InlineData("anna@exam_ple.se")]
        [InlineData("anna@example.s")]
        [InlineData("anna@example.12")]
        [InlineData("anna andersson@example.se")]
        public void RegisterCustomer_InvalidEmail_DoesNotSaveCustomer(
            string email)
        {
            CustomerRepository repository = new CustomerRepository();
            CustomerService service = new CustomerService(repository);

            string result = service.RegisterCustomer(
                "Anna Andersson",
                "556123-4567",
                email,
                "0701234567");

            Assert.Equal("E-postadressen har ett ogiltigt format.", result);
            Assert.Empty(repository.GetAll());
        }

        [Fact]
        public void RegisterCustomer_DuplicateEmail_DoesNotSaveSecondCustomer()
        {
            CustomerRepository repository = new CustomerRepository();
            CustomerService service = new CustomerService(repository);

            service.RegisterCustomer(
                "Anna Andersson",
                "556123-4567",
                "anna@example.se",
                "0701234567");

            string result = service.RegisterCustomer(
                "Erik Eriksson",
                "556987-6543",
                "ANNA@example.se",
                "0707654321");

            Assert.Equal("E-postadressen är redan registrerad.", result);
            Assert.Single(repository.GetAll());

            // Den befintliga kunden ska vara oförändrad.
            Assert.Equal("Anna Andersson", repository.GetAll()[0].Name);
        }

        [Fact]
        public void RegisterCustomer_InvalidThenValid_SavesOnlyValidCustomer()
        {
            CustomerRepository repository = new CustomerRepository();
            CustomerService service = new CustomerService(repository);

            string firstResult = service.RegisterCustomer(
                "Anna Andersson",
                "556123-4567",
                "anna@@example.se",
                "0701234567");

            Assert.Equal(
                "E-postadressen har ett ogiltigt format.",
                firstResult);

            Assert.Empty(repository.GetAll());

            string secondResult = service.RegisterCustomer(
                "Anna Andersson",
                "556123-4567",
                "anna@example.se",
                "0701234567");

            Assert.Equal("Kundkontot har skapats.", secondResult);
            Assert.Single(repository.GetAll());
        }
    }
}