using BlApi;
using BO;

namespace BlImplementation
{
    internal class TaskImplementation : ITask
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public void AddTask(BO.Task task)
        {
    //        try
    //        {
    //            if(task.TaskNumber>0 && 
    //                task.Nickname != ""
    //                //&& !string.IsNullOrEmpty(task.Nickname)
    //                )
    //            {
    //                DO.Task doTask = new DO.Task(
    //                    task.TaskNumber,
    //                    task.Description,
    //                    task.Nickname,
    //                    task.ProductionDate,
    //                    task.Status,
    //                    task.DependenciesList,
    //                    task.RelatedMileStone,
    //                    task.EstimatedStartDate,
    //                    task.ActualStartDate,
    //                    task.EstimatedCompletionDate, 
    //                    task.FinalDateForCompletion,
    //                    task.ActualEndDate,
    //                    task.Product,
    //                    task.Notes,
    //                    task.eng,
    //                    task.DifficultyLevel
    //                    );
    //                int numTask = _dal.Task.Create(doTask);

        
    
    //    //public DateTime ActualEndDate { get; set; }
    //    //public string Product { get; set; }
    //    //public string Notes { get; set; }
    //    //public Engineer? eng { get; set; }
    //    //public Levels DifficultyLevel { get; set; }


    //}
    //            else
    //            {
    //                throw new NotImplementedException();
    //            }
    //        }
    //        catch(DO.DalAlreayExistException)
    //        {
    //        }
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
