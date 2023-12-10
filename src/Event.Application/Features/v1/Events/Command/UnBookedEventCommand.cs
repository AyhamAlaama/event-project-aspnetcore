using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Event.Application.Features.v1.Events.Command
{
    public class UnBookedEventCommand : IRequest
    {
        [Required]
        public int EventId { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; }
    }
}
