using AutoMapper;
using Event.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Features.v1.Events.Command
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IBaseRepository<Domain.Events> _repo;

        public DeleteEventHandler(IBaseRepository<Domain.Events> repo)
         =>   _repo = repo;

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var isEventExist = await _repo.GetByAsync(x=> x.EventId == request.EventId);
            if (isEventExist is null)
                 throw new Exception("Invalid Id");
            await _repo.DeleteAsync(isEventExist);
            return Unit.Value;
        }
    }
}
