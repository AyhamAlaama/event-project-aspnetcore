using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Event.Application.Interfaces;
using Event.Domain.IdentityModels.ExtendedUser;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;

namespace Event.Application.Features.v1.Events.Command
{
    public class UpdateEventHandler:IRequestHandler<UpdateEventCommand, string>

    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Domain.Events> _repo;
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateEventHandler(IBaseRepository<Domain.Events> repo,
            IMapper mapper, 
            UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager= userManager;
        }

        public async Task<string> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                throw new Exception("Invalid User or event Id");
            var isEventExist = await _repo.GetByAsync(e => e.EventId == request.EventId);
            if (isEventExist is null) 
                throw new Exception( "Invalid User or event Id");
            var checkOwend = await _repo.GetByAsync(p => p.EventId == request.EventId && p.UserId == request.UserId);
            if (checkOwend is null)
                throw new Exception("you dont owen this event");

            var @event = _mapper.Map<Domain.Events>(request);

            if (@event.BookedTickets > request.TotalTickets)
                throw new Exception("Booked Tickets is > From your edit Tickets");

            // convert request to json to store it in dictionary
            //the save keys to list to pass it to UpdateAsync Method
            var valuesAsJson = JsonConvert.SerializeObject(request);
            var values = JsonConvert.DeserializeObject<Dictionary<string,object>>(valuesAsJson);
           
            List<string> keys = new List<string>();
 

            foreach(KeyValuePair<string,object> field in values)
            {
                var checkKey = !field.Key.Contains("id", StringComparison.OrdinalIgnoreCase);
                var checkFieldValue = field.Value != null;
                if (checkKey && checkFieldValue)
                    keys.Add(field.Key);
            }



            await _repo.UpdateAsync(@event, keys);
            return  "Done";
            
           
        }
    }
}
