﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollContext.Domain.Entities;
using PollContext.Domain.ValueObjects;

namespace PollContext.Test.EntitiesTest
{
    [TestClass]
    public class PollEntityTests
    {
        [TestMethod]
        public void Poll_whenCreateObject2_returnPoll()
        {
            DescriptionVO description = new DescriptionVO("poll 1");
            Poll poll = new Poll(description);

            DescriptionVO option1 = new DescriptionVO("poll 1");
            DescriptionVO option2 = new DescriptionVO("poll 1");
            OptionPoll optionPoll1 = new OptionPoll(option1);
            OptionPoll optionPoll2 = new OptionPoll(option2);

            poll.addOptions(optionPoll1);
            poll.addOptions(optionPoll2);

            Assert.AreEqual(0, poll.Notifications.Count);
        }

        [TestMethod]
        public void Poll_whenCreateObject3_returnNotification()
        {
            DescriptionVO description = new DescriptionVO("");
            Poll poll = new Poll(description);

            DescriptionVO option1 = new DescriptionVO("poll 1");
            DescriptionVO option2 = new DescriptionVO("poll 1");
            OptionPoll optionPoll1 = new OptionPoll(option1);
            OptionPoll optionPoll2 = new OptionPoll(option2);

            poll.addOptions(optionPoll1);
            poll.addOptions(optionPoll2);

            Assert.AreEqual(3, poll.Notifications.Count);

        }
    }
}
