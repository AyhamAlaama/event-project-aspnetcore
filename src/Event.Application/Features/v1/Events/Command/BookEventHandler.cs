using Event.Application.Interfaces;
using Event.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Features.v1.Events.Command
{
    internal class BookEventHandler : IRequestHandler<BookEventCommand>
    {
        private readonly IBaseRepository<EventMovement> _repoMovement;
        private readonly IBaseRepository<Domain.Events> _repoEvent;
        public BookEventHandler(IBaseRepository<EventMovement> repoMovement,
            IBaseRepository<Domain.Events> repoEvent)
        {
            _repoMovement = repoMovement; 
            _repoEvent = repoEvent;
        }
        public async Task<Unit> Handle(BookEventCommand request, CancellationToken cancellationToken)
        {
            var isEventExist = await _repoEvent.GetByAsync(e => e.EventId == request.EventId);
            if (isEventExist is null)
                throw new Exception("Invalid Event Id");
            var isUserBooked = await _repoMovement
                .GetByAsync(e => e.UserId == request.UserId && e.EventId == request.EventId);
            if (isUserBooked is not null)
                throw new Exception($"User Booked Tickets Before at {isUserBooked.CreatedAt:g}");
            var n = (isEventExist.TotalTickets - isEventExist.BookedTickets);
            if(n <= 0 || request.Tickets > n) 
                throw new Exception("Invalid number of Tickets");
            var movement = await _repoMovement
                .AddAsync(
                new EventMovement { EventId=request.EventId, UserId=request.UserId,BookedTickets=request.Tickets });
            
            var e = new Domain.Events
            {
                EventId = request.EventId,
                BookedTickets = isEventExist.BookedTickets + request.Tickets
            };

           
            await _repoEvent.UpdateAsync(e, new List<string> {nameof(e.BookedTickets) });

            return Unit.Value;
        }
    }
}
