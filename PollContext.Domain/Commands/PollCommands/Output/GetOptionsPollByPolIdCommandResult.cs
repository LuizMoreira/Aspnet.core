using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Commands.PollCommands.Output
{
    public class GetOptionsPollByPolIdCommandResult : ICommandResult
    {

        public Guid Option_id { get; set; }

        public string option_description { get; set; }


    }
}
