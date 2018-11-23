using System;
using System.Diagnostics;

namespace Pluto.Rover
{
    /// <summary>
    /// Modelize the map to be loaded and read by the rover
    /// </summary>
    public class Pluto
    {
        public const char ObsctacleChar = 'o';
        public const char LandChar = '.';
        private readonly int nbObstacles;

        public int Size { get; }
        public char[,] Map { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="nbObstacles"></param>
        public Pluto(uint size, uint nbObstacles)
        {
            if (size == 1)
            {
                throw new GridConfigurationException("Map should be at list 2 x 2");
            }

            if (size * size <= (nbObstacles + 1))
            {
                throw new GridConfigurationException("Too many obstacles for the size of the map.");
            }
            this.Size = (int) size;
            this.nbObstacles = (int) nbObstacles;
            Map = new char[size, size];
            InitializeLand();
            InitializeObsctacles();
        }

        /// <summary>
        /// Initialize map with random obsctacles according
        /// </summary>
        private void InitializeObsctacles()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            for (var i = 0; i < nbObstacles; i++)
            {
                var x = rand.Next(0, Size);
                var y = rand.Next(0, Size);
                if (!Map[x, y].IsObstacle() && y != 0 && x != 0)
                {
                    Map[x, y] = ObsctacleChar;
                }
                else
                {
                    i--;
                }
            }
        }

        /// <summary>
        /// Initialize the map with land
        /// </summary>
        private void InitializeLand()
        {
            for (var x = 0; x < Size; x++)
            {
                for (var y = 0; y < Size; y++)
                {
                    Map[x, y] = LandChar;
                }
            }
        }

        /// <summary>
        /// Display the grid in the debug output
        /// </summary>
        public void Display()
        {
            for (var y = Size -1; y >= 0; y--)
            { 
                for (var x = 0; x < Size; x++)
                {
                    Debug.Write(Map[x, y] + " ");
                }
                Debug.Write(Environment.NewLine);
            }
            Debug.WriteLine(" ");
        }
    }
}