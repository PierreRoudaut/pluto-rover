namespace Pluto.Rover
{
    public static class ExtensionMethods
    {
        public static bool IsObstacle(this char c) => c == Pluto.ObsctacleChar;

        public static bool IsLand(this char c) => c == Pluto.LandChar;
    }
}
