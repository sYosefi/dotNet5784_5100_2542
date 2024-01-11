using BlApi;
using DO;

namespace BlImplementation
{
    internal class TaskImplementation : ITask
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
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
                        true,
                        task.ProductionDate,
                        task.ActualStartDate,
                        task.EstimatedCompletionDate,
                        task.FinalDateForCompletion,
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

        public IEnumerable<BO.Task> GetAllTasks()
        {
            return (from DO.Task doTask in _dal.Task.ReadAll()

                    select new BO.Task
                    {
                        TaskNumber = doTask.TaskNumber,
                        Description = doTask.Description,
                        Nickname = doTask.Nickname,
                        ProductionDate = (DateTime)doTask.ProductionDate,
                        //Status=(Status)Enum.Parse(typeof(Status),doTask.)
                        //DependenciesList=
                        //RelatedMileStone =
                        ActualStartDate = (DateTime)doTask.StartDate,
                        EstimatedCompletionDate = (DateTime)doTask.EstimatedCompletionDate,
                        FinalDateForCompletion = (DateTime)doTask.FinalDateForCompletion,
                        ActualEndDate = (DateTime)doTask.ActualEndDate,
                        Notes = doTask.Notes,
                        eng =EngineerImplementation.GetEngineerDetails(doTask.EngineerId),
                        DifficultyLevel =(BO.Levels)Enum.Parse(typeof(BO.Levels),doTask.DifficultyLevel.ToString())
                    }) ;
        }

        public BO.Task GetTaskDetails(int taskNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveTask(int taskNumber)
        {
            //try
            //{
            //    if (!EngineerInTaskList.Any(e => e.IdEngineer == idEng))
            //    {
            //        _dal.Engineer.Delete(idEng);
            //    }
            //    else
            //    {
            //        throw new Exception();
            //    }

            //}
            //catch (DO.DalAlreayExistException)
            //{
            //    //זריקת חריגה של מהנדס קיים מה-BO 
            //    // throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
            //}
            try
            {
                
            }
            catch (Exception ex) { }
        }

        public void UpdateTask(BO.Task task)
        {
            //try
            //{
            //    if (eng.IdEngineer >= 0 && eng.Name != "" && eng.SalaryPerHour > 0 && eng.Email?.Contains("@") == true)
            //    {
            //        DO.Engineer doEng = new DO.Engineer(
            //            eng.IdEngineer,
            //            eng.Name,
            //            eng.Email,
            //            // Convert BO.Experience to DO.Experience?
            //            (DO.Experience?)eng.EngineerLevel,
            //            eng.SalaryPerHour
            //            );
            //        _dal.Engineer.Update(doEng);
            //    }
            //    else
            //    {
            //        throw new Exception();
            //    }

            //}
            //catch (DO.DalAlreayExistException)
            //{
            //    //זריקת חריגה של מהנדס קיים מה-BO 
            //    // throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
            //}
            try 
            {
                if (task.TaskNumber > 0 &&task.Nickname != "")
                {
                    DO.Task doTask = new DO.Task(
                       task.TaskNumber,
                       task.Description,
                       task.Nickname,
                       //task.RelatedMileStone,
                       false,
                       task.ProductionDate,
                       task.ActualStartDate,
                       task.EstimatedCompletionDate,
                       task.FinalDateForCompletion,
                       task.ActualEndDate,
                       task.Product,
                       task.Notes,
                       task.eng.IdEngineer,
                       task.DifficultyLevel
                       );
                    _dal.Task.Update(doTask);
                }
                   
            }
        }
    }
}
