using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using myAspApiTest.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace myAspApiTest.Controllers
{


    /// <summary>
    /// Ticket Controller Responsible For GET/POST/PUT/DELETE For Managing Tickets
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
   
    public class TicketsController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public TicketsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// The GET Method Returns All The Tickets
        /// </summary>
        [HttpGet]
        public JsonResult AllTicket()
        {
            string query = @"
                             select 
                                id,
                                title,
                                ticketowner,
                                destination,
                                DATE_FORMAT(flydate,'%y-%m-%d') as flydate 
                            from tickets.ticket;
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Ticketdb");
            using(MySqlConnection conn = new MySqlConnection(sqlDataSource))
            {
                conn.Open();
                using(MySqlCommand command = new MySqlCommand(query,conn))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    table.Load(reader);

                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }

        /// <summary>
        /// The POST Method Is To Create Ticket
        /// </summary>
        [HttpPost]
        public JsonResult CreateTicket([FromBody] TicketsModel tickets)
        {
            string query = @"
                    insert into tickets.ticket 
                        (id,title,ticketowner,destination,flydate) value
                         (@id,@title,@ticketowner,@destination,@flydate);
                   
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Ticketdb");
            using (MySqlConnection connnection = new MySqlConnection(sqlDataSource))
            {
                connnection.Open();
                using(MySqlCommand command = new MySqlCommand(query, connnection))
                {
                    command.Parameters.AddWithValue("@id",tickets.id);
                    command.Parameters.AddWithValue("@title", tickets.title);
                    command.Parameters.AddWithValue("@ticketowner", tickets.ticketowner);
                    command.Parameters.AddWithValue("@destination", tickets.destination);
                    command.Parameters.AddWithValue("@flydate", tickets.flydate);

                    MySqlDataReader reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    connnection.Close();
                }
                return new JsonResult(table);
            }
        }

        /// <summary>
        /// The GET Method Get A Single Ticket By Id 
        /// </summary>
        [HttpGet("{id}")]
        public JsonResult GetTicketById(int id)
        {
            string query = @"
                             select * from tickets.ticket where id = @id;
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Ticketdb");
            using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
            {
                conn.Open();
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@id",id);

                    MySqlDataReader reader = command.ExecuteReader();
                    table.Load(reader);

                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }


        /// <summary>
        /// The PUT Method Update A Single Ticket By Id 
        /// </summary>
        [HttpPut]
        public JsonResult UpdateTicket([FromBody] TicketsModel tickets)
        {
            string query = @"
                    update tickets.ticket set
                      title = @title,
                      ticketowner = @ticketowner,
                      destination = @destination,
                      flydate = @flydate
                    where id = @id;
                        
                   
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Ticketdb");
            using (MySqlConnection connnection = new MySqlConnection(sqlDataSource))
            {
                connnection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connnection))
                {
                    command.Parameters.AddWithValue("@id", tickets.id);
                    command.Parameters.AddWithValue("@title", tickets.title);
                    command.Parameters.AddWithValue("@ticketowner", tickets.ticketowner);
                    command.Parameters.AddWithValue("@destination", tickets.destination);
                    command.Parameters.AddWithValue("@flydate", tickets.flydate);

                    MySqlDataReader reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    connnection.Close();
                }
                return new JsonResult("Update Successful");
            }
        }

        /// <summary>
        /// The DELETE Method Delete A Single Ticket By Id 
        /// </summary>
        [HttpDelete("{id}")]
        public JsonResult DeleteTicket(int id)
        {
            string query = @"
                             delete from tickets.ticket where id = @id;
               ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Ticketdb");
            using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
            {
                conn.Open();
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@id", id);

                    MySqlDataReader reader = command.ExecuteReader();
                    table.Load(reader);

                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Delete Successful");
        }

    }
}
