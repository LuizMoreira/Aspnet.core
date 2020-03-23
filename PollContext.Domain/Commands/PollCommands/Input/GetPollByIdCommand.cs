using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Commands.PollCommands.Input
{
    public class GetPollByIdCommand : Notifiable, ICommand
    {

        public GetPollByIdCommand()
        {

        }

        public GetPollByIdCommand(Guid poll_Id)
        {
            Poll_Id = poll_Id;
        }

        public Guid Poll_Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                           new Contract()
                           .Requires()
                           .IsNotNull(Poll_Id, "VoteOptionPollCommand.Poll_Id", "Identificação da enquete é obrigatória"));

                    }
    }
}
