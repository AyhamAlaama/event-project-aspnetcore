using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Features.v1.Events.Query.Dtos
{
    public class ListOfUserBookedTicketsDtos
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime DateTime { get; set; }
        public int EventId { get; set; }
        public int BookedTickets { get; set; }

    }
}
