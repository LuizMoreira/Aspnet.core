using PollContext.Domain.ValueObjects;
using PollContext.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PollContext.Domain.Entities
{
    public class Poll : Entity
    {
        protected IList<OptionPoll> _optionsPoll;

        public Poll()
        {

        }

        public Poll(string description)
        {
            Description = description;
            OptionsPoll = new List<OptionPoll>();
            //caso tenha erro de description na classe descriptionVO, ele é adicionado aqui. 
            //AddNotifications(description);

        }

        public string Description { get; private set; }
        public int Views { get; private set; }

        //Impossibilita que seja adicionado o optionPoll diretamente --> Poll.OptionsPoll.add(Option)
        public IList<OptionPoll> OptionsPoll { get; private set;}
        //public IReadOnlyCollection<OptionPoll> OptionsPoll => _optionsPoll.ToArray();

        public void addOptions(OptionPoll option)
        {
            OptionsPoll.Add(option);
        }

        public void increaseView()
        {
            Views += 1;
        }

    }
}
