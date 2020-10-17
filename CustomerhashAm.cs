using System;
using System.Collections.Generic;

namespace Assessment
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public double CustomerSalary { get; set; }

        public Customer(int CustomerID, string CustomerName, string CustomerAddress, double CustomerSalary)
        {
            this.CustomerID = CustomerID;
            this.CustomerName = CustomerName;
            this.CustomerAddress = CustomerAddress;
            this.CustomerSalary = CustomerSalary;
        }
    }
    interface ICustomerManager
    {
        bool AddCustomer(Customer bk);
        bool DeleteCustomer(int id);
        bool UpdateCustomerDetails(int id);
        bool ViewAllCustomer();
    }
    class CustomerManager : ICustomerManager
    {
        HashSet<Customer> customer = new HashSet<Customer>();
        public bool AddCustomer(Customer cust)
        {
            return customer.Add(cust);
        }

        public bool ViewAllCustomer()
        {
            int i = 1;
            Console.WriteLine("---------- All Customer Details-----------");
            foreach (var cust in customer)
            {
                Console.WriteLine($"------------------S.No: {i}-----------------------");
                Console.WriteLine($"Customer ID: {cust.CustomerID}");
                Console.WriteLine($"Customer Name: {cust.CustomerName}");
                Console.WriteLine($"Customer Address: {cust.CustomerAddress}");
                Console.WriteLine($"Customer Salary: {cust.CustomerSalary}");
                Console.WriteLine("------------------------------------------");
                i++;
            }
            return true;
        }

        public bool UpdateCustomerDetails(int id)
        {
            foreach (Customer cust in customer)
            {
                if (cust.CustomerID == id)
                {
                    Console.Write("Enter the Customer name to update: ");
                    string NewCustomerName = Console.ReadLine();
                    Console.Write("Enter the Customer Address to update: ");
                    string NewCustomerAddress = Console.ReadLine();
                    Console.Write("Enter the Customer Salary to update: ");
                    double NewCustomerSalary = double.Parse(Console.ReadLine());
                    cust.CustomerName = NewCustomerName;
                    cust.CustomerAddress = NewCustomerAddress;
                    cust.CustomerSalary = NewCustomerSalary;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteCustomer(int id)
        {
            foreach (Customer cust in customer)
            {
                if (cust.CustomerID == id)
                {
                    customer.Remove(cust);
                    return true;
                }
            }
            return false;
        }


        public static object getInteger(string v)
        {
            Console.WriteLine(v);
            return (Console.ReadLine());
        }
    }

    class AllCustomerManager
    {
        static void Main(string[] args)
        {
            Customer customer1 = new Customer(100, "Sachin Kumar", "kolkata", 2500000);
            Customer customer2 = new Customer(100, "Akash", "Mysore", 8000000);
            Customer customer3 = new Customer(112, "Ayush", "Rajasthan", 7000000);

            CustomerManager mgr = new CustomerManager();

            mgr.AddCustomer(customer1);
            mgr.AddCustomer(customer2);
            mgr.AddCustomer(customer3);

            Console.WriteLine("Details of all the customers: ");
            mgr.ViewAllCustomer();

            Console.WriteLine("Which Customer you want to update");
            int choice = int.Parse(Console.ReadLine());
            mgr.UpdateCustomerDetails(choice);
            mgr.ViewAllCustomer();
        }

    }
}


