using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void CreateProjectSchedule()
    {
        var taskDependencies =
            _dal.Dependence.ReadAll()//list of all pairs of tasks
            .OrderBy(dep => dep.NuberPreviousTask)//all dependends on are ordered
            .GroupBy(dep => dep.NumberDependenceTask,//create IEnumerable of groups
                                                     //where the key is the dependent task and the group is the dependson
            dep => dep.NuberPreviousTask,
           (key, deps) => new { taskNumber = key, Dependencies = deps }).ToList();

        int milestomeNumber = 0;
        var milestones =
            (from deps in taskDependencies.Select(dep => dep.Dependencies).Distinct()
             let milestoneNum = _dal.Task.Create(new(
                 TaskNumber: 0,
                 Description: "",
                 Nickname: "MS" + ++milestomeNumber,
                 Milestone: true,
                 ProductionDate: DateTime.Now,
                 StartDate: DateTime.Now,
                 EstimatedCompletionDate: DateTime.Now,
                 FinalDateForCompletion: DateTime.Now,
                 ActualEndDate: DateTime.Now,
                 Product: null,
                 Notes: null,
                 EngineerId: null,
                 DifficultyLevel: null
                 ))
             select new { MilestoneNum = milestomeNumber, DependenciesList = deps }
             ).ToList();
        foreach (
            var depId in from task in taskDependencies
                         from milestone in milestones
                         where task.Dependencies.SequenceEqual(milestone.DependenciesList)
                         let dependencyId = _dal.Dependence.Create(new DO.Dependence(0, task.taskNumber, 0))
                         from dep in task.Dependencies
                         select _dal.Dependence.Read(item => item.NumberDependenceTask == task.taskNumber))
            _dal.Dependence.Delete(depId.IdDependence);

        foreach (var milestone in milestones)
        {
            {
                foreach (var dep in milestone.DependenciesList)
                {
                    _dal.Dependence.Create(new DO.Dependence(0, milestone.MilestoneNum, dep));
                }
            }
        }
    }


    public BO.Milestone GetMilestoneDetails(int milestoneNum)
    {
        try
        {
            DO.Task? doMilestone = _dal.Task.Read(d => d.TaskNumber == milestoneNum);
            return new BO.Milestone()
            {
                MilestoneNum = doMilestone.TaskNumber,
                NickName = doMilestone.Nickname,
                Description = doMilestone.Description,
                ProductionDate = (DateTime)doMilestone.ProductionDate,
                ActualEndDate = (DateTime)doMilestone.ActualEndDate,
                EstimatedCompletionDate = (DateTime)doMilestone.EstimatedCompletionDate,
                FinalDateForCompletion = (DateTime)doMilestone.FinalDateForCompletion,
                Notes = doMilestone.Notes,
                progressPercentage = 0, //to change
                StartDate = (DateTime)doMilestone.StartDate,
                Status = getStatus((DateTime)doMilestone.EstimatedCompletionDate, (DateTime)doMilestone.ActualEndDate, (DateTime)doMilestone.FinalDateForCompletion),
                DependenciesList = getDependenciesList(milestoneNum)
            };
            throw new Exception("error");
        }
        catch {
            throw new Exception();
        }

    }

    private List<BO.Task> getDependenciesList(int milestoneNum)
    {
        try
        {
            TaskImplementation task = new();
            return getDependenciesList(milestoneNum);
        }
        catch (Exception ex)
        {
            throw new Exception("");
        }
    }

    public BO.Milestone UpdateMilestone(int milestoneNum)
    {
        throw new NotImplementedException();
    }


    public BO.Status getStatus( DateTime estimatedStartDate,
                                 DateTime actualStartDate,
                                 ///*DateTime estimatedCompletionDate*/,
                                 DateTime finalDateForCompletion
                                    //,DateTime actualEndDate
)
    {
        //לא מתוכנן
        if (estimatedStartDate > DateTime.Now)
            return BO.Status.Unscheduled;
        //מתוזמן    
        if (actualStartDate < DateTime.Now)
            return BO.Status.Scheduled;
        //בסכנה
        if ((finalDateForCompletion - DateTime.Now).TotalDays <= 3)
            return BO.Status.InJeopardy;
        return BO.Status.OnTrack;

    }

  
}
