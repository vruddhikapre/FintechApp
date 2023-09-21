using System;
using HW.Models;
namespace HW.Interfaces
{
    public interface IFintechRepository
    {

        /// <summary>
        /// Getting all the accounts details.
        /// </summary>
        /// <returns></returns>
        ICollection<Fintech> getItems();
        /// <summary>
        /// GEtting a single Account by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Fintech GetItem(int id);

        /// <summary>
        /// Checking if an account exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool AccountExists(int id);

        /// <summary>
        /// Customer service Details
        /// </summary>
        /// <returns></returns>
        string CustomerService();

        /// <summary>
        /// Checking already created request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CustomerService GetCustomerServiceById(int id);

        /// <summary>
        /// Creating a Customer service request
        /// </summary>
        /// <param name="customerservice"></param>
        /// <returns></returns>
        bool CreateCustomerService(CustomerService customerservice);

        /// <summary>
        /// Adding a new Bank Account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool AddAccount(Fintech account);

        /// <summary>
        /// Updating Bank Account Customer Details
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool EditAccount(Fintech account);

        /// <summary>
        /// Deleting a Bank Account based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteAccount(int  id);

        /// <summary>
        /// Getting all the expenses from the database
        /// </summary>
        /// <returns></returns>
        ICollection<FinTechModel> getAllExpenses();
        /// <summary>
        /// Getting a single expense by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FinTechModel GetExpense(int id);

        /// <summary>
        ///  Adding an Expense.
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        bool AddExpense(FinTechModel expense);

        /// <summary>
        /// Updating an Expense.
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        bool UpdateExpense(FinTechModel expense);

        /// <summary>
        /// Deleting an Expense.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteExpense(int id);

        /// <summary>
        /// Analysing the expenses.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, dynamic> analyzeBill();

        /// <summary>
        /// Withdrawing money function
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        bool WithdrawAmount(int id, double amount);

        /// <summary>
        /// Depositing Checks
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Check_Amount"></param>
        /// <returns></returns>
        bool DepositCheck (int id, double Check_Amount);

        /// <summary>
        /// A1 is account 1 from where amount is debited and account 2 is account where amount needs to be deposited
        /// </summary>
        /// <param name="A1"></param>
        /// <param name="A2"></param>
        /// <returns></returns>
        bool TransferAmount(int A1, int A2, double Amount);

        /// <summary>
        /// Save changes to databse
        /// </summary>
        /// <returns></returns>
        bool Save();
    }
}