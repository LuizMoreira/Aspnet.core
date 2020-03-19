using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Commands.PollCommands.Output
{
    public class CreatePollCommandResult : ICommandResult
    {
        public CreatePollCommandResult(Guid poll_id)
        {
            Poll_id = poll_id;
        }

        public Guid Poll_id { get; set; }

    }
}
