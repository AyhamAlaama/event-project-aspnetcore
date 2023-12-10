using Event.Domain.IdentityModels.ExtendedUser;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event.Domain
{
    public class Events
    {
        [Key]
        public int EventId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime? DateTime { get; set; } //default value
        // or we can use sperate date and time

        /*
        [Column(TypeName = "date")]
        public DateTime EventDate { get; set; } = DateTime.Now.Date
        public TimeSpan EventTime { get; set; } = DateTime.Now.TimeOfDay
         */
    

        public int? TotalTickets { get; set; }
        public int BookedTickets { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        public ICollection<EventMovement>? eventMovements { get; set; }


    }
}
