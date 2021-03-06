﻿using PollContext.Shared.Queries.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollContext.Shared.Queries
{
    public class GenericQueryResult : IQueryResult
    {
        public GenericQueryResult()
        {
        }

        public GenericQueryResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

    }
}
