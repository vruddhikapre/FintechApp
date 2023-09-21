
using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using HW.Interfaces;
using HW.Models;
using HW.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;


namespace HW.Repositories
{
    /// <summary>
	/// instead of performing the operations directly,
	/// it uses an instance of the FintechRepository interface to perform the operations(read,delete,edit)
	/// </summary>
    public class FintechRepository : IFintechRepository
    {
        private DataContext _context;

        public FintechRepository(DataContext context)
        {
            _context = context;
        }

		/// <summary>
		/// The getItems method reads all the contents of a text file 
		/// </summary>
		/// <returns>returns it in the response.</returns>
		public ICollection<Fintech> getItems()
		{
			return _context.Fintech.ToList();
		}

		/// <summary>
		/// The GetItem method reads all the contents of a text file 
		/// </summary>
		/// <param name="id"></param>
		/// <returns>returns response based on the id</returns>
		public Fintech GetItem(int id)
		{
			return _context.Fintech.Where(account => account.Id == id).FirstOrDefault();
		}

		/// <summary>
		/// The FintechExists method checks if account exists or not 
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Returns true if account exists</returns>
		public bool AccountExists(int id)
		{
			return _context.Fintech.Any(account => account.Id == id);
		}

		/// <summary>
		/// The CustomerService method provides help
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Returns true, provides help</returns>
		public string CustomerService()
		{
			return "Contact Customer Service Phone number at 1-888-888-2400 or Email on help@gmail.com";
		}

		/// <summary>
		/// This method will get customer service request based on id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Customer Service Request</returns>
		public CustomerService GetCustomerServiceById(int id)
		{
			return _context.customerservice.Where(account => account.Id == id).FirstOrDefault();
		}

		/// <summary>
		/// This method will create new Customer Service Request
		/// </summary>
		/// <param name="customerService"></param>
		/// <returns>Returns true and updates in database</returns>
		public bool CreateCustomerService(CustomerService customerService)
		{
			_context.customerservice.AddAsync(customerService);
			return Save();
		}

		/// <summary>
		/// The addItem method adds to the response
		/// </summary>
		/// <param name="account"></param>
		/// <returns>returns true if successfully added</returns>
		public bool AddAccount(Fintech account)
		{
			_context.Add(account);
			return Save();
		}

		/// <summary>
		/// The editItem method updated the content based on id with specified content
		/// </summary>
		/// <param name="account"></param>
		/// <returns>returns true if updated successfully</returns>
		public bool EditAccount(Fintech account)
		{
			_context.Update(account);
			return Save();
		}

		/// <summary>
		/// The deleteItem method deletes request based on the specified id
		/// </summary>
		/// <param name="account"></param>
		/// <returns>returns item deleted and null if not found </returns>
		public bool DeleteAccount(int id)
		{
			var items = _context.Fintech.Where(account => account.Id == id);
			foreach (var account in items)
			{
				_context.Remove(account);
			}

			return Save();
		}



		/// <summary>
		/// The getAllExpenses method reads all the contents of a text file 
		/// </summary>
		/// <returns>returns it in the response.</returns>
		public ICollection<FinTechModel> getAllExpenses()
		{
			return _context.monthly_expenses.ToList();
		}


		/// <summary>
		/// The GetExpese method reads contents of a text file which matches the id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>returns response based on the id</returns>
		public FinTechModel GetExpense(int id)
		{
			return _context.monthly_expenses.Where(expense => expense.Id == id).FirstOrDefault();
		}

		/// <summary>
		/// The addExpense method adds to the response
		/// </summary>
		/// <param name="account"></param>
		/// <returns>returns true if successfully added</returns>
		public bool AddExpense(FinTechModel expense)
		{
			_context.Add(expense);
			return Save();
		}


		/// <summary>
		/// The editItem method updated the content based on id with specified content
		/// </summary>
		/// <param name="account"></param>
		/// <returns>returns true if updated successfully</returns>
		/// <summary>
		public bool UpdateExpense(FinTechModel expense)
		{
			_context.Update(expense);
			return Save();
		}

		/// <summary>
		/// The deleteItem method deletes request based on the specified id
		/// </summary>
		/// <param name="account"></param>
		/// <returns>returns item deleted and null if not found </returns>
		public bool DeleteExpense(int id)
		{
			var items = _context.monthly_expenses.Where(expense => expense.Id == id);
			foreach (var expense in items)
			{
				_context.Remove(expense);
			}

			return Save();
		}

		/// <summary>
		/// Data Analysis method to calculate min,max and average of the amount
		/// </summary>
		/// <returns>Returns min, max,average of the amount </returns>
		public Dictionary<string, dynamic> DataAnalysis()
		{
			List<FinTechModel> data = _context.monthly_expenses.ToList();
			List<int> numbers = data
							.Select(num => num.Expense)
							.ToList();

			Dictionary<string, dynamic> analysis = new();
			// calculate the mean of the numbers
			double mean = numbers.Average();

			// calculate the median of the numbers
			int halfIndex = numbers.Count() / 2;
			var sortedNumbers = numbers.OrderBy(n => n);

			int median;

			if ((numbers.Count() % 2) == 0)
				median = (sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt(halfIndex - 1)) / 2;
			else
				median = sortedNumbers.ElementAt(halfIndex);

			analysis.Add("Mean", mean);
			analysis.Add("Median", median);
			analysis.Add("Most Amount was Spent on ", data.FirstOrDefault(a => a.Expense == numbers.Max())?.Category ?? string.Empty);
			analysis.Add("Amount ", numbers.Max());
			analysis.Add("Least Amount was Spent on", data.FirstOrDefault(a => a.Expense == numbers.Min())?.Category ?? string.Empty);
			analysis.Add("Amount Spent ", numbers.Min());

			return analysis;
		}

		/// <summary>
		/// Creating an API to withdraw money
		/// </summary>
		/// <param name="id"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		public bool WithdrawAmount(int id, double amount)
		{
			var account = _context.Fintech.FirstOrDefault(a => a.Id == id);
			if (account == null)
			{
				return false;
			}

			if (account.Balance < amount)
			{
				return false;
			}

			account.Balance -= amount;
			_context.Update(account);
			return Save();
		}

		/// <summary>
		/// Method to deposit a check in an account
		/// </summary>
		/// <param name="id"></param>
		/// <param name="Check_Amount"></param>
		/// <returns></returns>
		public bool DepositCheck(int id, double Check_Amount)
		{
			var account = _context.Fintech.FirstOrDefault(a => a.Id == id);
			if (account == null)
			{
				return false;
			}
			account.Balance += Check_Amount;
			_context.Update(account);
			return Save();

		}

		/// <summary>
		/// Method to allow transfer of funds between accounts.
		/// </summary>
		/// <param name="A1"></param>
		/// <param name="A2"></param>
		/// <param name="Amount"></param>
		/// <returns></returns>
		public bool TransferAmount(int A1, int A2, double Amount)
		{
			WithdrawAmount(A1, Amount);
			DepositCheck(A2, Amount);

			return Save();

		}

		/// <summary>
		/// Save changes to database
		/// </summary>
		/// <returns>Store in database</returns>
		public bool Save()
		{
			int saved = _context.SaveChanges();
			return saved == 1;

		}


	}

}
