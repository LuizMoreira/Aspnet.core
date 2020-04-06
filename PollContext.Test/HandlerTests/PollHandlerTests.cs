using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollContext.Domain.Commands.PollCommands.Input;
using PollContext.Domain.Entities;
using PollContext.Domain.Handlers;
using PollContext.Domain.ValueObjects;
using PollContext.Shared.Commands;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using PollContext.Test.FakeRepositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace PollContext.Test.HandlerTests
{

    [TestClass]
    public class PollHandlerTests
    {

        private readonly CreatePollCommand _createPollCommandValid;
        private readonly CreatePollCommand _createPollCommandInvalid;
        private readonly GetPollByIdCommand _getPollByIdCommandValid;
        private readonly GetPollByIdCommand _getPollByIdCommandInvalid;
        private Mock<ILoggerFactory> _mockLogger;

        private List<Poll> _polls;

        public PollHandlerTests()
        {
            //Arrange
            //_logger = new Lo
            //_logger = logger.CreateLogger("PollContext.Test.HandlerTests");
            _mockLogger = new Mock<ILoggerFactory>();
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



            _getPollByIdCommandValid = new GetPollByIdCommand(Guid.NewGuid());
            _getPollByIdCommandInvalid = new GetPollByIdCommand();
            




            //ACT

        }


        [TestMethod]
        public async Task ShouldReturnGenericCommandResultSucessTrueWhenCreatePollCommandIsValid()
        {

            PollHandler _handler = new PollHandler(new FakePollRepository(), _mockLogger.Object);
            var result = (GenericCommandResult)await _handler.Handle(_createPollCommandValid);

            Assert.IsTrue(_createPollCommandValid.Valid);
            Assert.AreEqual(true, result.Success);

        }

        [TestMethod]
        public async Task ShouldReturnGenericCommandResultSucessTrueWhenCreatePollCommandIsInvalid()
        {
            PollHandler _handler = new PollHandler(new FakePollRepository(), _mockLogger.Object);
            var result = (GenericCommandResult) await _handler.Handle(_createPollCommandInvalid);

            Assert.IsFalse(_createPollCommandInvalid.Valid);
            Assert.AreEqual(true, result.Success);
        }



        [TestMethod]
        public async Task ShouldReturnGenericCommandResultSucessTrueWhenGetPollByIdCommandIsValid()
        {
         PollHandler _handler = new PollHandler(new FakePollRepository(), _mockLogger.Object);
        var result = (GenericCommandResult) await _handler.Handle(_getPollByIdCommandValid);

            Assert.IsTrue(_getPollByIdCommandValid.Valid);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnGenericCommandResultSucessTrueWhenGetPollByIdCommandIsInvalid()
        {
            PollHandler _handler = new PollHandler(new FakePollRepository(), _mockLogger.Object);
            var result = (GenericCommandResult) await _handler.Handle(_getPollByIdCommandInvalid);

            Assert.IsFalse(_getPollByIdCommandInvalid.Valid);
            Assert.AreEqual(true, result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnGenericCommandResultSucessTrueGetPollByIdCommandResultWhenGetPollByIdCommandIsValid()
        {
            PollHandler _handler = new PollHandler(new FakePollRepository(), _mockLogger.Object);
            var result = (GenericCommandResult) await _handler.Handle(_getPollByIdCommandValid);

            Assert.IsTrue(_getPollByIdCommandValid.Valid);
            Assert.AreEqual(true, result.Success);
            Assert.IsNotNull(result);
            //Assert.AreEqual(_getPollByIdCommandValid.Poll_Id, result.Data.id)
        }

    }
}
