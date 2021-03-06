﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PollContext.Domain.CommandHandlers;
using PollContext.Domain.Commands.OptionPollCommands.Input;
using PollContext.Domain.Commands.PollCommands.Input;
using PollContext.Domain.Handlers;
using PollContext.Domain.Queries.PollQueriesInput;
using PollContext.Domain.Repositories;
using PollContext.Infra.Repositories;
using PollContext.Shared.Commands;
using PollContext.Shared.Queries;
using System;
using System.Threading.Tasks;

namespace PollContext.webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class PollController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPollQueryRepository _pollQueryRepository;

        public PollController(ILoggerFactory logger, IPollQueryRepository pollQueryRepository)
        {
            _logger = logger.CreateLogger("WebAPI.PollController");
            _pollQueryRepository = pollQueryRepository;
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<GenericCommandResult>> Post([FromBody] CreatePollCommand command, [FromServices] PollHandler handler)
        {
            //var user = User.Claims.FirstOrDefault(x=>x.Type == "user_id")?.Value;
            var ret = (GenericCommandResult)await handler.Handle(command);
            if (!ret.Success)
            {
                _logger.LogWarning("Post --> ", ret);
                return BadRequest(ret);

            }
            _logger.LogInformation("Post --> ", ret);
            return Ok(ret);
            
        }
        
        //Para cache de método
        //[ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
        //para dizer que o método não é cache, teria que habilitar o cache geral na startup com services.AddResponseCaching();
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //com restrição de rota
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GenericCommandResult>> Get(Guid id, [FromServices] PollHandler handler)
        {
            var ret = (GenericCommandResult) await handler.Handle(new UpdatePollByIdCommand(id));
            if (!ret.Success)
            {
                _logger.LogWarning("Get --> {Id}",id, ret);
                return BadRequest(ret);
            }
            _logger.LogInformation("Get --> {Id}", id, ret);

            return Ok(ret);

        }

        [HttpPost("{id:Guid}/vote")]
        public async Task<ActionResult<GenericCommandResult>> Vote(Guid id, [FromBody] VoteOptionPollCommand command, [FromServices] OptionPollHandler handler)
        {
            command.Poll_Id = id;
            var ret = (GenericCommandResult)await handler.Handle(command);
            if (!ret.Success)
            {
                _logger.LogWarning("Vote --> {Id}", id, ret);
                return BadRequest(ret);
            }
            return Ok(ret);

        }

        [HttpGet("{id:Guid}/stats")]
        public async Task<ActionResult<GenericQueryResult>> GetStats(Guid id)
        {
            var ret = (GenericQueryResult)await _pollQueryRepository.GetStatsById(new GetPollStatsQuery(id));
            if (!ret.Success)
            {
                _logger.LogWarning("GetStats --> {Id}", id, ret);
                return BadRequest(ret);
            }
            return Ok(ret);

        }
    }
}