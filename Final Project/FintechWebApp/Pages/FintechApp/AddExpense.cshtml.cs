using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using HW.Interfaces;
using HW.Models;
using HW.Repositories;
using System.Text.Json;

namespace FintechWebApp.Pages.FintechApp
{

	// Define a class named "AddExpenseModel" that inherits from "PageModel"
	public class AddExpenseModel : PageModel
	{
		// Define a public object of type "FinTechModel" named "monthly_expenses" and initialize it with a new instance of the "FinTechModel" class
		public HW.Models.FinTechModel monthly_expenses = new();
        public string errorMessage = "";
        public string successMessage = "";

		// Define an asynchronous method "OnPostAsync" that returns an IActionResult

		public async Task<IActionResult> OnPostAsync()

        {
			// Set the "monthly_expenses" object's properties to values from the form

			monthly_expenses.Account_number = int.Parse(Request.Form["accountnumber"]);
            monthly_expenses.Date = Request.Form["date"];
            monthly_expenses.Category = Request.Form["category"];
            monthly_expenses.Expense = int.Parse(Request.Form["expense"]);

			// Check if the "Expense" property of the "monthly_expenses" object is equal to 0
			if (monthly_expenses.Expense == 0)
			{
				errorMessage = "Add an Expense";
			}
			else
			{
				// Create a new instance of "JsonSerializerOptions" class and set the "WriteIndented" property to true
				var opt = new JsonSerializerOptions() { WriteIndented = true };

				// Serialize the "monthly_expenses" object to a JSON string
				string json = System.Text.Json.JsonSerializer.Serialize<FinTechModel>(monthly_expenses, opt);

				// Create a new instance of "HttpClient" class and set the base address to "http://localhost:5264"
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:5264");

					// Create a new instance of "StringContent" class with JSON data and UTF-8 encoding
					var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

					// Post the content to the "Fintech/AddExpense" endpoint using the "client" object
					var result = await client.PostAsync("Fintech/AddExpense", content);

					// Read the response content as a string
					string resultContent = await result.Content.ReadAsStringAsync();

					Console.WriteLine(resultContent);

					// If the server returns an error, set "errorMessage" to "Error adding"
					if (!result.IsSuccessStatusCode)
					{
						errorMessage = "Error adding";
					}
					else
					{
						// If the server returns a success message, set "successMessage" to "Successfully added"
						successMessage = "Successfully added";
					}
				}
			}

			// Redirect to the "/Pages/ExpenseTracker" page
			return RedirectToPage("/Pages/ExpenseTracker");
		}
	}

}



