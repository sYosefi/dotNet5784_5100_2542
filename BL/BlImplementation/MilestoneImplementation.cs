using BlApi;

namespace BlImplementation
{
    internal class MilestoneImplementation : IMilestone
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public void CreateProjectSchedule()
        {
            var taskDependencies=
                _dal.Dependence.ReadAll()//list of all pairs of tasks
                .OrderBy(dep=>dep.NumberDependenceTask)//all dependends on are ordered
                .GroupBy(dep=>dep.NumberDependenceTask,//create IEnumerable of groups
                //where the key is the dependent task and the group is the dependson
                dep=>dep.depOnTask,
               ( key,deps)=>new {taskNumber=key,Dependencies=deps}).ToList();
            throw new NotImplementedException();
        }

        public BO.Milestone GetMilestoneDetails(int milestoneNum)
        {
            try
            {
                _dal.Dependence.Read(d => d.IdDependence == milestoneNum);

            }
            catch { }
           
        }

        public BO.Milestone UpdateMilestone(int milestoneNum)
        {
            throw new NotImplementedException();
        }
    }
}
