using DalApi;
using DO;

namespace Dal
{
    sealed internal class DalList : IDal
    {

        public static IDal Instance { get; } = new DalList();
        private DalList() { }

        public IEngineer Engineer =>  new EngineerImplementation();

        public IDependence Dependence =>  new DependenceImplementation();

        public ITask Task => new TaskImplementation();

        public void Reset()
        {
            Engineer.Reset();
            Task.Reset();
            Dependence.Reset();
        }
    }
}
