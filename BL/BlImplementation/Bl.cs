
using BlApi;

namespace BlImplementation;

internal class Bl : IBl
{
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer =>  new EngineerImplementation();

    public void Reset()
    {
        DalApi.IDal _dal = DalApi.Factory.Get;
        _dal.Reset();
    }
}
