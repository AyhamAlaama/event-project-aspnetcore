using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Features.v1.Events.Command
{
    public class DeleteEventCommand:IRequest
    {
        public int EventId { get; set; }
    }
}
