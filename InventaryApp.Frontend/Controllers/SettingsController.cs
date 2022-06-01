using InventaryApp.Utilities.Logger;
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
    public class SettingsController : Controller
    {
        private readonly IConfiguration _configuration;
        protected readonly string WebApiUrl;
        public SettingsController(IConfiguration configuration)
        {
            _configuration = configuration;
            WebApiUrl = _configuration["AppSettings:WebApiUrl"];
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IDictionary<string, object> settings = new Dictionary<string, object>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("api/Settings");

                    if (response.IsSuccessStatusCode)
                    {
                        var settingsResponse = response.Content.ReadAsStringAsync().Result;
                        settings = JsonSerializer.Deserialize<Dictionary<string, object>>(settingsResponse);
                    }
                }

                return View(settings);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error en vista Index Role");
                return View("~/Views/Shared/Error.cshtml", new Models.ErrorViewModel() { Message = "Error en la consulta de los datos. Revisar web api" });
            }
        }

        public async Task<IActionResult> Post_WebApi(string Settings)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(WebApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var Data = new StringContent(Settings, Encoding.UTF8, "application/json");
                    response = await client.PostAsync("api/Settings", Data);
                    Logger.LogInfo("Respuesta de POST: api/Settings.", response.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error POST api/Settings: ", ex);
                    return StatusCode(500, new { ErrorMessage="Error en la respuesta del web api."});
                }

            }

            return Ok(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<IActionResult> Put_WebApi(string Settings)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(WebApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var Data = new StringContent(Settings, Encoding.UTF8, "application/json");
                    response = await client.PutAsync("api/Settings", Data);
                    Logger.LogInfo("Respuesta de PUT: api/Settings.", response.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error PUT api/Settings: ", ex);
                    return StatusCode(500, new { ErrorMessage = "Error en la respuesta del web api." });
                }

            }

            return Ok(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<IActionResult> Delete_WebApi(string key)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    response = await client.DeleteAsync($"/api/Settings/{key}");
                    Logger.LogInfo($"Error DELETE api/Settings/{key}.", response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error DELETE api/Settings: ", ex);
                return StatusCode(500, new { ErrorMessage = "Error en la respuesta del web api." });
            }

            return Ok(response.Content.ReadAsStringAsync().Result);
        }
    }
}
