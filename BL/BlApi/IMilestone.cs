namespace BlApi;

public interface IMilestone
{
    public void CreateProjectSchedule();
    public BO.Milestone GetMilestoneDetails(int milestoneNum);
    public BO.Milestone UpdateMilestone(int milestoneNum);
    //public BO.TaskOnList GetTaskOnListDetails(int numberPreviousTask);


}
