using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Commands.OptionPollCommands.Input
{
    public class VoteOptionPollCommand : Notifiable, ICommand
    {

        public VoteOptionPollCommand()
        {

        }


        public VoteOptionPollCommand(Guid option_Id)
        {
            Option_Id = option_Id;
        }

        public Guid Poll_Id { get; set; }

        public Guid Option_Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                           new Contract()
                           .Requires()
                           .IsNotNull(Poll_Id, "VoteOptionPollCommand.Poll_Id", "Identificação da enquete é obrigatória")
                           .IsNotNull(Option_Id, "VoteOptionPollCommand.Option_Id", "Identificação do item a ser votado é obrigatório"));
        }
    }
}
