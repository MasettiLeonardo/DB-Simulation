using Microsoft.AspNetCore.Mvc;
using DBSimulation.Models.Requests;
using MySql.Data.MySqlClient;
using System.Data.Common;
using DBSimulation.Database;

namespace DBSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CredentialsController : Controller
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ConnectToDB([FromBody] DBCredentials credentials)
        {
            var connectionString = $"Server={credentials.Hostname};" +
                                   $"Database={credentials.DatabaseName};" +
                                   $"User ID={credentials.Username};" +
                                   $"Password={credentials.Password};" +
                                   $"Port={credentials.Port};";

            try
            {
                DBManager.connection = new MySqlConnection(connectionString);
                DBManager.connection.Open();

                return Ok(new { message = "Connection successful!" });
            }
            catch (MySqlException ex)
            {
                return BadRequest(new { message = "Connection failed", error = ex.Message });
            }
        }
    }

}
