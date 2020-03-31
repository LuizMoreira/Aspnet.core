using PollContext.Domain.ValueObjects;
using PollContext.Shared.Entities;
using System;

namespace PollContext.Domain.Entities
{
    public class OptionPoll : Entity
    {
        public OptionPoll()
        {
           
        }

        public OptionPoll(DescriptionVO description)
        {
            Description = description;
            //caso tenha erro de description na classe descriptionVO, ele é adicionado aqui. 
            AddNotifications(description);

        }

        public DescriptionVO Description { get; private set; }

        public Guid Poll_Id { get; private set; }

        public Poll Poll { get; private set; }

        public int Qty { get; private set; }

        public void increaseQty()
        {
            Qty += 1;
        }

    }
}
