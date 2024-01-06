using BlApi;

namespace BlImplementation
{
    internal class MilestoneImplementation : IMilestone
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public void CreateProjectSchedule()
        {
            throw new NotImplementedException();
        }

        public BO.Milestone GetMilestoneDetails(int milestoneNum)
        {
            throw new NotImplementedException();
        }

        public BO.Milestone UpdateMilestone(int milestoneNum)
        {
            throw new NotImplementedException();
        }
    }
}
