using System;

namespace Pluto.Rover
{
    public class ObsctacleEncounteredException : Exception
    {
        public ObsctacleEncounteredException((int x, int y) newCoordinates) :
            base($"Cannot move to coordinate ({newCoordinates.x},{newCoordinates.y}). An obstacle was encoutered.")
        {
        }
    }
}