using InventaryApp.Utilities.Logger;
using InventaryApp.Utilities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventaryApp.Frontend.Controllers
{
    public class RoleController : Controller
    {
        private readonly IConfiguration _configuration;
        protected readonly string WebApiUrl;
        public RoleController(IConfiguration configuration)
        {
            _configuration = configuration;
            WebApiUrl = _configuration["AppSettings:WebApiUrl"];
        }
        public async Task<IActionResult> Index()
        {
            List<Role> role = new List<Role>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Role");

                if (response.IsSuccessStatusCode)
                {
                    var RoleResponse = response.Content.ReadAsStringAsync().Result;
                    role = JsonSerializer.Deserialize<List<Role>>(RoleResponse);
                }
            }

            return View(role);
        }

        public async Task<IActionResult> Post(string Role)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(WebApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var Data = new StringContent(Role, Encoding.UTF8, "application/json");
                    response = await client.PostAsync("api/Role", Data);
                    Logger.LogInfo("Respuesta de POST: api/role.", response.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error POST api/Role: ", ex);
                }

            }

            return Ok(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<IActionResult> Put(string Role)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(WebApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var Data = new StringContent(Role, Encoding.UTF8, "application/json");
                    response = await client.PutAsync("api/Role", Data);
                    Logger.LogInfo("Respuesta de PUT: api/role.", response.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error PUT api/Role: ", ex);
                }

            }

            return Ok(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<IActionResult> Delete(string Role_Guid)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(WebApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    response = await client.DeleteAsync($"api/Role/{Role_Guid}");
                    Logger.LogInfo($"Respuesta de DELETE: api/role/{Role_Guid}.", response.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error DELETE api/role/{Role_Guid}: ", ex);
                }

            }

            return Ok(response.Content.ReadAsStringAsync().Result);
        }
    }
}
