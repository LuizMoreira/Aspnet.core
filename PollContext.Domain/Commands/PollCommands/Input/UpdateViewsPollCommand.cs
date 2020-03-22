using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Commands.PollCommands.Input
{
    public class UpdateViewsPollCommand : Notifiable, ICommand
    {

        public UpdateViewsPollCommand()
        {

        }

        public UpdateViewsPollCommand(Guid poll_Id)
        {
            Poll_Id = poll_Id;
        }

        public Guid Poll_Id { get; set; }

        public int Views { get; set; }


        public void Validate()
        {
            AddNotifications(
                           new Contract()
                           .Requires()
                           .IsNotNull(Poll_Id, "VoteOptionPollCommand.Poll_Id", "Identificação da enquete é obrigatória")
                           .IsGreaterOrEqualsThan(Views, 0, "UpdateViewsPollCommand.Views", "A quantidade de views não pode ser negativa"));

                    }
    }
}
