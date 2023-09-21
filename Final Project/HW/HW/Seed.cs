using HW.Data;
using HW.Models;
using System;

public class Seed
{
    /// <summary>
    /// 
    ///  Seed method in C# is typically used as a method to populate data when the database is first created. 
	///  Method is called in the context of a database migration or setup process.
    /// </summary>
    private readonly DataContext dataContext;
    public Seed(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }
    public void SeedDataContext()
    {
        if (!dataContext.Fintech.Any())
        {
            //Account data
            List<Fintech> bill = new()
            {
                new Fintech {Id = 9, Firstname = "Likhita", Lastname = "B", Address = "Seattle", Email = "likithab@gmail.com",Contact ="2602154789", Age = 25, SSN="0145786545",Balance=30.0},
                new Fintech {Id = 9, Firstname = "Anuradha", Lastname = "K", Address = "New York", Email = "anuk@gmail.com",Contact ="7845124245", Age = 23, SSN="4587891254",Balance=30.0}
            };
            dataContext.Fintech.AddRange(bill);
            dataContext.SaveChanges();
        }
        if (!dataContext.monthly_expenses.Any())
        {
            List<FinTechModel> fintech = new()
                {
                    new FinTechModel {Account_number = 10001, Id = 1, Date = "2023-07-03", Category ="Grocery", Expense = 230},
                    new FinTechModel {Account_number = 10001, Id = 2, Date = "2023-07-03", Category ="Gas", Expense = 40},

                };


            dataContext.monthly_expenses.AddRange(fintech);//Saving contents into the database
                                                           //dataContext.SaveChanges();

        }
        if (!dataContext.customerservice.Any())
        {
            List<CustomerService> customerservice = new()
                {
                    new CustomerService {Id = 1 ,CustomerServiceDescription = "Withdraw Issue", PhoneNumber = "2546548796", Date = "2023-02-12"},
                    new CustomerService {Id = 2 ,CustomerServiceDescription = "Transfer Issue", PhoneNumber = "2604514789", Date = "2023-03-24"},

                };


            dataContext.customerservice.AddRange(customerservice);//Saving contents into the database
                                                           //dataContext.SaveChanges();

        }
    }

}
