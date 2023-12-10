using Event.Domain.IdentityModels.ExtendedUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Domain
{
    public class EventMovement
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
        public int EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public Events Event { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;//default value
        public int BookedTickets { get; set; }
         
        
    }
}
