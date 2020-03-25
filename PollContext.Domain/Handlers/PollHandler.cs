using Flunt.Notifications;
using PollContext.Domain.Commands.PollCommands.Input;
using PollContext.Domain.Commands.PollCommands.Output;
using PollContext.Domain.Entities;
using PollContext.Domain.Repositories;
using PollContext.Domain.ValueObjects;
using PollContext.Shared.Commands;
using PollContext.Shared.Commands.Contracts;
using PollContext.Shared.Handlers.Contracts;
using System.Collections.Generic;

namespace PollContext.Domain.Handlers
{
    public class PollHandler : Notifiable, IHandler<CreatePollCommand>, IHandler<GetPollByIdCommand>, IHandler<GetPollStatsByIdCommand>
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

            List<OptionPoll> opt = new List<OptionPoll>();
            foreach (var item in command.Options)
            {
                DescriptionVO vo = new DescriptionVO(item);
                OptionPoll option = new OptionPoll(vo);
                poll.addOptions(option);
            }

            _pollRepository.Create(poll);

            return new GenericCommandResult(true, "Enquete gravada com sucesso", new CreatePollCommandResult(poll.Id));
        }

        public ICommandResult Handle(GetPollByIdCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Enquete inválida", command.Notifications);

            //obtem a enquete por id
            var poll = _pollRepository.GetById(command.Poll_Id);
            
            if(poll==null) return new GenericCommandResult(false, "Enquete não encontrada", null);
            //incrementa a visualização
            poll.increaseView();

            // salva a alteração feita na views
            _pollRepository.Update(poll);

            GetPollByIdCommandResult getPollByIdCommandResult = new GetPollByIdCommandResult(poll.Id, poll.Description.Description);
            foreach (var item in poll.OptionsPoll)
            {
                getPollByIdCommandResult.options.Add(new GetOptionsPollByPolIdCommandResult(item.Id, item.Description.Description));
            }

            return new GenericCommandResult(true, "Enquete gravada com sucesso", getPollByIdCommandResult);
        }

        public ICommandResult Handle(GetPollStatsByIdCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Enquete inválida", command.Notifications);

            //obtem a enquete por id
            var poll = _pollRepository.GetById(command.Poll_Id);

            if (poll == null) return new GenericCommandResult(false, "Enquete não encontrada", null);

            GetPollStatsByIdCommandResult getPollStatsByIdCommandResult = new GetPollStatsByIdCommandResult(poll.Id, poll.Views);
            foreach (var item in poll.OptionsPoll)
            {
                getPollStatsByIdCommandResult.options.Add(new GetOptionsPollStatsByPolIdCommandResult(item.Id, item.Qty));
            }

            return new GenericCommandResult(true, "Enquete gravada com sucesso", getPollStatsByIdCommandResult);
        }

    }
}
