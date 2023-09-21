using System;
using System.ComponentModel.DataAnnotations;

namespace FinTech.Models
{

    public class FinTechModel

    {
        public int Account_number { get; set; }
        public int Id { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public int Expense { get; set; }

        public FinTechModel(int account_number = 10001, string date = "", string category = "", int expense = 0)
        {
            Account_number = account_number;
            Date = date;
            Expense = expense;
            Category = category;

        }//This is the constructor for the "Bills" class. It takes in two parameter "month" and "expense"
    }
}


