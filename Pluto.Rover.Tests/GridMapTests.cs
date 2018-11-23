
using System;
using NUnit.Framework;

namespace Pluto.Rover.Tests
{
    [TestFixture]
    public class GridMapTests
    {
        [Test]
        public void WrongConfigurationTest()
        {
            Assert.Throws<GridConfigurationException>(() =>
            {
                var map = new Pluto(2, 4);
            });

            Assert.Throws<GridConfigurationException>(() =>
            {
                var map = new Pluto(1, 0);
            });
        }

        [Test]
        public void InitializeTest()
        {
            uint size = 3;
            var expectedObstacles = 3;
            var expectedLand = (size * size) - expectedObstacles;
            var grid = new Pluto(size, 3);
            var obstaclesCount = 0;
            var landCount = 0;

            foreach (var c in grid.Map)
            {
                if (c.IsObstacle())
                {
                    obstaclesCount++;
                }

                if (c.IsLand())
                {
                    landCount++;
                }
            }
            grid.Display();
            Assert.AreEqual(expectedObstacles, obstaclesCount);
            Assert.AreEqual(expectedLand, landCount);
        }
    }
}
