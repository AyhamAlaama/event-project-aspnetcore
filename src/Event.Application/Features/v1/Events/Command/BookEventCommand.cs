using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Event.Application.Features.v1.Events.Command
{
    public class BookEventCommand:IRequest
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public int Tickets { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
