using NUnit.Framework;
using System;

namespace Pluto.Rover.Tests
{
    public class RoverTests
    {

        //[Test]
        //public void ExecuteCommandTestCaseNotHandled()
        //{

        //}

        [TestCase("RFLFFF", 1, 3)]
        [TestCase("FFRFF", 2, 2)]
        [TestCase("BLFF", 8, 9)]
        [TestCase("LLF", 0, 9)]
        [TestCase("RRB", 0, 1)]
        [TestCase("LB", 1, 0)]
        public void ExecuteCommandTestCaseOk(string command, int expectedX, int expectedY)
        {
            var map = new Pluto(10, 0);
            var expectedCoordinates = (expectedX, expectedY);
            var rover = new Rover(map);
            rover.ExecuteCommand(command);
            Assert.AreEqual(expectedCoordinates, rover.Coordinates);
        }

        [Test]
        public void ExecuteCommandTestCaseNotHandled()
        {
            var map = new Pluto(10, 0);
            var rover = new Rover(map);
            Assert.Throws<ApplicationException>(() => { rover.ExecuteCommand("P"); });
        }

        [Test]
        public void ExecuteCommandTestCaseForwardObstacle()
        {
            var map = new Pluto(10, 0);
            map.Map[1, 1] = Pluto.ObsctacleChar;
            var rover = new Rover(map);
            Assert.Throws<ObsctacleEncounteredException>(() => { rover.ExecuteCommand("FRF"); });
        }

        [Test]
        public void ExecuteCommandTestCaseBackwardObstacle()
        {
            var map = new Pluto(10, 0);
            map.Map[9, 0] = Pluto.ObsctacleChar;
            var rover = new Rover(map);
            Assert.Throws<ObsctacleEncounteredException>(() => { rover.ExecuteCommand("RB"); });
        }

    }
}