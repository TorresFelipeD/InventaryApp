using InventaryApp.Utilities.Logger;
using InventaryApp.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventaryApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                List<Role> roles = new List<Role>();

                using (SqlConnection context = new SqlConnection(connection))
                {
                    string query = @"
SELECT [id]
      ,[role_guid]
      ,[name]
      ,[description]
FROM [dbo].[Role]
;";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        context.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.HasRows)
                                {
                                    roles.Add(new Role
                                    {
                                        id = int.Parse(reader["id"].ToString()),
                                        role_guid = Guid.Parse(reader["role_guid"].ToString()),
                                        name = reader["name"].ToString(),
                                        description = reader["description"].ToString(),
                                    });
                                }
                            }
                        }
                        context.Close();
                    }
                }
                return Ok(roles);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error en método Get {Url.Action("Get", "Role")}");
                return StatusCode(500);
            }
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                Role role = new Role();

                using (SqlConnection context = new SqlConnection(connection))
                {
                    string query = @"
SELECT [id]
      ,[role_guid]
      ,[name]
      ,[description]
FROM [dbo].[Role]
WHERE [role_guid] = @rolguid
;";
                    SqlParameter roleGuid = new SqlParameter("@rolguid", SqlDbType.NVarChar);
                    roleGuid.Value = id;

                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.Parameters.Add(roleGuid);
                        context.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.HasRows)
                                {
                                    role.id = int.Parse(reader["id"].ToString());
                                    role.role_guid = Guid.Parse(reader["role_guid"].ToString());
                                    role.name = reader["name"].ToString();
                                    role.description = reader["description"].ToString();
                                }
                            }
                        }
                        context.Close();
                    }
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error en método Get {Url.Action("Get", "Role")} con id: {id}");
                return StatusCode(500);
            }
        }

        // POST api/<RoleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
