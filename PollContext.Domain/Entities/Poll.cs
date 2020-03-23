using PollContext.Domain.ValueObjects;
using PollContext.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PollContext.Domain.Entities
{
    public class Poll : Entity
    {

        public Poll()
        {

        }

        public Poll(string description)
        {
            Description = description;
            _optionsPoll = new List<OptionPoll>();
            //caso tenha erro de description na classe descriptionVO, ele é adicionado aqui. 
            //AddNotifications(description);

        }

        public string Description { get; private set; }

        public int Views { get; private set; }

        private readonly List<OptionPoll> _optionsPoll;

        //Impossibilita que seja adicionado o optionPoll diretamente --> Poll.OptionsPoll.add(Option)
        //public IList<OptionPoll> OptionsPoll { get; private set;}
        public IReadOnlyCollection<OptionPoll> OptionsPoll => _optionsPoll;

        public void addOptions(OptionPoll option)
        {
            _optionsPoll.Add(option);
        }

        public void increaseView()
        {
            Views += 1;
        }

    }
}
