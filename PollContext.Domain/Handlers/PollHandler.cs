﻿using Flunt.Notifications;
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
    public class PollHandler : Notifiable, IHandler<CreatePollCommand>, IHandler<GetPollByIdCommand>, IHandler<GetPollStatsByIdCommand>
    {
        private readonly IPollRepository _pollRepository;
        
        public PollHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<ICommandResult> Handle(CreatePollCommand command)
        {
            try
            {
                // fail fast validation
                command.Validate();
                if (command.Invalid)
                    return new GenericCommandResult(false, "Enquete inválida", command.Notifications);

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

                return new GenericCommandResult(true, "Enquete gravada com sucesso", new CreatePollCommandResult(poll.Id));
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Falha ao gravar enquete", ex);
            }
        }

        public async Task<ICommandResult> Handle(GetPollByIdCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                    return new GenericCommandResult(false, "Enquete inválida", command.Notifications);

                //obtem a enquete por id
                var poll = await _pollRepository.GetById(command.Poll_Id);

                if (poll == null) return new GenericCommandResult(false, "Enquete não encontrada", null);
                //incrementa a visualização
                poll.increaseView();

                // salva a alteração feita na views
                await _pollRepository.Update(poll);

                GetPollByIdCommandResult getPollByIdCommandResult = new GetPollByIdCommandResult(poll.Id, poll.Description.Description);
                foreach (var item in poll.OptionsPoll)
                {
                    getPollByIdCommandResult.options.Add(new GetOptionsPollByPolIdCommandResult(item.Id, item.Description.Description));
                }

                return new GenericCommandResult(true, "Enquete gravada com sucesso", getPollByIdCommandResult);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Falha ao obter enquete", ex);
            }

        }

        public async Task<ICommandResult> Handle(GetPollStatsByIdCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                    return new GenericCommandResult(false, "Enquete inválida", command.Notifications);

                //obtem a enquete por id
                var poll = await _pollRepository.GetById(command.Poll_Id);

                if (poll == null) return new GenericCommandResult(false, "Enquete não encontrada", null);

                GetPollStatsByIdCommandResult getPollStatsByIdCommandResult = new GetPollStatsByIdCommandResult(poll.Id, poll.Views);
                foreach (var item in poll.OptionsPoll)
                {
                    getPollStatsByIdCommandResult.options.Add(new GetOptionsPollStatsByPolIdCommandResult(item.Id, item.Qty));
                }

                return new GenericCommandResult(true, "Enquete gravada com sucesso", getPollStatsByIdCommandResult);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Falha ao obter a estatística de uma enquete", ex);
            }
        }

    }
}
