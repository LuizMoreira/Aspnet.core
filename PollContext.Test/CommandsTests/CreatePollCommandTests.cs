using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollContext.Domain.Commands.PollCommands.Input;
using System.Collections.Generic;

namespace PollContext.Test.CommandsTests
{

    [TestClass]
    public class CreatePollCommandTests
    {

        private readonly CreatePollCommand _pollCommandValid;
        private readonly CreatePollCommand _pollCommandInvalid;

        public CreatePollCommandTests()
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

            //ACT
            _pollCommandValid.Validate();
            _pollCommandInvalid.Validate();


        }


        [TestMethod]
        public void ShouldReturnErrorWhenDescriptionIsValid()
        {
            //Asserts
            Assert.IsTrue(_pollCommandValid.Valid);
            Assert.AreEqual(0, _pollCommandValid.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenDescriptionIsInvalid()
        {
            //Asserts
            Assert.IsFalse(_pollCommandInvalid.Valid);
            Assert.AreEqual(8, _pollCommandInvalid.Notifications.Count);

        }
    }
}
