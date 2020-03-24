using Microsoft.AspNetCore.Mvc;
using PollContext.Domain.Commands.OptionPollCommands.Input;
using PollContext.Domain.Commands.PollCommands.Input;
using PollContext.Domain.Handlers;
using PollContext.Shared.Commands;
using System;

namespace PollContext.webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        public GenericCommandResult Post([FromBody] CreatePollCommand command, [FromServices] PollHandler handler)
        {
            
            return (GenericCommandResult)handler.Handle(command);
            
        }

        [HttpGet("{id}")]
        public GenericCommandResult Get(Guid id, [FromServices] PollHandler handler)
        {
            return (GenericCommandResult) handler.Handle(new GetPollByIdCommand(id));
           
        }

        [HttpPost("{id}/vote")]
        public GenericCommandResult Post(Guid id, [FromBody] VoteOptionPollCommand command, [FromServices] OptionPollHandler handler)
        {
            command.Poll_Id = id;
            return (GenericCommandResult)handler.Handle(command);
        }

        [HttpGet("{id}/stats")]
        public GenericCommandResult GetStats(Guid id, [FromServices] PollHandler handler)
        {
            return (GenericCommandResult)handler.Handle(new GetPollStatsByIdCommand(id));
        }
    }
}