namespace BlApi;

public interface ITask
{
    public IEnumerable<BO.Task> GetAllTask();
    public BO.Task GetTaskDetails(int taskNumber);
    public void AddTask(BO.Task task);
    public void UpdateTask(BO.Task task);
    public void RemoveTask(int taskNumber);

}
