using AutoMapper;
using Event.Application.Features.v1.Events.Command;
using Event.Application.Features.v1.Events.Query.Dtos;
using Event.Domain;
using Events = Event.Domain;

namespace Event.Application.Profiles
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            // ListOfUserBookedTicketsDtos
            CreateMap<EventMovement, ListOfUserBookedTicketsDtos>()
                 .ForMember
                 (
                 dest => dest.Name,
                 options => options.MapFrom(src => src.Event.Name)
                 ).ForMember
                 (
                 dest => dest.Description,
                 options => options.MapFrom(src => src.Event.Description)
                 ).ForMember
                 (
                 dest => dest.DateTime,
                 options => options.MapFrom(src => src.Event.DateTime)
                 ).ForMember
                 (
                 dest => dest.BookedTickets,
                 options => options.MapFrom(src => src.Event.BookedTickets)
                 ).ForMember
                 (
                 dest => dest.EventId,
                 options => options.MapFrom(src => src.Event.EventId)
                 ).ForMember
                 (
                 dest => dest.Location,
                 options => options.MapFrom(src => src.Event.Location)
                 )

                 .ReverseMap();

            CreateMap<Events.Events, ListOfEventsDto>()
                .ForMember
                (
                dest => dest.OwnerName,
                options => options.MapFrom(src => src.User.UserName)
                )
                .ForMember(p=> p.AvailableTickets,
                o => o.MapFrom(s => (s.TotalTickets - s.BookedTickets) ))
                .ReverseMap();
            
            
 
            CreateMap<Events.Events, CreateEventCommand>().ReverseMap();
            CreateMap<Events.Events, UpdateEventCommand>().ReverseMap();
            CreateMap<Events.Events, BookEventCommand>().ReverseMap();

           
        }
    }
}
