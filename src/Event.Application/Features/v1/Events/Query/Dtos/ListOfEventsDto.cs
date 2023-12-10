using Event.Domain.IdentityModels.ExtendedUser;
using Event.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Event.Application.Features.v1.Events.Query.Dtos
{
    public class ListOfEventsDto
    {
      
        public int EventId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime DateTime { get; set; }
        public int AvailableTickets { get; set; }
      
        public string? OwnerName { get; set; }

    }
}
