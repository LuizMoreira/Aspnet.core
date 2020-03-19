﻿using Flunt.Notifications;
using PollContext.Domain.Commands.PollCommands.Input;
using PollContext.Domain.Commands.PollCommands.Output;
using PollContext.Domain.Entities;
using PollContext.Domain.Repositories;
using PollContext.Domain.ValueObjects;
using PollContext.Shared.Commands;
using PollContext.Shared.Commands.Contracts;
using PollContext.Shared.Handlers.Contracts;

namespace PollContext.Domain.Handlers
{
    public class PollHandler : Notifiable, IHandler<CreatePollCommand>, IHandler<UpdateViewsPollCommand>
    {
        private readonly IPollRepository _pollRepository;

        public PollHandler(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public ICommandResult Handle(CreatePollCommand command)
        {
            // fail fast validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Enquete inválida", command.Notifications);
            
            DescriptionVO description = new DescriptionVO(command.Poll_Description);
            Poll poll = new Poll(description);
            
            foreach (var item in command.Options)
            {
                DescriptionVO vo = new DescriptionVO(item);
                OptionPoll option = new OptionPoll(vo);
                poll.addOptions(option);
            }

            _pollRepository.Create(poll);

            return new CreatePollCommandResult(poll.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ICommandResult Handle(UpdateViewsPollCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Enquete inválida", command.Notifications);

            //obtem a enquete por id
            var poll = _pollRepository.GetById(command.Poll_Id);
            
            //incrementa a visualização
            poll.increaseView();

            // salva a alteração feita na views
            _pollRepository.Update(poll);

            GetPollByIdCommandResult getPollByIdCommandResult = new GetPollByIdCommandResult(poll.Id, poll.Description.Description);
            foreach (var item in poll.OptionsPoll)
            {
                GetOptionsPollByPolIdCommandResult optResult = new GetOptionsPollByPolIdCommandResult();
                optResult.Option_id = item.Id;
                optResult.option_description = item.Description.Description;
                getPollByIdCommandResult.options.Add(optResult);
            }

            return getPollByIdCommandResult;
        }

    }
}
