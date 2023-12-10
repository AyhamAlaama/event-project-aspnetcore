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
    public class CreateEventCommand:IRequest<string>
    {
        [Required,MinLength(5)]
        public string? Name { get; set; }
        [Required, MinLength(15)]
        public string? Description { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public DateTime DateTime { get; set; } 
        [Required]
        public int TotalTickets { get; set; }
        
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
