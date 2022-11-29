using DZN4JR_HFT_2022231.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.client.Infrastructure
{
    public class HttpService
    {
        string controllerName;

        Uri baseAddress;

        JsonSerializerOptions webSpecSerializerOptions;

        public HttpService(string controllerName, Uri baseAddress)
        {
            this.controllerName = controllerName;
            this.baseAddress = baseAddress;
            webSpecSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        }

        public List<T> GetAll<T>(string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.GetAsync(GetActionName(actionName ?? nameof(GetAll))).GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<List<T>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), webSpecSerializerOptions);
            }
        }

        public T Get<T, TKey>(TKey id, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.GetAsync($"{(GetActionName(actionName ?? nameof(Get)))}/{id}").GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), webSpecSerializerOptions);
            }
        }

        public APIResult Create<T>(T model, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.PostAsJsonAsync<T>(GetActionName(actionName ?? nameof(Create)), model).GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<APIResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), webSpecSerializerOptions);
            }
        }

        public APIResult Update<T>(T model, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.PutAsJsonAsync<T>(GetActionName(actionName ?? nameof(Update)), model).GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<APIResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), webSpecSerializerOptions);
            }
        }

        public APIResult Delete<TKey>(TKey id, string actionName = null)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                var response = client.DeleteAsync($"{(GetActionName(actionName ?? nameof(Delete)))}/{id}").GetAwaiter().GetResult();
                return JsonSerializer.Deserialize<APIResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult(), webSpecSerializerOptions);
            }
        }

        #region Utils

        private string GetActionName(string actionName) => $"{controllerName}/{actionName}";

        private void InitClient(HttpClient client)
        {
            // Set base address
            client.BaseAddress = baseAddress;
        }

        #endregion
    }
}
