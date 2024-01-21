
namespace BO;

public  class Task
{
    public int TaskNumber { get; init; }
    public string Description { get; set; }
    public string Nickname { get; set; }
    public DateTime ProductionDate { get; set; }
    public  BO.Status Status { get; set; }
    public List<TaskOnList> DependenciesList { get; set; }
    public MilestoneInTask RelatedMileStone { get; set; }
    public DateTime EstimatedStartDate { get; set; }
    public DateTime ActualStartDate { get; set; }
    public DateTime EstimatedCompletionDate { get; set; }
    public DateTime FinalDateForCompletion { get; set; }
    public DateTime ActualEndDate { get; set; }
    public string Product { get; set; }
    public string Notes { get; set; }
    public Engineer? eng { get; set; }
    public Levels DifficultyLevel { get; set; }

}
