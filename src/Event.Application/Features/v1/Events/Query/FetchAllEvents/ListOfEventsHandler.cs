using AutoMapper;
using MediatR;
using Event.Application.Features.v1.Events.Query.Dtos;
using Event.Application.Interfaces;

namespace Event.Application.Features.v1.Events.Query.FetchAllEvents
{
    public class ListOfEventsHandler : IRequestHandler<ListOfEventsRequest, List<ListOfEventsDto>>
    {
        private readonly IBaseRepository<Domain.Events> _repo;
        private readonly IMapper _mapper;
        public ListOfEventsHandler(IBaseRepository<Domain.Events> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<ListOfEventsDto>> Handle(ListOfEventsRequest request, CancellationToken cancellationToken)
        {
            var query = await _repo.GetAllAsync(
                criteria:m=> ( (m.TotalTickets-m.BookedTickets)>0 ),
                includes: new string[] { "User" });
            return _mapper.Map<List<ListOfEventsDto>>(query);
        }
    }
}
