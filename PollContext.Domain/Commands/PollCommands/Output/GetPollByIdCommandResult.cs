using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Commands.PollCommands.Output
{
    public class GetPollByIdCommandResult : ICommandResult
    {
        public GetPollByIdCommandResult(Guid poll_id, string poll_description)
        {
            Poll_id = poll_id;
            this.poll_description = poll_description;
            options = new List<GetOptionsPollByPolIdCommandResult>();
        }

        public Guid Poll_id { get; set; }

        public string poll_description { get; set; }

        public List<GetOptionsPollByPolIdCommandResult> options { get; set; }


    }
}
