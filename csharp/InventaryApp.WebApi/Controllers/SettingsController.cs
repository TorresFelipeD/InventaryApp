using InventaryApp.Utilities.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InventaryApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        IConfiguration _configurarion;

        public SettingsController(IConfiguration configurarion)
        {
            _configurarion = configurarion;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Dictionary<string, object> dicAppSettings = new Dictionary<string, object>();

            try
            {
                using (StreamReader r = new StreamReader("appsettings.json"))
                {
                    string json = r.ReadToEnd();
                    var dictJson = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                    if (!dictJson.ContainsKey("AppSettings"))
                        return BadRequest();

                    string jsonAppSettingsStr = JsonSerializer.Serialize(dictJson["AppSettings"]);
                    dicAppSettings = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonAppSettingsStr);
                }

                return Ok(dicAppSettings);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error en Settings: Get");
                return StatusCode(500);
            }

        }

        [HttpPost]
        public IActionResult Post(Dictionary<string, string> dicSettings)
        {
            IDictionary<string, object> dicAppSettings = new Dictionary<string, object>();
            IDictionary<string, object> dictJson = new Dictionary<string, object>();
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                dictJson = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                if (!dictJson.ContainsKey("AppSettings"))
                    return BadRequest();

                string jsonAppSettingsStr = JsonSerializer.Serialize(dictJson["AppSettings"]);
                dicAppSettings = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonAppSettingsStr);
            }

            foreach (var item in dicSettings)
            {
                if (!dicAppSettings.ContainsKey(item.Key))
                {
                    dicAppSettings.Add(item.Key, item.Value);
                }
            }

            dictJson["AppSettings"] = dicAppSettings;

            using (StreamWriter writer = System.IO.File.CreateText("appsettings.json"))
            {
                writer.Write(JsonSerializer.Serialize(dictJson, jsonSerializerOptions));
            }

            return Ok(dictJson);
        }

        [HttpPut]
        public IActionResult Put(Dictionary<string, string> dicSettings)
        {
            IDictionary<string, object> dicAppSettings = new Dictionary<string, object>();
            IDictionary<string, object> dictJson = new Dictionary<string, object>();
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                dictJson = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                if (!dictJson.ContainsKey("AppSettings"))
                    return BadRequest();

                string jsonAppSettingsStr = JsonSerializer.Serialize(dictJson["AppSettings"]);
                dicAppSettings = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonAppSettingsStr);
            }

            foreach (var item in dicSettings)
            {
                if (dicAppSettings.ContainsKey(item.Key))
                {
                    dicAppSettings[item.Key] = item.Value;
                }
            }

            dictJson["AppSettings"] = dicAppSettings;

            using (StreamWriter writer = System.IO.File.CreateText("appsettings.json"))
            {
                writer.Write(JsonSerializer.Serialize(dictJson, jsonSerializerOptions));
            }

            return Ok(dictJson);
        }

        [HttpDelete("{Key}")]
        public IActionResult Delete(string Key)
        {
            IDictionary<string, object> dicAppSettings = new Dictionary<string, object>();
            IDictionary<string, object> dictJson = new Dictionary<string, object>();
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                dictJson = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                if (!dictJson.ContainsKey("AppSettings"))
                    return BadRequest();

                string jsonAppSettingsStr = JsonSerializer.Serialize(dictJson["AppSettings"]);
                dicAppSettings = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonAppSettingsStr);
            }

            if (dicAppSettings.ContainsKey(Key))
            {
                dicAppSettings.Remove(Key);
            }

            dictJson["AppSettings"] = dicAppSettings;

            using (StreamWriter writer = System.IO.File.CreateText("appsettings.json"))
            {
                writer.Write(JsonSerializer.Serialize(dictJson, jsonSerializerOptions));
            }

            return Ok(dictJson);
        }
    }
}
