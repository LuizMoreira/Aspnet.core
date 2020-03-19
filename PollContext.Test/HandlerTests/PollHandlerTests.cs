using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollContext.Domain.Commands.PollCommands.Input;
using PollContext.Domain.Entities;
using PollContext.Domain.Handlers;
using PollContext.Domain.ValueObjects;
using PollContext.Shared.Commands;
using PollContext.Test.Repositories;
using System.Collections.Generic;

namespace PollContext.Test.HandlerTests
{

    [TestClass]
    public class PollHandlerTests
    {

        private readonly CreatePollCommand _createPollCommandValid;
        private readonly CreatePollCommand _createPollCommandInvalid;
        private readonly PollHandler _handler;
        private List<Poll> _polls;

        public PollHandlerTests()
        {
            //Arrange
            _createPollCommandValid = new CreatePollCommand();
            _createPollCommandValid.Poll_Description = "poll 1";
            string option1 = "option 1";
            string option2 = "option 2";
            string option3 = "option 3";

            _createPollCommandValid.Options = new List<string>
            {
                option1, option2, option3
            };

            _createPollCommandInvalid = new CreatePollCommand();
            _createPollCommandInvalid.Poll_Description = "";
            _createPollCommandInvalid.Options = new List<string>
            {
                "", "", ""
            };

            _polls = new List<Poll>();

            var poll1 = new Poll(new DescriptionVO("poll 1"));
            poll1.addOptions(new OptionPoll(new DescriptionVO("opt 1")));
            poll1.addOptions(new OptionPoll(new DescriptionVO("opt 2")));

            var poll2 = new Poll(new DescriptionVO("poll 2"));
            poll2.addOptions(new OptionPoll(new DescriptionVO("opt 3")));
            poll2.addOptions(new OptionPoll(new DescriptionVO("opt 4")));

            _polls.Add(poll1);
            _polls.Add(poll2);

            _handler = new PollHandler(new FakePollRepository());

            //ACT
            _createPollCommandValid.Validate();
            _createPollCommandInvalid.Validate();

        }


        [TestMethod]
        public void ShouldReturnGenericCommandResultSucessTrueWhenCreatePollCommandIsValid()
        {

            var result = (GenericCommandResult)_handler.Handle(_createPollCommandValid);

            Assert.IsTrue(_createPollCommandValid.Valid);
            Assert.AreEqual(true, result.Success);
            
        }

        [TestMethod]
        public void ShouldReturnGenericCommandResultSucessFalseWhenUpdateViewsPollCommandIsValid()
        {
            var result = (GenericCommandResult)_handler.Handle(_createPollCommandInvalid);

            Assert.IsFalse(_createPollCommandInvalid.Valid);
            Assert.AreEqual(false, result.Success);
        }

        public void ShouldReturnGenericCommandResultSucessTrueWhenCommandIsValid()
        {

            Assert.Fail();

        }
    }
}
