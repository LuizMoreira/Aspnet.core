using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Commands.PollCommands.Output
{
    public class GetOptionsPollByPolIdCommandResult : ICommandResult
    {
        public GetOptionsPollByPolIdCommandResult(Guid option_id, string option_description)
        {
            Option_id = option_id;
            this.option_description = option_description;
        }

        public Guid Option_id { get; set; }

        public string option_description { get; set; }


    }
}
