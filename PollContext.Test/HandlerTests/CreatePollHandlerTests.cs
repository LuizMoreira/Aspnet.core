using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollContext.Domain.Commands;
using PollContext.Domain.Handlers;
using PollContext.Shared.Commands;
using PollContext.Test.Repositories;
using System.Collections.Generic;

namespace PollContext.Test.HandlerTests
{

    [TestClass]
    public class CreatePollHandlerTests
    {

        private readonly CreatePollCommand _pollCommandValid;
        private readonly CreatePollCommand _pollCommandInvalid;
        private readonly PollHandler _handler;

        public CreatePollHandlerTests()
        {
            //Arrange
            _pollCommandValid = new CreatePollCommand();
            _pollCommandValid.Poll_Description = "poll 1";
            string option1 = "option 1";
            string option2 = "option 2";
            string option3 = "option 3";
            
            _pollCommandValid.Options = new List<string>
            {
                option1, option2, option3
            };

            _pollCommandInvalid = new CreatePollCommand();
            _pollCommandInvalid.Poll_Description = "";
            _pollCommandInvalid.Options = new List<string>
            {
                "", "", ""
            };

             _handler = new PollHandler(new FakePollRepository());

            //ACT
            _pollCommandValid.Validate();
            _pollCommandInvalid.Validate();


        }


        [TestMethod]
        public void ShouldReturnSucessTrueWhenCommandIsValid()
        {

            var result = (GenericCommandResult)_handler.Handle(_pollCommandValid);

            Assert.IsTrue(_pollCommandValid.Valid);
            Assert.AreEqual(true, result.Success);
            
        }

        [TestMethod]
        public void ShouldReturnSucessTrueWhenCommandIsInvalid()
        {
            var result = (GenericCommandResult)_handler.Handle(_pollCommandInvalid);

            Assert.IsFalse(_pollCommandInvalid.Valid);
            Assert.AreEqual(false, result.Success);
        }
    }
}
