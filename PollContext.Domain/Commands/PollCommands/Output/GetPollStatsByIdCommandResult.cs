using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Commands.PollCommands.Output
{
    public class GetPollStatsByIdCommandResult : ICommandResult
    {
        public GetPollStatsByIdCommandResult(Guid poll_id, int views)
        {
            Poll_id = poll_id;
            Views = views;
            options = new List<GetOptionsPollStatsByPolIdCommandResult>();
        }

        public Guid Poll_id { get; set; }

        public int Views { get; set; }

        public List<GetOptionsPollStatsByPolIdCommandResult> options { get; set; }


    }
}
