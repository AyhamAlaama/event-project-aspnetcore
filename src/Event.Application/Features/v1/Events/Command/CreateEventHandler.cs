using AutoMapper;
using MediatR;
using Event.Application.Interfaces;

namespace Event.Application.Features.v1.Events.Command
{
    public class CreateEventHandler:IRequestHandler<CreateEventCommand, string>

    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Domain.Events> _repo;

        public CreateEventHandler(IBaseRepository<Domain.Events> repo, IMapper mapper)
        { 
            _repo = repo;
            _mapper= mapper;
        }
        public async Task<string> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event =  _mapper.Map<Domain.Events>(request);
            //we can use validator logic here
            @event = await _repo.AddAsync(@event);
            return new { EventId= @event.EventId }.ToString();
        }
    }
} 
