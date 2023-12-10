using AutoMapper;
using Event.Application.Features.v1.Events.Query.Dtos;
using Event.Application.Interfaces;
using Event.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Features.v1.Events.Query.FetchAllEvents
{
    public class ListOfUserBookedTicketsHandler : IRequestHandler<ListOfUserBookedTicketsRequest, List<ListOfUserBookedTicketsDtos>>
    {
        private readonly IBaseRepository<EventMovement> _repoMovement;
        private readonly IMapper _mapper;
        public ListOfUserBookedTicketsHandler
            ( IMapper mapper, 
            IBaseRepository<EventMovement> repoMovement)
        {
          
            _mapper = mapper;
            _repoMovement = repoMovement;
        }
        public async Task<List<ListOfUserBookedTicketsDtos>> Handle(ListOfUserBookedTicketsRequest request, CancellationToken cancellationToken)
        {
            
            var query = await _repoMovement.GetAllAsync(
                criteria: p=> p.UserId == request.UserId,
                includes: new string[] {"Event"});
            return _mapper.Map<List<ListOfUserBookedTicketsDtos>>(query);
        }
    }
}
