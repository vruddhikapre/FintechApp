using HW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FintechWebApp.Pages.FintechApp
{

	// This is a C# code for a TransferFundsModel class that inherits from PageModel
	public class TransferFundsModel : PageModel
	{
		// Creating a new instance of the Fintech class and initializing it
		public Fintech fintech = new();
		// Declaring two strings to hold error and success messages
		public string errorMessage = "";
		public string successMessage = "";

		/// <summary>
		/// In the OnPost method, the code updates the properties of the 
		/// Fintech object using values from the request form data. 
		/// </summary>

		// An asynchronous method that returns IActionResult
		public async Task<IActionResult> OnPostAsync()
		{
			// Parsing the account Id from the request form data and storing it in the Fintech object
			fintech.Id = int.Parse(Request.Form["id"]);

			// Checking if the account Id is less than 1001 and setting an error message if it is
			if (fintech.Id < 1001)
			{
				errorMessage = "Account Id is wrong";
			}
			else
			{
				// Parsing the withdrawal amount from the request form data
				double withdrawAmount = double.Parse(Request.Form["balance"]);

				// Creating a new instance of HttpClient and setting its base address
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:5264");

					// Creating a request body to send with the Put request
					var requestContent = new StringContent(JsonConvert.SerializeObject(withdrawAmount), Encoding.UTF8, "application/json");

					// Sending a Put request to the Fintech API to deposit the check
					var result = await client.PutAsync("/Fintech/DepositChecks/" + fintech.Id + "/" + withdrawAmount, requestContent);

					// Reading the response content
					string resultContent = await result.Content.ReadAsStringAsync();

					// Checking if the response is successful and setting appropriate messages and updating the balance
					if (!result.IsSuccessStatusCode)
					{
						errorMessage = "Error in Check deposit";
					}
					else
					{
						successMessage = "Successfully Deposited";
						fintech.Balance += withdrawAmount;
					}
				}
			}
			// Returning the page
			return Page();
		}
	}
}




