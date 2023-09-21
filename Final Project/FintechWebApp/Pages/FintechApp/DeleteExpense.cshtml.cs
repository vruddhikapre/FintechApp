using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using HW.Interfaces;
using HW.Models;
using HW.Repositories;
using System.Text.Json;

namespace FintechWebApp.Pages.FintechApp
{

	/// <summary>
	/// // The public class is defined as a PageModel
	/// </summary>
	public class DeleteExpenseModel : PageModel
	{
		/// <summary>
		/// A public Fintech object is created to represent the monthly expenses.
		/// </summary>
		public FinTechModel monthly_expenses = new();

		// These variables are used to display error or success messages to the user.
		public string errorMessage = "";
		public string successMessage = "";

		/// <summary>
		/// This method is called when the page is loaded with an HTTP GET request. 
		/// </summary>
		public async void OnGet()
		{
			// Retrieve the id parameter from the query string of the request.
			string id = Request.Query["id"];

			// Create a new HttpClient instance
			using (var client = new HttpClient())
			{
				// Set the base address of the API
				client.BaseAddress = new Uri("http://localhost:5264");

				// Send an HTTP DELETE request to the API with the specified id
				var responseTask = client.DeleteAsync("/Fintech/DeleteExpense/" + id);
				responseTask.Wait();

				// If the response is successful, set the Id property of the monthly_expenses object to the id parameter
				if (responseTask.Result.IsSuccessStatusCode)
				{
					monthly_expenses.Id = int.Parse(id);
				}
			}
		}
	}

}



