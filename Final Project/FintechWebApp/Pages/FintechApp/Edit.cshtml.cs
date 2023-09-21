using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Newtonsoft.Json;
using System.Text.Json;
using HW.Interfaces;
using HW.Models;
using HW.Repositories;
using HW.Models;
using static System.Net.WebRequestMethods;

namespace FintechWebApp.Pages.FintechApp
{

	// Define a public class named EditModel that inherits from PageModel
	public class EditModel : PageModel
	{
		// Create a new instance of the Fintech class and assign it to the fintech field
		public Fintech fintech = new();
		// Declare two string variables for error and success messages
		public string errorMessage = "";
		public string successMessage = "";

		/// <summary>
		/// In the OnGet method, retrieve the ID of the account
		/// to be edited from the request query parameters.
		/// </summary>
		public async void OnGet()
		{
			// Retrieve the "id" value from the request query parameters
			string id = Request.Query["id"];

			// Create a new HttpClient object and set its BaseAddress
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:5264");

				// Send an HTTP GET request to the "/Fintech/{id}" endpoint
				var responseTask = client.GetAsync("/Fintech/" + id);
				responseTask.Wait();

				var result = responseTask.Result;

				// If the request was successful, deserialize the response JSON
				// into a Fintech object and assign it to the fintech field
				if (result.IsSuccessStatusCode)
				{
					var readTask = await result.Content.ReadAsStringAsync();
					fintech = JsonConvert.DeserializeObject<Fintech>(readTask);
				}
			}
		}

		/// <summary>
		/// In the OnPost method, update the properties of the 
		/// Fintech object using values from the request form data. 
		/// </summary>
		public async Task<IActionResult> OnPostAsync()
		{
			// Update the properties of the fintech object with form data
			fintech.Id = int.Parse(Request.Form["id"]);
			fintech.Firstname = Request.Form["firstname"];
			fintech.Lastname = Request.Form["lastname"];
			fintech.Address = Request.Form["address"];
			fintech.Email = Request.Form["email"];
			fintech.Contact = Request.Form["contact"];
			fintech.Age = int.Parse(Request.Form["age"]);
			fintech.SSN = Request.Form["ssn"];
			fintech.Balance = double.Parse(Request.Form["balance"]);

			// If the firstname field is empty, set an error message
			// Otherwise, serialize the fintech object to JSON and send
			// an HTTP PUT request to the "/Fintech" endpoint with the JSON payload
			if (fintech.Firstname.Length == 0)
			{
				errorMessage = "Firstname is required";
			}
			else
			{
				var opt = new JsonSerializerOptions() { WriteIndented = true };
				string json = System.Text.Json.JsonSerializer.Serialize<Fintech>(fintech, opt);

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:5264");

					var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

					var result = await client.PutAsync("/Fintech", content);
					string resultContent = await result.Content.ReadAsStringAsync();
					Console.WriteLine(resultContent);

					// If the request was not successful, set an error message
					// Otherwise, set a success message
					if (!result.IsSuccessStatusCode)
					{
						errorMessage = "Error editing";
					}
					else
					{
						successMessage = "Successfully editing";
					}

				}

			}

			// Redirect to the "/Index" page
			return RedirectToPage("/Index");
		}

	}
}
