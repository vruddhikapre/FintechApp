
using System;
using System.Runtime.CompilerServices;

namespace HW.Models
{
    public class Fintech
    {
        /// <summary>
		/// Added getters and setters for all the input var
		/// </summary>
        /// 
        public int Id { get; set; }

        public string Firstname { get; set; } = String.Empty;

        public string Lastname { get; set; } = String.Empty;

        public string Address { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public string Contact { get; set; } = String.Empty;

        public int Age { get; set; }

        public string SSN { get; set; } = String.Empty;

        public double Balance { get; set; }


    }
    public class FinTechModel

    {
        public int Account_number { get; set; }
        public int Id { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public int Expense { get; set; }

        public FinTechModel(int account_number = 10001, string date = "", string category = "", int expense =0)
        {
            Account_number = account_number;
            Date = date;
            Expense = expense;
            Category = category;

        }//This is the constructor for the "FinTechModel" class. It takes in two parameter "month" and "expense"
    }

    public class CustomerService
    {

        public int Id { get; set; }
        public string CustomerServiceDescription { get; set; }
        public string PhoneNumber { get; set; }
        public string Date { get; set; }

        public CustomerService()
        {
            // empty constructor needed for Entity Framework
        }

        public CustomerService(string description, string phoneNumber, string date)
        {
            CustomerServiceDescription = description;
            PhoneNumber = phoneNumber;
            Date = date;
        }

        public CustomerService(CustomerService customerService)
        {
            CustomerServiceDescription = customerService.CustomerServiceDescription;
            PhoneNumber = customerService.PhoneNumber;
            Date = customerService.Date;
        }

    }
}
