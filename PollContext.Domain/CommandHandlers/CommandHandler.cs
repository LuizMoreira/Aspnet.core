using Flunt.Notifications;
using PollContext.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollContext.Domain.CommandHandlers
{
    public class CommandHandler:Notifiable
    {
        private readonly IUow _uow;

        public CommandHandler(IUow uow)
        {
            _uow = uow;
        }


        public bool Commit()
        {
            if (_uow.Commit()) return true;
            //_bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }
    }
}
