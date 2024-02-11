using BO;
using DalApi;

namespace BlApi;

public interface ITask
{
    public IEnumerable<BO.Task> GetAllTasks();
    public BO.Task GetTaskDetails(int taskNumber);
    public void AddTask(BO.Task task);
    public void UpdateTask(BO.Task task);
    public void RemoveTask(int taskNumber);
    public BO.TaskOnList GetTaskOnListDetails(int numberPreviousTask);

    public DateTime GetEstimatedCompletionDate(DateTime EstimatedStartDate,
      DateTime ActualStartDate, int RequiredEffortTime);
    public List<BO.TaskOnList> getDependenciesList(int taskNumber);
    //public MilestoneOnList getRelatedMilestone(int taskNumber);
    //public MilestoneInTask getRelatedMilestoneInTask(int taskNumber);

}
