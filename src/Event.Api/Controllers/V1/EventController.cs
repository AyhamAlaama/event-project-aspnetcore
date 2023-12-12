using MediatR;
using Microsoft.AspNetCore.Mvc;
using Event.Api.Extensions;
using Event.Application.Features.v1.Events.Command;
using Event.Application.Features.v1.Events.Query.Dtos;
using Event.Application.Features.v1.Events.Query.FetchAllEvents;

namespace Event.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Manage Users And Roles
        /// </summary>
        /// <param name="mediator"></param>
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region EventCreation
        /// <summary>
        /// Create New Event (Authorize)
        /// </summary>
        /// <param name="command">CreateEventCommand model</param>
        /// <returns> event id</returns>
        [HttpPost("CreateNewEvent")]
        public async Task<ActionResult<string>> CreateEvent([FromBody] CreateEventCommand command)
      => Ok(await _mediator.Send(
             new CreateEventCommand()
              {
                Name = command.Name,
                Description=command.Description,
                Location=command.Location,
                DateTime=command.DateTime,
                TotalTickets=command.TotalTickets,
                UserId = HttpContext.GetUserId()
             })
             );
        #endregion EventCreation
        #region EventManagement
        /// <summary>
        /// Update Event (Authoriz)
        /// </summary>
        /// <param name="command">UpdateEvent model</param>
        /// <returns> magic string</returns>
        [HttpPut("UpdateEvent")]
        public async Task<ActionResult<string>> UpdateEvent([FromBody] UpdateEventCommand command)
        {
            var updatedObj = new UpdateEventCommand {
                EventId      = command.EventId,
                UserId       = HttpContext.GetUserId(),
                Name         = command.Name,
                TotalTickets = command.TotalTickets,
                Description  = command.Description,
                DateTime     = command.DateTime,
                Location     = command.Location//....etc
            };
            var result = await _mediator.Send(updatedObj);
          return  Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}",Name ="DeleteEvent")]    
        public async Task<ActionResult> Delete(int id)
        {
            var deleteEvent = new DeleteEventCommand() { EventId = id };
            if (deleteEvent is null) return NotFound();
            await _mediator.Send(deleteEvent);
            return Ok(deleteEvent.EventId);
        }
        #endregion EventManagement
        #region EventParticipation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost("BookTicket")]
        public async Task<ActionResult> BookTicket([FromBody] BookEventCommand cmd)
        {

            var booked = await _mediator.Send(
                new BookEventCommand
                {
                    EventId = cmd.EventId,
                    UserId = HttpContext.GetUserId(),
                    Tickets = cmd.Tickets
                }
                );
            return Ok(cmd.EventId);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Mybooked")]
        public async Task<ActionResult<List<ListOfUserBookedTicketsDtos>>> Mybooked()
         => Ok(await _mediator.Send(new ListOfUserBookedTicketsRequest { UserId = HttpContext.GetUserId() }));
        /// <summary>
        /// List of available Events (Authorize)
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ListOfEventsDto>>> GetAllEvents()
         => Ok(await _mediator.Send(new ListOfEventsRequest()));
        [HttpPost("UnBookTicket")]
        public async Task<ActionResult> UnBookTicket([FromBody] UnBookedEventCommand cmd)
        {

            var unbooked = await _mediator.Send(
                new UnBookedEventCommand
                {
                    EventId = cmd.EventId,
                    UserId = HttpContext.GetUserId()
                }
                );
            return Ok(cmd.EventId);
        }
        #endregion
    }
}
