﻿using Flunt.Notifications;
using PollContext.Domain.Commands.OptionPollCommands.Input;
using PollContext.Domain.Repositories;
using PollContext.Shared.Commands;
using PollContext.Shared.Commands.Contracts;
using PollContext.Shared.Handlers.Contracts;
using System;
using System.Threading.Tasks;

namespace PollContext.Domain.Handlers
{
    public class OptionPollHandler : Notifiable, IHandler<VoteOptionPollCommand>
    {
        private readonly IOptionPollRepository _optionPollRepository;

        public OptionPollHandler(IOptionPollRepository optionPollRepository)
        {
            _optionPollRepository = optionPollRepository;
        }


        public async Task<ICommandResult> Handle(VoteOptionPollCommand command)
        {
            try
            {
                command.Validate();
                if (command.Invalid)
                    return new GenericCommandResult(true, "Enquete inválida", command.Notifications);

                //obtem a resposta de uma enquete por id e poll id
                var optionPoll = await _optionPollRepository.GetOptionPollById(command.Option_Id, command.Poll_Id);

                if (optionPoll == null) return new GenericCommandResult(true, "Enquete não encontrada", null);

                //incrementa a qty
                optionPoll.increaseQty();

                // salva a alteração feita na qtd
                _optionPollRepository.Update(optionPoll);

                //TODO: retornar o obj DTO para evitar retornar nossa entity
                return new GenericCommandResult(true, "Enquete salva com sucesso", null);
            }
            catch (Exception ex)
            {
                return new GenericCommandResult(false, "Falha ao votar em uma enquete", ex);
            }
        }
    }
}
