using CustomerRegistration.Models;
using CustomerRegistration.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Services;

public class CustomerService
{
    private readonly CustomerRepository customerRepository;

    public CustomerService(CustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public string RegisterCustomer(string name, string organizationNumber, string email, string phoneNumber)

    {
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(organizationNumber) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(phoneNumber))
        {
            return "Alla fyra uppgifter måste vara ifyllda.";
        }

        email = email.ToLowerInvariant();

        if (customerRepository.EmailExists(email))
        {
            return "E-postadressen är redan registrerad.";
        }

        Customer customer = new Customer
        {
            Name = name.Trim(),
            OrganizationNumber = organizationNumber.Trim(),
            Email = email,
            PhoneNumber = phoneNumber.Trim()
        };

        customerRepository.Save(customer);

        return "Kundkontot har skapats.";
    }
}
