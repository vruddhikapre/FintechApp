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
        public class CreateModel : PageModel
        {
            /// <summary>
            ///  A public Fintech object is created
            /// </summary>
            public HW.Models.Fintech fintech = new();
            public string errorMessage = "";
            public string successMessage = "";

		/// <summary>
		/// 
		///  The OnPost method is defined as an async void method
		/// </summary>

		public async Task<IActionResult> OnPostAsync()
		{
			// The values from the form are set to the fintech object
			fintech.Firstname = Request.Form["firstname"];
			fintech.Lastname = Request.Form["lastname"];
			fintech.Address = Request.Form["address"];
			fintech.Email = Request.Form["email"];
			fintech.Contact = Request.Form["contact"];
			fintech.Age = int.Parse(Request.Form["age"]);
			fintech.SSN = Request.Form["ssn"];
			fintech.Balance = double.Parse(Request.Form["balance"]);

			// If the first name is empty, an error message is displayed, else JSON data is created and sent to the server
			if (fintech.Firstname.Length == 0)
			{
				errorMessage = "Firstname is required";
			}
			else
			{
				// Creates JSON string from fintech object
				var opt = new JsonSerializerOptions() { WriteIndented = true };
				string json = System.Text.Json.JsonSerializer.Serialize<Fintech>(fintech, opt);

				using (var client = new HttpClient())
				{
					// Sets the base address of the HttpClient instance
					client.BaseAddress = new Uri("http://localhost:5264");

					// Encodes the JSON string and sets the content type to "application/json"
					var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

					// Sends a POST request to the server and waits for the response
					var result = await client.PostAsync("Fintech", content);
					string resultContent = await result.Content.ReadAsStringAsync();
					Console.WriteLine(resultContent);

					// If the server returns an error, an error message is displayed
					if (!result.IsSuccessStatusCode)
					{
						errorMessage = "Error adding";
					}
					else
					{
						// If the server returns a success message, a success message is displayed
						successMessage = "Successfully added";
					}
				}
			}

			// Redirects to the "Index" page
			return RedirectToPage("/Index");
		}

	}
}

