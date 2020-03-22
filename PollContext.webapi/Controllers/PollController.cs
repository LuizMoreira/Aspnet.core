using Microsoft.AspNetCore.Mvc;
using PollContext.Domain.Commands;
using PollContext.Domain.Commands.OptionPollCommands.Input;
using PollContext.Domain.Commands.PollCommands.Input;
using PollContext.Domain.Commands.PollCommands.Output;
using PollContext.Domain.Entities;
using PollContext.Domain.Handlers;
using PollContext.Domain.Repositories;
using PollContext.Shared.Commands;
using System;

namespace PollContext.webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        //Asp.net core aceita indicar qual o serviço que será usado (handle) que já foi iniciado na DI
        [Route("")]
        [HttpPost]
        public GenericCommandResult Post([FromBody] CreatePollCommand command, [FromServices] PollHandler handler)
        {
            
            return (GenericCommandResult)handler.Handle(command);
            
        }

        [HttpGet("{id}")]
        public GenericCommandResult Get(Guid id, [FromServices] PollHandler handler)
        {
            if (id == null) return new GenericCommandResult(false, "Identificador obrigatório", null);
            UpdateViewsPollCommand command = new UpdateViewsPollCommand(id);
            var ret = (GetPollByIdCommandResult)handler.Handle(command);
            return new GenericCommandResult(true, "Sucesso", ret);
           
        }

        [HttpPost("{id}/vote")]
        public GenericCommandResult Post(string id, [FromBody] VoteOptionPollCommand command, [FromServices] OptionPollHandler handler)
        {
            command.Poll_Id = Guid.Parse(id);
            return (GenericCommandResult)handler.Handle(command);
        }

        [HttpGet("{id}/stats")]
        public Poll GetStats(string id, [FromServices] IPollRepository repository)
        {
            return repository.GetById(Guid.Parse(id));
        }
    }
}