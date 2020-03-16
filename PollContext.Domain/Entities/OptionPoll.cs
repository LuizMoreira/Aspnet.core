using PollContext.Domain.ValueObjects;
using PollContext.Shared.Entities;

namespace PollContext.Domain.Entities
{
    public class OptionPoll : Entity
    {

        public OptionPoll(DescriptionVO description)
        {
            Description = description;
            //caso tenha erro de description na classe descriptionVO, ele é adicionado aqui. 
            AddNotifications(description);

        }

        //set private evita manipulação fora da criação da classe; caso seja necessário alterar, vms criar um método de alterar a propriedade
        public DescriptionVO Description { get; private set; }

        public int Poll_Id { get; private set; }

        public Poll Poll { get; private set; }

        public int Qty { get; private set; }

    }
}
