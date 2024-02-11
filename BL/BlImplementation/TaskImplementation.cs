using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    EngineerImplementation engineerImplementation = new EngineerImplementation();
    private DalApi.IDal _dal = DalApi.Factory.Get;

    //פונקציית עזר שמחזירה רשימה של תלויות עבור מספר משימה
    public List<BO.TaskOnList> getDependenciesList(int taskNumber)
    {
        try
        {
            List<Dependence> allDependencies = new List<Dependence>();
            allDependencies = _dal.Dependence.ReadAll().Where(d => d.NumberDependenceTask == taskNumber).ToList();


            //return allDependencies.Select(d => GetTaskDetails(d.NuberPreviousTask)).ToList();
            return allDependencies.Select(d => GetTaskOnListDetails(d.NuberPreviousTask)).ToList();
        }
        catch (Exception ex) 
        {
            throw new Exception("");
        }
    }

    public BO.TaskOnList GetTaskOnListDetails(int numberPreviousTask)
    {
        DO.Task? doTask = _dal.Task.Read(t => t.TaskNumber == numberPreviousTask);
        if (doTask == null)
        {
            throw new Exception("Task not found");
        }
        return new BO.TaskOnList()
        {
            TaskNumber = doTask.TaskNumber,
            Description = doTask.Description,
            Nickname = doTask.Nickname,
            Status = getStatus((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (DateTime)doTask.FinalDateForCompletion),

        };
    }

    //פונקציית עזר להחזרת הסטטוס המתאים עבור כל משימה בהתאם לתאריכים של המשימה ולתאריך של היום
    public BO.Status getStatus(
     DateTime estimatedStartDate, //תאריך משוער להתחלה
     DateTime actualStartDate, // תאריך התחלה בפועל
     ///*DateTime estimatedCompletionDate*/,
     DateTime finalDateForCompletion //תאריך סופי לסיום
        //,DateTime actualEndDate 
        )
    {
        //לא מתוכנן
        if (estimatedStartDate ==null)
            return BO.Status.Unscheduled;
        //מתוזמן   
        else if (estimatedStartDate != null)
            return BO.Status.Scheduled;
        //בוצע
        if (finalDateForCompletion != null)
            return BO.Status.Done;
        //במעקב
        if (finalDateForCompletion > DateTime.Now &&
            actualStartDate < DateTime.Now)
        return BO.Status.OnTrack;
       return BO.Status.Unscheduled;

    }
    public void AddTask(BO.Task task)
    {
        try
        {
            if (task.TaskNumber > 0 &&
                task.Nickname != ""
                //&& !string.IsNullOrEmpty(task.Nickname)
                )
            {
                DO.Task doTask = new DO.Task(
                    task.TaskNumber,
                    task.Description,
                    task.Nickname,
                    //true,
                    task.ProductionDate,
                    task.ActualStartDate,
                    task.EstimatedStartDate,
                    task.RequiredEffortTime,
                    task.EstimatedCompletionDate,
                    //task.FinalDateForCompletion,
                    task.ActualEndDate,
                    task.Product,
                    task.Notes,
                    task.eng!.IdEngineer,
                    (DO.Levels)(int)Enum.Parse(typeof(DO.Levels),task.DifficultyLevel.ToString())    
                    );
                    int numTask = _dal.Task.Create(doTask);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        catch (DO.DalAlreayExistException)
        {

        }
    }

    public DateTime GetEstimatedCompletionDate(DateTime EstimatedStartDate,
       DateTime ActualStartDate, int RequiredEffortTime)
    {
        TimeSpan timeToAdd = TimeSpan.FromHours(RequiredEffortTime); // אם RequiredEffortTime מייצג שעות
        if (EstimatedStartDate > ActualStartDate)
            return EstimatedStartDate.Add(timeToAdd);
        else
            return ActualStartDate.Add(timeToAdd);

    }
    public IEnumerable<BO.Task> GetAllTasks()
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()

                select new BO.Task
                {
                    TaskNumber = doTask.TaskNumber,
                    Description = doTask.Description,
                    Nickname = doTask.Nickname,
                    Status = getStatus((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (DateTime)doTask.FinalDateForCompletion),
                    DependenciesList=getDependenciesList(doTask.TaskNumber),
                    ProductionDate = (DateTime)doTask.CreatedAtDate,
                    //RelatedMileStone = getRelatedMilestoneInTask(doTask.TaskNumber),
                    EstimatedStartDate = (DateTime)doTask.EstimatedStartDate,
                    ActualStartDate = (DateTime)doTask.StartDate,
                    EstimatedCompletionDate = GetEstimatedCompletionDate((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (int)doTask.RequiredEffortTime),
                    //FinalDateForCompletion = (DateTime)doTask.FinalDateForCompletion,
                    ActualEndDate = (DateTime)doTask.ActualEndDate,
                    RequiredEffortTime=(int)doTask.RequiredEffortTime,
                    Product=doTask.Product,
                    Notes = doTask.Notes,
                    eng = engineerImplementation.GetEngineerDetails(doTask.EngineerId ?? 0),
                    DifficultyLevel =(BO.Levels)Enum.Parse(typeof(BO.Levels),doTask.DifficultyLevel.ToString())
                }) ;
    }

    //public MilestoneOnList getRelatedMilestone(int taskNumber)
    //{
    //    try
    //    {
    //        var depenedcies = (from dep in _dal.Dependence.ReadAll(d => d.NuberPreviousTask == taskNumber).ToList()
    //                          let id = dep.NuberPreviousTask
    //                          where _dal.Task.Read(t => t.TaskNumber == id).Milestone
    //                          select _dal.Task.Read(t => t.TaskNumber == id)
    //                          ).FirstOrDefault();
    //        return new MilestoneOnList() {
    //            Id = depenedcies.TaskNumber,
    //            Description = depenedcies.Description, 
    //            Nickname = depenedcies.Nickname,
    //            ProductionDate = (DateTime)depenedcies.ProductionDate,
    //            progressPercentage = 0, 
    //            Status = getStatus((DateTime)depenedcies.EstimatedCompletionDate, 
    //            (DateTime)depenedcies.ActualEndDate,
    //            (DateTime)depenedcies.FinalDateForCompletion) };

    //    }
    //    catch(Exception ex)
    //    {
    //        throw new Exception();
    //    }
    //}
    //public MilestoneInTask getRelatedMilestoneInTask(int taskNumber)
    //{
    //    try
    //    {
    //        var depenedcies = (from dep in _dal.Dependence.ReadAll(d => d.NuberPreviousTask == taskNumber).ToList()
    //                           let id = dep.NuberPreviousTask
    //                           where _dal.Task.Read(t => t.TaskNumber == id).Milestone
    //                           select _dal.Task.Read(t => t.TaskNumber == id)
    //                          ).FirstOrDefault();
    //        return new MilestoneInTask()
    //        {
    //            MilestoneNum = taskNumber,
    //            Nickname = depenedcies.Nickname
    //        };

    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception();
    //    }
    //}

    public BO.Task GetTaskDetails(int taskNumber)
    {
        DO.Task? doTask=_dal.Task.Read(t=>t.TaskNumber == taskNumber);
        if(doTask == null)
        {
            throw new Exception("Task not found");
        }
        return new BO.Task()
        {
            TaskNumber = doTask.TaskNumber,
            Description = doTask.Description,
            Nickname = doTask.Nickname,
            Status = getStatus((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (DateTime)doTask.FinalDateForCompletion),
            DependenciesList = getDependenciesList(doTask.TaskNumber),
            ProductionDate = (DateTime)doTask.CreatedAtDate,
            //RelatedMileStone = getRelatedMilestoneInTask(doTask.TaskNumber),
            EstimatedStartDate = (DateTime)doTask.EstimatedStartDate,
            ActualStartDate = (DateTime)doTask.StartDate,
            EstimatedCompletionDate = GetEstimatedCompletionDate((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (int)doTask.RequiredEffortTime),
            //FinalDateForCompletion = (DateTime)doTask.FinalDateForCompletion,
            ActualEndDate = (DateTime)doTask.ActualEndDate,
            RequiredEffortTime = (int)doTask.RequiredEffortTime,
            Product = doTask.Product,
            Notes = doTask.Notes,
            eng = engineerImplementation.GetEngineerDetails(doTask.EngineerId ?? 0),
            DifficultyLevel = (BO.Levels)Enum.Parse(typeof(BO.Levels), doTask.DifficultyLevel.ToString())
        };
    }

    public void RemoveTask(int taskNumber)
    {
        try
        {
            if (_dal.Task.ReadAll().Any(t => t?.TaskNumber == taskNumber) == false)
                throw new Exception("Task not exist");
            if (_dal.Dependence.ReadAll().Any(d => d?.NuberPreviousTask == taskNumber))
                throw new Exception("DEpence");
            _dal.Task.Delete(taskNumber);

        }
        catch (Exception ex) { }
    }

    public void UpdateTask(BO.Task task)
    {
        try 
        {
            if (task.TaskNumber > 0 &&task.Nickname != "")
            {
                DO.Task doTask = new DO.Task(
                  task.TaskNumber,
                    task.Description,
                    task.Nickname,
                    //true,
                    task.ProductionDate,
                    task.ActualStartDate,
                    task.EstimatedStartDate,
                    task.RequiredEffortTime,
                    task.EstimatedCompletionDate,
                    //task.FinalDateForCompletion,
                    task.ActualEndDate,
                    task.Product,
                    task.Notes,
                    task.eng!.IdEngineer,
                    (DO.Levels)(int)Enum.Parse(typeof(DO.Levels), task.DifficultyLevel.ToString())
                  ) ;
                _dal.Task.Update(doTask);
            }
               
        }
        catch { }
    }
}
