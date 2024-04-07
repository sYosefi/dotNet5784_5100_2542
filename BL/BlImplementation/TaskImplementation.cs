using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    EngineerImplementation engineerImplementation = new EngineerImplementation();
    private DalApi.IDal _dal = DalApi.Factory.Get;

    private readonly IBl _bl;
    internal TaskImplementation(IBl bl) => _bl = bl;

    //פונקציית עזר שמחזירה רשימה של תלויות עבור מספר משימה
    public List<BO.TaskOnList> getDependenciesList(int taskNumber)
    {
        try
        {
            List<Dependence> allDependencies = _dal.Dependence.ReadAll().Where(d => d.NumberDependenceTask == taskNumber).ToList();
            return allDependencies.Select(d => GetTaskOnListDetails(d.NuberPreviousTask)).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while retrieving dependencies list: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    //public List<BO.TaskOnList> getDependenciesList(int taskNumber)
    //{
    //    try
    //    {
    //        List<Dependence> allDependencies = new List<Dependence>();
    //        allDependencies = _dal.Dependence.ReadAll().Where(d => d.NumberDependenceTask == taskNumber).ToList();
    //        return allDependencies.Select(d => GetTaskOnListDetails(d.NuberPreviousTask)).ToList();
    //    }
    //    catch (Exception ex) 
    //    {
    //        throw new Exception("");
    //    }
    //}

    public BO.TaskOnList GetTaskOnListDetails(int numberPreviousTask)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while retrieving task details: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    //public BO.TaskOnList GetTaskOnListDetails(int numberPreviousTask)
    //{
    //    DO.Task? doTask = _dal.Task.Read(t => t.TaskNumber == numberPreviousTask);
    //    if (doTask == null)
    //    {
    //        throw new Exception("Task not found");
    //    }
    //    return new BO.TaskOnList()
    //    {
    //        TaskNumber = doTask.TaskNumber,
    //        Description = doTask.Description,
    //        Nickname = doTask.Nickname,
    //        Status = getStatus((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (DateTime)doTask.FinalDateForCompletion),

    //    };
    //}

    //פונקציית עזר להחזרת הסטטוס המתאים עבור כל משימה בהתאם לתאריכים של המשימה ולתאריך של היום
    public BO.Status getStatus(
     DateTime estimatedStartDate, //תאריך משוער להתחלה
     DateTime actualStartDate, // תאריך התחלה בפועל
     DateTime finalDateForCompletion //תאריך סופי לסיום
        )
    {
        try
        {
            // Your implementation of getStatus method
            //לא מתוכנן
            if (estimatedStartDate == null)
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while determining status: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    //public BO.Status getStatus(
    // DateTime estimatedStartDate, //תאריך משוער להתחלה
    // DateTime actualStartDate, // תאריך התחלה בפועל
    // DateTime finalDateForCompletion //תאריך סופי לסיום
    //    )
    //{
    //    //לא מתוכנן
    //    if (estimatedStartDate ==null)
    //        return BO.Status.Unscheduled;
    //    //מתוזמן   
    //    else if (estimatedStartDate != null)
    //        return BO.Status.Scheduled;
    //    //בוצע
    //    if (finalDateForCompletion != null)
    //        return BO.Status.Done;
    //    //במעקב
    //    if (finalDateForCompletion > DateTime.Now &&
    //        actualStartDate < DateTime.Now)
    //    return BO.Status.OnTrack;
    //   return BO.Status.Unscheduled;
    //}

    public void AddTask(BO.Task task)
    {
        try
        {
            // Your implementation of AddTask method
            if (task.TaskNumber >= 0 && task.Nickname != "")
            {
                DO.Task doTask = new DO.Task(
                    task.TaskNumber,
                    task.Description,
                    task.Nickname,
                    task.ProductionDate,
                    task.ActualStartDate,
                    task.EstimatedStartDate,
                    task.RequiredEffortTime,
                    task.EstimatedCompletionDate,
                    null,
                    task.Product,
                    task.Notes,
                    null,
                    (DO.Levels)(int)Enum.Parse(typeof(DO.Levels), task.DifficultyLevel.ToString())
                    );
                int numTask = _dal.Task.Create(doTask);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while adding task: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }
    //public void AddTask(BO.Task task)
    //{
    //    try
    //    {
    //        if (task.TaskNumber >=0 && task.Nickname != "")
    //        {
    //            DO.Task doTask = new DO.Task(
    //                task.TaskNumber,
    //                task.Description,
    //                task.Nickname,
    //                task.ProductionDate,
    //                task.ActualStartDate,
    //                task.EstimatedStartDate,
    //                task.RequiredEffortTime,
    //                task.EstimatedCompletionDate,
    //                null,
    //                task.Product,
    //                task.Notes,
    //                null,
    //                (DO.Levels)(int)Enum.Parse(typeof(DO.Levels),task.DifficultyLevel.ToString())    
    //                );
    //                int numTask = _dal.Task.Create(doTask);
    //        }
    //        else
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }
    //    catch (DO.DalAlreayExistException)
    //    {

    //    }
    //}

    public DateTime GetEstimatedCompletionDate(DateTime EstimatedStartDate, DateTime ActualStartDate, int RequiredEffortTime)
    {
        try
        {
            TimeSpan timeToAdd = TimeSpan.FromHours(RequiredEffortTime);
            if (EstimatedStartDate > ActualStartDate)
                return EstimatedStartDate.Add(timeToAdd);
            else
                return ActualStartDate.Add(timeToAdd);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while calculating estimated completion date: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }
    //public DateTime GetEstimatedCompletionDate(DateTime EstimatedStartDate,
    //   DateTime ActualStartDate, int RequiredEffortTime)
    //{
    //    TimeSpan timeToAdd = TimeSpan.FromHours(RequiredEffortTime); // אם RequiredEffortTime מייצג שעות
    //    if (EstimatedStartDate > ActualStartDate)
    //        return EstimatedStartDate.Add(timeToAdd);
    //    else
    //        return ActualStartDate.Add(timeToAdd);

    //}

    public IEnumerable<BO.Task> GetAllTasks()
    {
        try
        {
            return _dal.Task.ReadAll().Select(doTask => new BO.Task
            {
                TaskNumber = doTask.TaskNumber,
                Description = doTask.Description,
                Nickname = doTask.Nickname,
                Status = getStatus((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (DateTime)doTask.FinalDateForCompletion),
                DependenciesList = getDependenciesList(doTask.TaskNumber),
                ProductionDate = (DateTime)doTask.CreatedAtDate,
                EstimatedStartDate = (DateTime)doTask.EstimatedStartDate,
                ActualStartDate = (DateTime)doTask.StartDate,
                EstimatedCompletionDate = GetEstimatedCompletionDate((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (int)doTask.RequiredEffortTime),
                ActualEndDate = doTask.ActualEndDate == null ? null : (DateTime)doTask.ActualEndDate,
                RequiredEffortTime = (int)doTask.RequiredEffortTime,
                Product = doTask.Product,
                Notes = doTask.Notes,
                eng = engineerImplementation.GetEngineerInTask(doTask?.EngineerId ?? 0),
                DifficultyLevel = (BO.Levels)Enum.Parse(typeof(BO.Levels), doTask.DifficultyLevel.ToString())
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while retrieving all tasks: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }
    //public IEnumerable<BO.Task> GetAllTasks()
    //{
    //    return (from DO.Task doTask in _dal.Task.ReadAll()

    //            select new BO.Task
    //            {
    //                TaskNumber = doTask.TaskNumber,
    //                Description = doTask.Description,
    //                Nickname = doTask.Nickname,
    //                Status = getStatus((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (DateTime)doTask.FinalDateForCompletion),
    //                DependenciesList=getDependenciesList(doTask.TaskNumber),
    //                ProductionDate = (DateTime)doTask.CreatedAtDate,
    //                EstimatedStartDate = (DateTime)doTask.EstimatedStartDate,
    //                ActualStartDate = (DateTime)doTask.StartDate,
    //                EstimatedCompletionDate = GetEstimatedCompletionDate((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (int)doTask.RequiredEffortTime),
    //                ActualEndDate = (DateTime)doTask.ActualEndDate,
    //                RequiredEffortTime=(int)doTask.RequiredEffortTime,
    //                Product=doTask.Product,
    //                Notes = doTask.Notes,
    //                eng = engineerImplementation.GetEngineerInTask(doTask.EngineerId ?? 0),
    //                DifficultyLevel =(BO.Levels)Enum.Parse(typeof(BO.Levels),doTask.DifficultyLevel.ToString())
    //            }) ;
    //}

    public BO.Task GetTaskDetails(int taskNumber)
    {
        try
        {
            DO.Task? doTask = _dal.Task.Read(t => t.TaskNumber == taskNumber);
            if (doTask == null)
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
                EstimatedStartDate = (DateTime)doTask.EstimatedStartDate,
                ActualStartDate = (DateTime)doTask.StartDate,
                EstimatedCompletionDate = GetEstimatedCompletionDate((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (int)doTask.RequiredEffortTime),
                ActualEndDate = (DateTime)doTask.ActualEndDate,
                RequiredEffortTime = (int)doTask.RequiredEffortTime,
                Product = doTask.Product,
                Notes = doTask.Notes,
                eng = engineerImplementation.GetEngineerInTask(doTask.EngineerId ?? 0),
                DifficultyLevel = (BO.Levels)Enum.Parse(typeof(BO.Levels), doTask.DifficultyLevel.ToString())
            };
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw;
        }
    }

    //public BO.Task GetTaskDetails(int taskNumber)
    //{
    //    DO.Task? doTask=_dal.Task.Read(t=>t.TaskNumber == taskNumber);
    //    if(doTask == null)
    //    {
    //        throw new Exception("Task not found");
    //    }
    //    return new BO.Task()
    //    {
    //        TaskNumber = doTask.TaskNumber,
    //        Description = doTask.Description,
    //        Nickname = doTask.Nickname,
    //        Status = getStatus((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (DateTime)doTask.FinalDateForCompletion),
    //        DependenciesList = getDependenciesList(doTask.TaskNumber),
    //        ProductionDate = (DateTime)doTask.CreatedAtDate,
    //        EstimatedStartDate = (DateTime)doTask.EstimatedStartDate,
    //        ActualStartDate = (DateTime)doTask.StartDate,
    //        EstimatedCompletionDate = GetEstimatedCompletionDate((DateTime)doTask.EstimatedStartDate, (DateTime)doTask.StartDate, (int)doTask.RequiredEffortTime),
    //        ActualEndDate = (DateTime)doTask.ActualEndDate,
    //        RequiredEffortTime = (int)doTask.RequiredEffortTime,
    //        Product = doTask.Product,
    //        Notes = doTask.Notes,
    //        eng = engineerImplementation.GetEngineerInTask(doTask.EngineerId ?? 0),
    //        DifficultyLevel = (BO.Levels)Enum.Parse(typeof(BO.Levels), doTask.DifficultyLevel.ToString())
    //    };
    //}

    public void RemoveTask(int taskNumber)
    {
        try
        {
            if (_dal.Task.ReadAll().Any(t => t?.TaskNumber == taskNumber) == false)
                throw new Exception("Task not exist");
            if (_dal.Dependence.ReadAll().Any(d => d?.NuberPreviousTask == taskNumber))
                throw new Exception("Dependency exists for this task");
            _dal.Task.Delete(taskNumber);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw;
        }
    }

    //public void RemoveTask(int taskNumber)
    //{
    //    try
    //    {
    //        if (_dal.Task.ReadAll().Any(t => t?.TaskNumber == taskNumber) == false)
    //            throw new Exception("Task not exist");
    //        if (_dal.Dependence.ReadAll().Any(d => d?.NuberPreviousTask == taskNumber))
    //            throw new Exception("DEpence");
    //        _dal.Task.Delete(taskNumber);

    //    }
    //    catch (Exception ex) { }
    //}

    public void UpdateTask(BO.Task task)
    {
        try
        {
            if (task.TaskNumber > 0 && task.Nickname != "")
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
                    task.ActualEndDate,
                    task.Product,
                    task.Notes,
                    task.eng == null ? null : task.eng.IdEngineer,
                    (DO.Levels)(int)Enum.Parse(typeof(DO.Levels), task.DifficultyLevel.ToString())
                );
                _dal.Task.Update(doTask);
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw;
        }
    }



    //public void UpdateTask(BO.Task task)
    //{
    //    try 
    //    {
    //        if (task.TaskNumber > 0 &&task.Nickname != "")
    //        {
    //            DO.Task doTask = new DO.Task(
    //              task.TaskNumber,
    //                task.Description,
    //                task.Nickname,
    //                //true,
    //                task.ProductionDate,
    //                task.ActualStartDate,
    //                task.EstimatedStartDate,
    //                task.RequiredEffortTime,
    //                task.EstimatedCompletionDate,
    //                task.ActualEndDate,
    //                task.Product,
    //                task.Notes,
    //                task.eng == null? null: task.eng.IdEngineer,
    //                (DO.Levels)(int)Enum.Parse(typeof(DO.Levels), task.DifficultyLevel.ToString())
    //              ) ;
    //            _dal.Task.Update(doTask);
    //        }
               
    //    }
    //    catch (Exception ex) {  }
    //}
}
