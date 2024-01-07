using BlApi;
using BO;

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
            //return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
            //        select new BO.Engineer
            //        {
            //            IdEngineer = doEngineer.IdEngineer ?? 0,
            //            Name = doEngineer.NameEngineer,
            //            Email = doEngineer.MailEnginerr,
            //            EngineerLevel = (BO.Experience)(int)Enum.Parse(typeof(BO.Experience), doEngineer.EngineerRank.ToString()),
            //            SalaryPerHour = doEngineer.PricePerHour ?? 0
            //            // CurrentTask = null
            //        });

            return (from DO.Task doTask in _dal.Task.ReadAll()
                    select new BO.Task
                    {
                        //TaskNumber=doTask.TaskNumber,
                        //Description=doTask.Description,
                        //Nickname=doTask.Nickname,
                        //ProductionDate=(DateTime)doTask.ProductionDate,
                        //Status=


                        //                public int TaskNumber { get; init; }
                        //public string Description { get; set; }
                        //public string Nickname { get; set; }
                        //public DateTime ProductionDate { get; set; }
                        //public Status Status { get; set; }
                        //public List<Task> DependenciesList { get; set; }
                        //public Milestone RelatedMileStone { get; set; }
                        //public DateTime EstimatedStartDate { get; set; }
                        //public DateTime ActualStartDate { get; set; }
                        //public DateTime EstimatedCompletionDate { get; set; }
                        //public DateTime FinalDateForCompletion { get; set; }
                        //public DateTime ActualEndDate { get; set; }
                        //public string Product { get; set; }
                        //public string Notes { get; set; }
                        //public Engineer? eng { get; set; }
                        //public Levels DifficultyLevel { get; set; }


                    });
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
