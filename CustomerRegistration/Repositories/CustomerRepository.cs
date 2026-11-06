using CustomerRegistration.Models;

namespace CustomerRegistration.Repositories;

public class CustomerRepository
{
    private readonly List<Customer> customers = new List<Customer>();

    public bool EmailExists(string email)
    {
        foreach (Customer customer in customers)
        {
            if (string.Equals(customer.Email, email, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
                

        }

        return false;
    }

    public void Save(Customer customer)
    {
        customers.Add(customer);
    }

    public List<Customer> GetAll()
    {
        return new List<Customer>(customers);
    }

    
}
