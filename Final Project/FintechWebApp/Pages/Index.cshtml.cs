using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using HW.Interfaces;
using HW.Models;
using HW.Repositories;

namespace FintechWebApp.Pages.FintechApp
{
    public class IndexModel : PageModel
    {
        public List<Fintech> fintech = new();
        /// <summary>
        /// The OnGet method is responsible for getting the data for this property 
        /// from the API using HTTP GET request
        /// </summary>
        public async void OnGet()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5264");
                //HTTP GET
                var responseTask = client.GetAsync("/Fintech");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    fintech = JsonConvert.DeserializeObject<List<Fintech>>(readTask);
                }
            }
        }
    }
}
