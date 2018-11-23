using System;
using System.Collections.Generic;

namespace Pluto.Rover
{
    public class Rover
    {
        private readonly Pluto map;
        public (int x, int y) Coordinates { get; private set; }

        private CardinalPoint compass;

        private readonly Dictionary<CommandType, Action> commands;
        private static readonly Dictionary<CardinalPoint, char> RoverOrientations = new Dictionary<CardinalPoint, char>
        {
            {CardinalPoint.North, '^' },
            {CardinalPoint.Est, '>' },
            {CardinalPoint.South, 'v' },
            {CardinalPoint.West, '<' },
        };

        /// <summary>
        /// Rover constructor
        /// </summary>
        /// <param name="map"></param>
        /// <param name="initialCoordinates"></param>
        public Rover(Pluto map, (int x, int y) initialCoordinates = default((int x, int y)))
        {
            this.map = map;
            Coordinates = initialCoordinates;
            // initial orientation
            this.compass = CardinalPoint.North;
            commands = new Dictionary<CommandType, Action>
            {
                {CommandType.Forward, MoveForwards},
                {CommandType.Right, PivotRight},
                {CommandType.Left, PivotLeft},
                {CommandType.Backward, MoveBackwards}
            };
            map.Map[Coordinates.x, Coordinates.y] = RoverOrientations[compass];
        }

        /// <summary>
        /// Execute a list of handled commands
        /// </summary>
        /// <param name="command"></param>
        public void ExecuteCommand(string command)
        {
            foreach (var c in command.ToCharArray())
            {
                if (!commands.TryGetValue((CommandType)c, out var action))
                {
                    throw new ApplicationException($"{c}: command not handled");
                }
                action.Invoke();
                map.Display();
            }
        }

        /// <summary>
        /// Rotate the orientation to the left
        /// </summary>
        private void PivotLeft()
        {
            var i = (int)compass - 1;
            i = i < 1 ? 4 : i;
            compass = (CardinalPoint)i;
            map.Map[Coordinates.x, Coordinates.y] = RoverOrientations[compass];
        }

        /// <summary>
        /// Rotate the orientation to the right
        /// </summary>
        private void PivotRight()
        {
            var i = (int)compass + 1;
            i = i > 4 ? 1 : i;
            compass = (CardinalPoint)i;
            map.Map[Coordinates.x, Coordinates.y] = RoverOrientations[compass];
        }

        /// <summary>
        /// Move the rover forward in the current compass direction
        /// </summary>
        private void MoveForwards()
        {
            var newCoordinates = Coordinates;
            switch (compass)
            {
                case CardinalPoint.North:
                    newCoordinates.y = newCoordinates.y + 1 > map.Size ? 0 : newCoordinates.y + 1;
                    break;
                case CardinalPoint.Est:
                    newCoordinates.x = newCoordinates.x + 1 > map.Size ? 0 : newCoordinates.x + 1;
                    break;
                case CardinalPoint.South:
                    newCoordinates.y = newCoordinates.y - 1 < 0 ? map.Size - 1 : newCoordinates.y - 1;
                    break;
                case CardinalPoint.West:
                    newCoordinates.x = newCoordinates.x - 1 < 0 ? map.Size - 1 : newCoordinates.x - 1;
                    break;
            }

            if (map.Map[newCoordinates.x, newCoordinates.y].IsObstacle())
            {
                throw new ObsctacleEncounteredException(newCoordinates);
            }

            map.Map[Coordinates.x, Coordinates.y] = Pluto.LandChar;
            Coordinates = newCoordinates;
            map.Map[Coordinates.x, Coordinates.y] = RoverOrientations[compass];
        }

        /// <summary>
        /// Move the rover backward in the current compass direction
        /// </summary>
        private void MoveBackwards()
        {
            var newCoordinates = Coordinates;
            switch (compass)
            {
                case CardinalPoint.North:
                    newCoordinates.y = newCoordinates.y - 1 < 0 ? map.Size - 1 : newCoordinates.y - 1;
                    break;
                case CardinalPoint.Est:
                    newCoordinates.x = newCoordinates.x - 1 < 0 ? map.Size - 1 : newCoordinates.x - 1;
                    break;
                case CardinalPoint.South:
                    newCoordinates.y = newCoordinates.y + 1 > map.Size ? 0 : newCoordinates.y + 1;
                    break;
                case CardinalPoint.West:
                    newCoordinates.x = newCoordinates.x + 1 > map.Size ? 0 : newCoordinates.x + 1;
                    break;
            }

            if (map.Map[newCoordinates.x, newCoordinates.y].IsObstacle())
            {
                throw new ObsctacleEncounteredException(newCoordinates);
            }

            map.Map[Coordinates.x, Coordinates.y] = Pluto.LandChar;
            Coordinates = newCoordinates;
            map.Map[Coordinates.x, Coordinates.y] = RoverOrientations[compass];
        }
    }
}