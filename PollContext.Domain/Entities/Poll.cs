using PollContext.Domain.ValueObjects;
using PollContext.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PollContext.Domain.Entities
{
    public class Poll : Entity
    {
        private IList<OptionPoll> _optionsPoll;
        public Poll(DescriptionVO description)
        {
            Description = description;
            _optionsPoll = new List<OptionPoll>();
            //caso tenha erro de description na classe descriptionVO, ele é adicionado aqui. 
            AddNotifications(description);

        }

        public DescriptionVO Description { get; private set; }
        public int Views { get; private set; }

        //Impossibilita que seja adicionado o optionPoll diretamente --> Poll.OptionsPoll.add(Option)
        //public IReadOnlyCollection<OptionPoll> OptionsPoll { get { return _optionsPoll.ToArray(); } }
        public IReadOnlyCollection<OptionPoll> OptionsPoll => _optionsPoll.ToArray();

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
