using Microsoft.AspNetCore.Mvc;
using myAspApiTest.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace myAspApiTest.Model
{
    public class TicketsModel
    {
        /// <summary>
        /// Ticket Id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Ticket Title 
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Ticket Owner
        /// </summary>
        public string ticketowner { get; set; }

        /// <summary>
        /// Ticket Owner Destination
        /// </summary>
        [EnsureDestinationValidation]
        public string destination { get; set; }

        /// <summary>
        /// Date To Fly
        /// </summary>
        public DateTime flydate { get; set; }
    }
}
