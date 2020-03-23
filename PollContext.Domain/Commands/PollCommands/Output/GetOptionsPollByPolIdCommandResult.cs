﻿using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System;
using System.Collections.Generic;

namespace PollContext.Domain.Commands.PollCommands.Output
{
    public class GetOptionsPollStatsByPolIdCommandResult : ICommandResult
    {
        public GetOptionsPollStatsByPolIdCommandResult(Guid option_id, int qty)
        {
            Option_id = option_id;
            Qty = qty;
        }

        public Guid Option_id { get; set; }

        public int Qty { get; set; }


    }
}
