
namespace Dal;
using DalApi;
using System.Diagnostics;

//Stage 3
sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IEngineer Engineer => new EngineerImplementation();

    public IDependence Dependence => new DependenceImplementation();

    public ITask Task => new TaskImplementation();

    public void Reset()
    {
        Engineer.Reset();
        Task.Reset();
        Dependence.Reset();
    }
}

