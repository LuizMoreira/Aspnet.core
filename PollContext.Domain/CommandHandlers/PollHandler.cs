using Flunt.Notifications;
using Microsoft.Extensions.Logging;
using PollContext.Domain.CommandHandlers;
using PollContext.Domain.Commands.PollCommands.Input;
using PollContext.Domain.Commands.PollCommands.Output;
using PollContext.Domain.Entities;
using PollContext.Domain.Repositories;
using PollContext.Domain.ValueObjects;
using PollContext.Shared.Commands;
using PollContext.Shared.Commands.Contracts;
using PollContext.Shared.Handlers.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollContext.Domain.Handlers
{
    public class PollHandler :  CommandHandler, IHandler<CreatePollCommand>, IHandler<UpdatePollByIdCommand>
    {
        private readonly IPollRepository _pollRepository;
        private readonly ILogger _logger;


        public PollHandler(IPollRepository pollRepository, ILoggerFactory logger, IUow uow):base(uow)
        {
            _pollRepository = pollRepository;
            _logger = logger.CreateLogger("Domain.Handlers.PollHandler");

        }

        public async Task<ICommandResult> Handle(CreatePollCommand command)
        {
            try
            {
                // fail fast validation
                command.Validate();
                if (command.Invalid)
                    return new GenericCommandResult(true, "Enquete inválida", command.Notifications);

                DescriptionVO description = new DescriptionVO(command.Poll_Description);
                Poll poll = new Poll(description);

                List<OptionPoll> opt = new List<OptionPoll>();
                foreach (var item in command.Options)
                {
                    DescriptionVO vo = new DescriptionVO(item);
                    OptionPoll option = new OptionPoll(vo);
                    poll.addOptions(option);
                }

                await _pollRepository.Create(poll);
                if (Commit())
                    return new GenericCommandResult(true, "Enquete gravada com sucesso", new CreatePollCommandResult(poll.Id));
                else
                    return new GenericCommandResult(false, "Falha ao gravar enquete", null);

            }
            catch (Exception ex)
            {
                _logger.LogError("CreatePollCommand --> ", ex);
                return new GenericCommandResult(false, "Falha ao gravar enquete", null);
            }
        }

        public async Task<ICommandResult> Handle(UpdatePollByIdCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                    return new GenericCommandResult(true, "Enquete inválida", command.Notifications);

                //obtem a enquete por id
                var poll = await _pollRepository.GetById(command.Poll_Id);

                if (poll == null) return new GenericCommandResult(true, "Enquete não encontrada", null);
                //incrementa a visualização
                poll.increaseView();

                // salva a alteração feita na views
                await _pollRepository.Update(poll);

                GetPollByIdCommandResult getPollByIdCommandResult = new GetPollByIdCommandResult(poll.Id, poll.Description.Description);
                foreach (var item in poll.OptionsPoll)
                {
                    getPollByIdCommandResult.options.Add(new GetOptionsPollByPolIdCommandResult(item.Id, item.Description.Description));
                }

                if (Commit())
                    return new GenericCommandResult(true, "Enquete recuperada com sucesso", getPollByIdCommandResult);
                else
                    return new GenericCommandResult(false, "Falha ao recuperar enquete enquete", null);

            }
            catch (Exception ex)
            {
                _logger.LogError("VoteOptionPollCommand --> {Poll_Id}", command.Poll_Id, ex);
                return new GenericCommandResult(false, "Falha ao obter enquete", ex);
            }

        }

        

    }
}
