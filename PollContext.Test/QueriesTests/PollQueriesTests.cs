using Microsoft.VisualStudio.TestTools.UnitTesting;
using PollContext.Domain.Entities;
using PollContext.Domain.Queries;
using PollContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PollContext.Test.QueriesTests
{

    [TestClass]
    public class PollQueriesTests
    {

        private List<Poll> _polls;

        public PollQueriesTests()
        {

            //arrange
            _polls = new List<Poll>();

            var poll1 = new Poll(new DescriptionVO("poll 1"));
            var opt1 = new OptionPoll(new DescriptionVO("opt 3"));
            opt1.increaseQty();
            poll1.addOptions(opt1);
            poll1.increaseView();
            var opt2 = new OptionPoll(new DescriptionVO("opt 3"));
            opt2.increaseQty();
            poll1.addOptions(opt2);

            var poll2 = new Poll(new DescriptionVO("poll 2"));
            var opt3 = new OptionPoll(new DescriptionVO("opt 3"));
            opt1.increaseQty();
            poll2.addOptions(opt3);
            var opt4 = new OptionPoll(new DescriptionVO("opt 4"));
            opt3.increaseQty();
            poll2.addOptions(opt4);
            poll2.increaseView();

            _polls.Add(poll1);
            _polls.Add(poll2);


        }


        [TestMethod]
        public void ShouldReturnPollStatsWhenPollIdIsValid()
        {

            //act
            var poll = _polls.AsQueryable().Where(PollQueries.GetById(_polls[0].Id)).FirstOrDefault();

            //Asserts
            Assert.IsNotNull(poll);
            Assert.IsTrue(poll.Equals(_polls[0]));

        }

        [TestMethod]
        public void ShouldReturnNullWhenPollIdIsInvalid()
        {

            //arrange
            var id = Guid.NewGuid();
            //act
            var poll = _polls.AsQueryable().Where(PollQueries.GetById(id)).FirstOrDefault();

            //Asserts
            Assert.IsNull(poll);

        }
    }
}
