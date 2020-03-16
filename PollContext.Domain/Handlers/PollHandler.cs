using Flunt.Notifications;
using PollContext.Domain.Commands;
using PollContext.Domain.Entities;
using PollContext.Domain.Repositories;
using PollContext.Domain.ValueObjects;
using PollContext.Shared.Commands;
using PollContext.Shared.Commands.Contracts;
using PollContext.Shared.Handlers.Contracts;
using System;

namespace PollContext.Domain.Handlers
{
    public class PollHandler : Notifiable, IHandler<CreatePollCommand>, IHandler<UpdateViewsPollCommand>, IHandler<VoteOptionPollCommand>
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

            _pollRepository.CreatePoll(poll);

            //TODO: retornar o obj DTO para evitar retornar nossa entity0
            return new GenericCommandResult(true, "Enquete salva com sucesso", poll);
        }

        public ICommandResult Handle(UpdateViewsPollCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(VoteOptionPollCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
