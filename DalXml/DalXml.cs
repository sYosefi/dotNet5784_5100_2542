
namespace Dal;
using DalApi;
//Stage 3
sealed public class DalXml : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public IDependence Dependence => new DependenceImplementation();

    public ITask Task => new TaskImplementation();
}
