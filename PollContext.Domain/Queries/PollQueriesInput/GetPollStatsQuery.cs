using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace PollContext.Domain.Queries.PollQueriesInput
{
    public class GetPollStatsQuery : Notifiable
    {
        public GetPollStatsQuery(Guid poll_Id)
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
