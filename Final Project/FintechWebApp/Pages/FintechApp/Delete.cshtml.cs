using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using HW.Interfaces;
using HW.Models;
using HW.Repositories;

namespace FintechWebApp.Pages.FintechApp
{
    /// <summary>
    /// The DeleteModel class inherits from the 
    /// PageModel class and contains three public fields: fintech, errorMessage, and successMessage.
    /// </summary>

    public class DeleteModel : PageModel
    {
        public Fintech fintech = new();
        public string errorMessage = "";
        public string successMessage = "";

		/// <summary>
		/// The OnGet method is called when the page is loaded and 
		/// uses the Request.Query property to get the ID of the Fintech Account object to delete.
		/// </summary>
		public async void OnGet()
		{
			// Retrieve the id parameter from the query string
			string id = Request.Query["id"];

			// Create a new HttpClient instance
			using (var client = new HttpClient())
			{
				// Set the base address of the API
				client.BaseAddress = new Uri("http://localhost:5264");

				// Send an HTTP GET request to the API with the specified id
				var responseTask = client.GetAsync("/Fintech/" + id);

				// Wait for the response to be returned
				responseTask.Wait();

				// Get the response object
				var result = responseTask.Result;

				// If the response is successful, deserialize the JSON content into a Fintech object
				if (result.IsSuccessStatusCode)
				{
					var readTask = await result.Content.ReadAsStringAsync();
					fintech = JsonConvert.DeserializeObject<Fintech>(readTask);
				}
			}
		}

		/// <summary>
		/// The OnPost method is called when the user submits the form to delete the Fintech account object.
		/// </summary>
		public async Task<IActionResult> OnPostAsync()
		{
			// Initialize variables
			bool isDeleted = false;
			int id = int.Parse(Request.Form["id"]);

			// Create a new HttpClient instance
			using (var client = new HttpClient())
			{
				// Set the base address of the API
				client.BaseAddress = new Uri("http://localhost:5264");

				// Send an HTTP DELETE request to the API with the specified id
				var response = await client.DeleteAsync("/Fintech/" + id);

				// If the response is successful, set the isDeleted variable to true
				if (response.IsSuccessStatusCode)
				{
					isDeleted = true;
				}
			}

			// If the account was deleted successfully, set the success message and write it to the console. Otherwise, set the error message.
			if (isDeleted)
			{
				successMessage = "Successfully deleted";
				Console.WriteLine("successMessage: " + successMessage);
			}
			else
			{
				errorMessage = "Error deleting";
			}

			// Redirect the user to the Index page
			return RedirectToPage("/Index");
		}

	}
}
