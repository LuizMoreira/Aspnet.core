using Flunt.Notifications;
using PollContext.Domain.Commands.OptionPollCommands.Input;
using PollContext.Domain.Repositories;
using PollContext.Shared.Commands;
using PollContext.Shared.Commands.Contracts;
using PollContext.Shared.Handlers.Contracts;

namespace PollContext.Domain.Handlers
{
    public class OptionPollHandler : Notifiable, IHandler<VoteOptionPollCommand>
    {
        private readonly IOptionPollRepository _optionPollRepository;

        public OptionPollHandler(IOptionPollRepository optionPollRepository)
        {
            _optionPollRepository = optionPollRepository;
        }


        public ICommandResult Handle(VoteOptionPollCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Enquete inválida", command.Notifications);

            //obtem a enquete por id e poll id
            var optionPoll = _optionPollRepository.GetOptionPollById(command.Option_Id, command.Poll_Id);

            //incrementa a qty
            optionPoll.increaseQty();

            // salva a alteração feita na qtd
            _optionPollRepository.Update(optionPoll);

            //TODO: retornar o obj DTO para evitar retornar nossa entity
            return new GenericCommandResult(true, "Enquete salva com sucesso", optionPoll);
        }
    }
}
