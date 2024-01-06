using BlApi;

namespace BlImplementation
{
    internal class TaskImplementation : ITask
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public void AddTask(BO.Task task)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Task> GetAllTask()
        {
            throw new NotImplementedException();
        }

        public BO.Task GetTaskDetails(int taskNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveTask(int taskNumber)
        {
            throw new NotImplementedException();
        }

        public void UpdateTask(BO.Task task)
        {
            throw new NotImplementedException();
        }
    }
}
