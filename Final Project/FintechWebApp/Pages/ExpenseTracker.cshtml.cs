using HW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FintechWebApp.Pages
{
    public class ExpenseTrackerModel : PageModel
    {
		public List<FinTechModel> monthly_expenses  = new();

		public async void OnGet()
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:5264");
				//HTTP GET
				var responseTask = client.GetAsync("/Fintech/GetAllExpenses");
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var readTask = await result.Content.ReadAsStringAsync();
					monthly_expenses = JsonConvert.DeserializeObject<List<FinTechModel>>(readTask);
				}
			}
		}
	}
}
