﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PollContext.Domain.Commands.OptionPollCommands.Input;
using PollContext.Domain.Commands.PollCommands.Input;
using PollContext.Domain.Handlers;
using PollContext.Shared.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PollContext.webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class PollController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        public async Task<ActionResult<GenericCommandResult>> Post([FromBody] CreatePollCommand command, [FromServices] PollHandler handler)
        {
            //var user = User.Claims.FirstOrDefault(x=>x.Type == "user_id")?.Value;
            var ret = (GenericCommandResult)await handler.Handle(command);
            if (!ret.Success)
                BadRequest(ret);
            return Ok(ret);
            
        }
        
        //com restrição de rota
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GenericCommandResult>> Get(Guid id, [FromServices] PollHandler handler)
        {
            var ret = (GenericCommandResult) await handler.Handle(new GetPollByIdCommand(id));
            if (!ret.Success)
                BadRequest(ret);
            return Ok(ret);

        }

        [HttpPost("{id:Guid}/vote")]
        public async Task<ActionResult<GenericCommandResult>> Post(Guid id, [FromBody] VoteOptionPollCommand command, [FromServices] OptionPollHandler handler)
        {
            command.Poll_Id = id;
            var ret = (GenericCommandResult)await handler.Handle(command);
            if (!ret.Success)
                BadRequest(ret);
            return Ok(ret);

        }

        [HttpGet("{id:Guid}/stats")]
        public async Task<ActionResult<GenericCommandResult>> GetStats(Guid id, [FromServices] PollHandler handler)
        {
            var ret = (GenericCommandResult)await handler.Handle(new GetPollStatsByIdCommand(id));
            if (!ret.Success)
                BadRequest(ret);
            return Ok(ret);

        }
    }
}