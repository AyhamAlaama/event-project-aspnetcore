using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Event.Application.Features.v1.Events.Command
{
    public class UpdateEventCommand : IRequest<string>
    {
        [Required]
        public int EventId { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        public DateTime DateTime { get; set; }

        public int? TotalTickets { get; set; }

    }
}
