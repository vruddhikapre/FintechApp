using FinTech.Data;
using FinTech.Models;
using Microsoft.EntityFrameworkCore;

namespace FinTech
{
    public class Seed
    {
        private readonly DataContext dataContext;
        private List<FinTechModel> fintech; /// <summary>

                                            /// Creating a list called bill.
                                            /// </summary>


        public Seed(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        /// <summary>
        /// data context to read and store data into Db initially
        /// </summary>
        public void SeedDataContext()
        {
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



        }
    }
}

