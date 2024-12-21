using DBSimulation.Database;
using DBSimulation.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using MySql.Data.MySqlClient;

namespace DBSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : Controller
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ExecuteQuery([FromBody] Query query)
        {
            if (query == null || string.IsNullOrWhiteSpace(query.Content))
            {
                return BadRequest("La query non è valida.");
            }

            if (DBManager.connection.State == ConnectionState.Closed)
            {
                return BadRequest("La connessione al database è chiusa.");
            }

            try
            {
                using var command = DBManager.connection.CreateCommand();

                command.CommandText = query.Content;
                command.CommandType = CommandType.Text;

                if (query.IsSelectQuery())
                {
                    using var reader = command.ExecuteReader();
                    var results = new List<Dictionary<string, object>>();

                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.GetValue(i);
                        }

                        results.Add(row);
                    }

                    return Ok(results);
                }
                else
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    return Ok(new { message = "Query executed successfully.", rowsAffected });
                }
            }
            catch (MySqlException ex)
            {
                return BadRequest(new { message = "Errore nell'esecuzione della query.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Errore interno del server.", error = ex.Message });
            }
        }
    }

}
