using InventaryApp.Utilities.Logger;
using InventaryApp.Utilities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventaryApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiVersion("1.0")]
    public class RoleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        protected readonly string connection;

        public RoleController(IConfiguration configuration)
        {
            _configuration = configuration;
            connection = _configuration.GetConnectionString("InventaryAppConnection");
        }

        // GET: api/<RoleController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Role> roleList = new List<Role>();
                string query = @"
                                SELECT 
                                    [id],
	                                [role_guid],
	                                [name],
	                                [description]
                                FROM [dbo].[Role]
                                ";

                using (SqlConnection context = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        context.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.HasRows)
                                {
                                    var tableData = new Dictionary<string, object>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                        tableData.Add(reader.GetName(i), reader.GetValue(i));

                                    var jsonString = JsonSerializer.Serialize(tableData);
                                    var rolesObj = JsonSerializer.Deserialize<Role>(jsonString);
                                    roleList.Add(rolesObj);
                                }
                            }
                        }
                        context.Close();
                    }
                }
                return Ok(roleList);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error en método Get {Url.Action("Get", "Role")}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post(Role role)
        {
            try
            {
                var query = @"
                        INSERT INTO [dbo].[Role]
                       ([name]
                       ,[description])
                        VALUES
                       (@name
                       ,@description)
                        ";

                using (SqlConnection context = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.Parameters.AddWithValue("@name", role.name);
                        cmd.Parameters.AddWithValue("@description", role.description);
                        
                        context.Open();
                        int rows = cmd.ExecuteNonQuery();
                        context.Close();
                    }
                }

                Logger.LogInfo($"Registro asociado al Rol {role.name} agregado correctamente");
                return Ok("Rol agregado correctamente\n");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error en metodo Post => {Url.Action("Post", "Role")}");
                return StatusCode(500, new { Message = "Error en el envio de datos, revisar log de errores.", ErrorMessage = ex.Message });
            }

        }

        [HttpPut]
        public IActionResult Put(Role role)
        {
            try
            {
                var query = @"
                            UPDATE [dbo].[Role]
                            SET [name] = @name
                               ,[description] = @description
                            WHERE [role_guid] = @role_guid
                            ";

                using (SqlConnection context = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.Parameters.AddWithValue("@role_guid", role.role_guid);
                        cmd.Parameters.AddWithValue("@name", role.name);
                        cmd.Parameters.AddWithValue("@description", role.description);

                        context.Open();
                        int rows = cmd.ExecuteNonQuery();
                        context.Close();
                    }
                }
                Logger.LogInfo($"Registro asociado al Guid {role.role_guid} actualizado correctamente");
                return Ok($"Registro asociado al rol {role.name} actualizado \n");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error en metodo Post => {Url.Action("Put", "Role")}");
                return StatusCode(500, new { Message = "Error en el envio de datos, revisar log de errores.", ErrorMessage = ex.Message });
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(string Id)
        {
            try
            {
                string query = @"
                                DELETE 
                                FROM [dbo].[Role]
                                WHERE [role_guid] = @role_guid
                                ";

                using (SqlConnection context = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.Parameters.AddWithValue("@role_guid", Id);

                        context.Open();
                        int rows = cmd.ExecuteNonQuery();
                        context.Close();
                    }
                }

                Logger.LogInfo($"Registro asociado al Guid {Id} eliminado correctamente");
                return Ok("Registro eliminado con exito");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error en metodo Post => {Url.Action("Put", "Role")}");
                return StatusCode(500, new { Message = "Error en el envio de datos, revisar log de errores.", ErrorMessage = ex.Message });
            }

        }
    }
}
