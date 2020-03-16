using PollContext.Domain.Entities;
using PollContext.Domain.ValueObjects;
using Xunit;

namespace PollContext.Tests.Entities
{
    public class PollTests
    {

        [Fact]
        public void Poll_whenCreateObject_returnPoll()
        {
            DescriptionVO description = new DescriptionVO("poll 1");
            Poll poll = new Poll(description);

            DescriptionVO option1 = new DescriptionVO("poll 1");
            DescriptionVO option2 = new DescriptionVO("poll 1");
            OptionPoll optionPoll1 = new OptionPoll(option1);
            OptionPoll optionPoll2 = new OptionPoll(option2);

            poll.addOptions(optionPoll1);
            poll.addOptions(optionPoll2);

            Assert.Equal(0 , poll.Notifications.Count);
        }

        [Fact]
        public void Poll_whenCreateObject_returnNotification()
        {
            DescriptionVO description = new DescriptionVO("");
            Poll poll = new Poll(description);

            DescriptionVO option1 = new DescriptionVO("");
            DescriptionVO option2 = new DescriptionVO("");
            OptionPoll optionPoll1 = new OptionPoll(option1);
            OptionPoll optionPoll2 = new OptionPoll(option2);

            poll.addOptions(optionPoll1);
            poll.addOptions(optionPoll2);

            Assert.Equal(3, poll.Notifications.Count);

        }
    }
}
