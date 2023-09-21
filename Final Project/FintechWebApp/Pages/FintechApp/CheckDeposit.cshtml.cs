using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Newtonsoft.Json;
using System.Text.Json;
using HW.Interfaces;
using HW.Models;
using HW.Repositories;
using HW.Models;
using static System.Net.WebRequestMethods;
using System.Text;

namespace FintechWebApp.Pages.FintechApp
{

	public class CheckDepositModel : PageModel
	{
		public Fintech fintech = new();
		public string errorMessage = "";
		public string successMessage = "";
		/// <summary>
		/// In the OnGet method, the code retrieves the ID of the account
		/// to be edited from the request query parameters.
		/// </summary>

		public async void OnGet()
		{
			//string id = Request.Query["id"];
			//using (var client = new HttpClient())
			//{
			//	client.BaseAddress = new Uri("http://localhost:5264");

			//	// HTTP GET
			//	//var responseTask = client.GetAsync("/Fintech/" + id);
			//	//responseTask.Wait();

			//	//var result = responseTask.Result;
			//	//if (result.IsSuccessStatusCode)
			//	//{
			//	//	var readTask = await result.Content.ReadAsStringAsync();
			//	//	fintech = JsonConvert.DeserializeObject<Fintech>(readTask);
			//	//}
			//}
		}
		/// <summary>
		/// In the OnPost method, the code updates the properties of the 
		/// Fintech object using values from the request form data. 
		/// </summary>
		/// 

		public async Task<IActionResult> OnPostAsync()
		{
			fintech.Id = int.Parse(Request.Form["id"]);

			if (fintech.Id < 1001)
			{
				errorMessage = "Account Id is wrong";
			}
			else
			{
				// retrieve the amount of the check from the HTTP POST request
				double checkAmount = double.Parse(Request.Form["balance"]);

				using (var client = new HttpClient())
				{
					// set the base address for the HTTP client
					client.BaseAddress = new Uri("http://localhost:5264");

					// create a new HTTP PUT request to the "Fintech/DepositChecks" API endpoint with the account id and check amount in the URL
					var result = await client.PutAsync("/Fintech/DepositChecks/" + fintech.Id + "/" + checkAmount, new StringContent("", Encoding.UTF8, "application/json"));
					string resultContent = await result.Content.ReadAsStringAsync();

					// check the response status code for success or failure
					if (!result.IsSuccessStatusCode)
					{
						errorMessage = "Error in Check deposit";
					}
					else
					{
						// update the success message and add the amount to the account balance
						successMessage = "Successfully Deposited";
						fintech.Balance += checkAmount;
					}
				}
			}

			// redirect to the index page
			return RedirectToPage("/Index");
		}

	}
}




