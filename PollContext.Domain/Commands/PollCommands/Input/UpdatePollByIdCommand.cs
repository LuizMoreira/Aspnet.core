using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Commands.PollCommands.Input
{
    public class UpdatePollByIdCommand : Notifiable, ICommand
    {

        public UpdatePollByIdCommand()
        {

        }

        public UpdatePollByIdCommand(Guid poll_Id)
        {
            Poll_Id = poll_Id;
        }

        public Guid Poll_Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                           new Contract()
                           .Requires()
                           .IsNotEmpty(Poll_Id, "Poll_Id", "Identificador é obrigatório"));

                    }
    }
}
