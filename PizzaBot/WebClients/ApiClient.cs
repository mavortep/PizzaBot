using Newtonsoft.Json;
using PizzaBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PizzaBot.WebClients
{
    public class ApiClient
    {
        static HttpClient client = new HttpClient();

        static ApiClient()
        {
            client.BaseAddress = new Uri("http://localhost:52195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<IEnumerable<Product>> GetProductAsync(string path)
        {
            string contents = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                contents = await response.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<IEnumerable<Product>>(contents);
        }

        public static async Task<IEnumerable<Size>> GetSizeAsync(string path)
        {
            string contents = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                contents = await response.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<IEnumerable<Size>>(contents);
        }

        public static async Task<Uri> PostHistoryAsync(History history)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Histories", history);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }
    }
}