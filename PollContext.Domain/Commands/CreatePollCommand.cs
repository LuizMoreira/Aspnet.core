using Flunt.Notifications;
using Flunt.Validations;
using PollContext.Shared.Commands.Contracts;
using System.Collections.Generic;

namespace PollContext.Domain.Commands
{
    public class CreatePollCommand : Notifiable, ICommand
    {

        public CreatePollCommand()
        {

        }

        public CreatePollCommand(string poll_Description, List<string> options)
        {
            Poll_Description = poll_Description;
            Options = options;
        }

        public string Poll_Description { get; set; }

        public List<string> Options{ get; set; }

        public void Validate()
        {
            AddNotifications(
                           new Contract()
                           .Requires()
                           .IsNotNullOrEmpty(Poll_Description, "Poll_Description", "Descrição é obrigatória")
                           .HasMinLen(Poll_Description, 3, "Poll_Description", "Descrição deve conter ao menos 3 caracteres.")
                           .IsNotNull(Options, "Options", "É necessário uma lista de itens")
                           .IsGreaterThan(Options.Count,1, "Options", "É necessário uma lista de itens"));

            foreach (var option in Options)
            {
                AddNotifications(
                               new Contract()
                               .Requires()
                               .IsNotNullOrEmpty(option, "Option", "Descrição é obrigatória")
                               .HasMinLen(Poll_Description, 3, "Option", "Descrição deve conter ao menos 3 caracteres."));

            }
        }
    }
}
