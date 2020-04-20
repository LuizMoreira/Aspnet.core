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

        protected void NotifyValidationErrors(CommandHandler message)
        {
            foreach (var error in message.Notifications)
            {
                //enviar pra bus
                
            }
        }

        public bool Commit()
        {
              
            if (_uow.Commit()) return true;
            
            return false;
        }
    }
}
