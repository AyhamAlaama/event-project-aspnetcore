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
    internal class UnBookedEventHandler : IRequestHandler<UnBookedEventCommand>
    {
        private readonly IBaseRepository<EventMovement> _repoMovement;
        private readonly IBaseRepository<Domain.Events> _repoEvent;
        public UnBookedEventHandler(IBaseRepository<EventMovement> repoMovement,
            IBaseRepository<Domain.Events> repoEvent)
        {
            _repoMovement = repoMovement;
            _repoEvent = repoEvent;
        }
        public async Task<Unit> Handle(UnBookedEventCommand request, CancellationToken cancellationToken)
        {
            var isEventExist = await _repoEvent.GetByAsync(e => e.EventId == request.EventId);
            if (isEventExist is null)
                throw new Exception("Invalid Event Id");
            var checkOwend = await _repoMovement
                .GetByAsync(p => p.EventId == request.EventId && p.UserId == request.UserId);
            if (checkOwend is null)
                throw new Exception("you dont owen this event");

           await _repoMovement.DeleteAsync(checkOwend);

            var e = new Domain.Events
            {
                EventId = request.EventId,
                BookedTickets = isEventExist.BookedTickets - checkOwend.BookedTickets
            };


            await _repoEvent.UpdateAsync(e, new List<string> { nameof(e.BookedTickets) });

            return Unit.Value;
        }
    }
}
