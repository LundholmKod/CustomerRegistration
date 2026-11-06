using System;
using CustomerRegistration.Models;
using CustomerRegistration.Repositories;
using CustomerRegistration.Services;

namespace CustomerRegistration;

internal class Program
{
    static void Main(string[] args)
    {
        CustomerRepository repository = new CustomerRepository();
        CustomerService service = new CustomerService(repository);

        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("1. Registrera kundkonto");
            Console.WriteLine("2. Visa sparade kunder");
            Console.WriteLine("3. Avsluta");
            Console.Write("Välj: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    RegisterCustomer(service);
                    break;

                case "2":
                    ShowCustomers(repository);
                    break;

                case "3":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Välj 1, 2 eller 3.");
                    break;
            }
        }
    }

    static void RegisterCustomer(CustomerService service)
    {
        bool tryAgain = true;

        while (tryAgain)
        {
            Console.WriteLine();
            Console.WriteLine("Registrera kundkonto");
            Console.WriteLine("Skriv /avbryt i valfritt fält för att avbryta.");

            Console.Write("Namn: ");
            string name = Console.ReadLine() ?? "";

            if (IsCancellation(name))
            {
                Console.WriteLine("Registreringen avbröts. Inget konto sparades.");
                return;
            }

            Console.Write("Organisationsnummer: ");
            string organizationNumber = Console.ReadLine() ?? "";

            if (IsCancellation(organizationNumber))
            {
                Console.WriteLine("Registreringen avbröts. Inget konto sparades.");
                return;
            }

            Console.Write("E-postadress: ");
            string email = Console.ReadLine() ?? "";

            if (IsCancellation(email))
            {
                Console.WriteLine("Registreringen avbröts. Inget konto sparades.");
                return;
            }

            Console.Write("Telefonnummer: ");
            string phoneNumber = Console.ReadLine() ?? "";

            if (IsCancellation(phoneNumber))
            {
                Console.WriteLine("Registreringen avbröts. Inget konto sparades.");
                return;
            }

            Console.Write("Skicka registreringen? (j/n): ");
            string confirmation = Console.ReadLine() ?? "";

            if (!string.Equals(confirmation, "j", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Registreringen avbröts. Inget konto sparades.");
                return;
            }

            string result = service.RegisterCustomer(name, organizationNumber, email, phoneNumber);

         
            Console.WriteLine(result);

            if (result == "Kundkontot har skapats.")
            {
                return;
            }

            Console.Write("Vill du ange uppgifterna igen? (j/n): ");
            string answer = Console.ReadLine() ?? "";

            tryAgain = string.Equals(answer, "j", StringComparison.OrdinalIgnoreCase);

            if (!tryAgain)
            {
                Console.WriteLine("Registreringen avslutades utan att skapa ett konto.");
            }
        }
    }

    static bool IsCancellation(string input)
    {
        return string.Equals(input, "/avbryt", StringComparison.OrdinalIgnoreCase);
    }

    static void ShowCustomers(CustomerRepository repository)
    {
        var customers = repository.GetAll();

        Console.WriteLine();
        Console.WriteLine("Antal sparade kunder: " + customers.Count);

        foreach (Customer customer in customers)
        {
            Console.WriteLine(
                customer.Name + " | " +
                customer.OrganizationNumber + " | " +
                customer.Email + " | " +
                customer.PhoneNumber);
        }
    }
}