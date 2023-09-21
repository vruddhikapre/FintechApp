using System;
using HW.Models;
namespace HW.Interfaces
{
    public interface IFintechRepository
    {
        ICollection<Fintech> getItems(); //Method to retrieve all Fintech items

		Fintech GetItem(int id); //Method to retrieve a Fintech item by ID

		bool AccountExists(int id); //Method to check if a Fintech account with a given ID exists

		string CustomerService(); //Method to get customer service details

		CustomerService GetCustomerServiceById(int id); //Method to retrieve a customer service record by ID

		bool CreateCustomerService(CustomerService customerservice); //Method to create a new customer service record

		bool AddAccount(Fintech account); //Method to add a new Fintech account

		bool EditAccount(Fintech account); //Method to edit an existing Fintech account

		bool DeleteAccount(int id); //Method to delete a Fintech account by ID

		ICollection<FinTechModel> getAllExpenses(); //Method to retrieve all FinTechModel expenses

		FinTechModel GetExpense(int id); //Method to retrieve a FinTechModel expense by ID

		bool AddExpense(FinTechModel expense); //Method to add a new FinTechModel expense

		bool UpdateExpense(FinTechModel expense); //Method to update an existing FinTechModel expense

		bool DeleteExpense(int id); //Method to delete a FinTechModel expense by ID

		//Dictionary<string, dynamic> analyzeBill(); //Method to analyze a bill (commented out)

		Dictionary<string, dynamic> DataAnalysis(); //Method to perform data analysis

		bool WithdrawAmount(int id, double amount); //Method to withdraw money from a Fintech account

		bool DepositCheck(int id, double Check_Amount); //Method to deposit a check into a Fintech account

		bool TransferAmount(int A1, int A2, double Amount); //Method to transfer money between two Fintech accounts

		bool Save(); //Method to save changes to the database
	}

}
}