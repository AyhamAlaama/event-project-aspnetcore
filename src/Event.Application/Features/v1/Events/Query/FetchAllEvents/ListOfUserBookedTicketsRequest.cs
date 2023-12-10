using Event.Application.Features.v1.Events.Query.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Event.Application.Features.v1.Events.Query.FetchAllEvents
{
    public class ListOfUserBookedTicketsRequest : IRequest<List<ListOfUserBookedTicketsDtos>>
    {
       
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
