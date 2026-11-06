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
        if (!IsValidEmail(email))
        {
            return "E-postadressen har ett ogiltigt format.";
        }

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
    private bool IsValidEmail(string email)
    {
        string[] parts = email.Split('@');

        if (parts.Length != 2)
        {
            return false;
        }

        string localPart = parts[0];
        string domain = parts[1];

        if (!IsValidLocalPart(localPart))
        {
            return false;
        }

        if (!IsValidDomain(domain))
        {
            return false;
        }

        return true;
    }

    private bool IsValidLocalPart(string localPart)
    {
        if (localPart.Length == 0)
        {
            return false;
        }

        if (localPart.StartsWith(".") ||
            localPart.EndsWith(".") ||
            localPart.Contains(".."))
        {
            return false;
        }

        string allowedCharacters =
            "abcdefghijklmnopqrstuvwxyz0123456789._-+";

        foreach (char character in localPart)
        {
            if (!allowedCharacters.Contains(character))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsValidDomain(string domain)
    {
        string[] domainParts = domain.Split('.');

        if (domainParts.Length < 2)
        {
            return false;
        }

        string allowedCharacters =
            "abcdefghijklmnopqrstuvwxyz0123456789-";

        foreach (string part in domainParts)
        {
            if (part.Length == 0)
            {
                return false;
            }

            if (part.StartsWith("-") || part.EndsWith("-"))
            {
                return false;
            }

            foreach (char character in part)
            {
                if (!allowedCharacters.Contains(character))
                {
                    return false;
                }
            }
        }

        string lastPart = domainParts[domainParts.Length - 1];

        if (lastPart.Length < 2)
        {
            return false;
        }

        string letters = "abcdefghijklmnopqrstuvwxyz";

        foreach (char character in lastPart)
        {
            if (!letters.Contains(character))
            {
                return false;
            }
        }

        return true;
    }
}

