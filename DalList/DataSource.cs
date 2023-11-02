

namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        internal const int startTaskNumber = 1000;
        private static int nextTaskNumber = startTaskNumber;
        internal static int NextTaskNumber { get => nextTaskNumber++; }

        internal const int startIdDependence = 1000;
        private static int nextIdDependence = startIdDependence;
        internal static int NextIdDependence { get => nextIdDependence++; }

    }

    internal static List<DO.Dependence> Dependences {  get; }= new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();

}




