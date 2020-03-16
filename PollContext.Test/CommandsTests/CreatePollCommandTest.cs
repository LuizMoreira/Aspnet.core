using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollContext.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollContext.Test.CommandsTests
{

    [TestClass]
    public class CreatePollCommandTest
    {

        CreatePollCommand _pollCommand;
        CreatePollCommand _pollCommandFail;

        public CreatePollCommandTest()
        {
            _pollCommand = new CreatePollCommand();
            _pollCommand.Poll_Description = "poll 1";
            string option1 = "option 1";
            string option2 = "option 2";
            string option3 = "option 3";
            
            _pollCommand.Options = new List<string>
            {
                option1, option2, option3
            };

            _pollCommandFail = new CreatePollCommand();
            _pollCommandFail.Poll_Description = "";
            _pollCommandFail.Options = new List<string>
            {
                "", "", ""
            };
        }


        [TestMethod]
        public void ShouldReturnErrorWhenDescriptionIsValid()
        {
            _pollCommand.Validate();
            Assert.IsTrue(_pollCommand.Valid);
            Assert.AreEqual(0, _pollCommand.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenDescriptionIsInvalid()
        {
            _pollCommandFail.Validate();
            Assert.IsFalse(_pollCommandFail.Valid);
            Assert.AreEqual(8, _pollCommandFail.Notifications.Count);

        }
    }
}
