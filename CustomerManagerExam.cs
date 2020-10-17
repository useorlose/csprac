using System;

namespace Exam
{
    class GetData
    {
        internal static double getDouble(string question)
        {
            Console.WriteLine(question);
            return double.Parse(Console.ReadLine());
        }

        internal static string getString(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        internal static int getNumber(string question)
        {
            return int.Parse(getString(question));
        }

        internal static DateTime getDate(string question)
        {
            return DateTime.Parse(getString(question));
        }
    }
    interface ICustomerManager
    {
        bool AddCustomer(Customer bk);
        bool DeleteCustomer(int id);
        bool UpdateCustomer(Customer bk);
        Customer[] SearchByName(string name);
    }
    class CustomerManager : ICustomerManager
    {

        private Customer[] allCustomer = null;

        private bool hasCustomer(int id)
        {
            foreach (Customer bk in allCustomer)
            {
                if ((bk != null) && (bk.CustomerID == id))
                    return true;
            }
            return false;
        }
        //Function that is invoked when the object is created
        public CustomerManager(int size)
        {
            allCustomer = new Customer[size];
        }
        public bool AddCustomer(Customer bk)
        {
            bool available = hasCustomer(bk.CustomerID);
            if (available)
                throw new Exception("Customer by this ID already exists");
            for (int i = 0; i < allCustomer.Length; i++)
            {
                //find the first occurance of null in the array...
                if (allCustomer[i] == null)
                {
                    allCustomer[i] = new Customer();
                    allCustomer[i].CustomerID = bk.CustomerID;
                    allCustomer[i].CustomerName = bk.CustomerName;
                    allCustomer[i].Address = bk.Address;
                    allCustomer[i].Salary = bk.Salary;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteCustomer(int id)
        {
            for (int i = 0; i < allCustomer.Length; i++)
            {
                //find the first occurance of null in the array...
                if ((allCustomer[i] != null) && (allCustomer[i].CustomerID == id))
                {
                    allCustomer[i] = null;
                    return true;
                }
            }
            return false;
        }

        public Customer[] SearchByName(string name)
        {
            //create a blank array of customers of the same size as the original.
            Customer[] copy = new Customer[allCustomer.Length];
            //iterate thro the original to find the matching Customer.
            int index = 0;
            foreach (Customer bk in allCustomer)
            {
                //if found add the Customer to the copy...
                if ((bk != null) && (bk.CustomerName.Contains(name)))
                {
                    copy[index] = bk;
                    index++;
                }
            }

            //return the copy...
            return copy;
        }

        public bool UpdateCustomer(Customer bk)
        {
            for (int i = 0; i < allCustomer.Length; i++)
            {
                //find the first occurance of null in the array...
                if ((allCustomer[i] != null) && (allCustomer[i].CustomerID == bk.CustomerID))
                {
                    allCustomer[i].CustomerName = bk.CustomerName;
                    allCustomer[i].Address = bk.Address;
                    allCustomer[i].Salary = bk.Salary;
                    return true;
                }
            }
            return false;
        }
    }
    class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
    }

    //Clients should not instantiate the object. they will call our factory method to get the type of the object they want...
    class CustomerFactoryComponent
    {
        public static ICustomerManager GetComponent(int size)
        {
            return new CustomerManager(size);
        }
    }
    class CustomerManagerExam
    {
        static string menu = string.Empty;
        static ICustomerManager mgr = null;
        static void InitializeComponent()
        {
            menu = string.Format($"~~~~~~~ CUSTOMER MANAGEMENT ~~~~~~~\n1. ADD CUSTOMER\n2. UPDATE CUSTOMER\n3. DELETE CUSTOMER\n4. SEARCH BY NAME\nPRESS ANY OTHER KEY TO EXIT\n");
            int size = GetData.getNumber("Enter the size of Customer Array");
            mgr = CustomerFactoryComponent.GetComponent(size);
        }

        static void Main(string[] args)
        {
            InitializeComponent();
            bool @continue = true;
            do
            {
                string choice = GetData.getString(menu);
                @continue = processing(choice);
            } while (@continue);
        }

        private static bool processing(string choice)
        {
            switch (choice)
            {
                case "1":
                    addingCustomerFeature();
                    break;
                case "2":
                    updatingCustomerFeature();
                    break;
                case "3":
                    deletingFeature();
                    break;
                case "4":
                    readingFeature();
                    break;
                default:
                    return false;
            }
            return true;
        }

        private static void readingFeature()
        {
            string name = GetData.getString("Enter the Name or part of the Name to search");
            Customer[] customers = mgr.SearchByName(name);
            foreach (var bk in customers)
            {                if (bk != null)
                {
                    Console.WriteLine("--------------");
                    Console.WriteLine(bk.CustomerID);
                    Console.WriteLine(bk.CustomerName);
                    Console.WriteLine(bk.Address);
                    Console.WriteLine(bk.Salary);
                    Console.WriteLine("--------------");
                }
            }
        }

        private static void deletingFeature()
        {
            int id = GetData.getNumber("Enter the ID of the Customer to remove");
            if (mgr.DeleteCustomer(id))
                Console.WriteLine("Customer Deleted successfully");
            else
                Console.WriteLine("Could not find the Customer to delete");
        }

        private static void updatingCustomerFeature()
        {
            Customer bk = new Customer();
            bk.CustomerID = GetData.getNumber("Enter the ID of the Customer to update");
            bk.CustomerName = GetData.getString("Enter the new name of the Customer");
            bk.Address = GetData.getString("Enter the new Address of the Customer");
            bk.Salary = GetData.getDouble("Enter the new Salary of the Customer");
            bool result = mgr.UpdateCustomer(bk);
            if (!result)
                Console.WriteLine($"No Customer by this id {bk.CustomerID} found to update");
            else
                Console.WriteLine($"Customer by ID {bk.CustomerID} is updated successfully to the database");
        }

        private static void addingCustomerFeature()
        {
            Customer bk = new Customer();
            bk.CustomerID = GetData.getNumber("Enter the ID of the Customer");
            bk.CustomerName = GetData.getString("Enter the Name of the Customer");
            bk.Address = GetData.getString("Enter the Address of the Customer");
            bk.Salary = GetData.getDouble("Enter the Salary of the Customer");
            try
            {
                bool result = mgr.AddCustomer(bk);
                if (!result)
                    Console.WriteLine("No more customers could be added");
                else
                    Console.WriteLine($"Customer by name {bk.CustomerName} is added successfully to the database");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}