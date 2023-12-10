using MediatR;
using Event.Application.Features.v1.Events.Query.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Features.v1.Events.Query.FetchAllEvents
{
    public class ListOfEventsRequest:IRequest<List<ListOfEventsDto>>
    {
    }
}
