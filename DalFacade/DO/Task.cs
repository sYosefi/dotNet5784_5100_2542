namespace DO;

public record Task
 (
  int TaskNumber,
  string Description,
  string Nickname,
  //bool Milestone,
  //TimeSpan RequiredEffortTime,
  DateTime CreatedAtDate,
  DateTime? StartDate=null,
  DateTime? EstimatedStartDate = null,
  int? RequiredEffortTime = null,
  DateTime? FinalDateForCompletion = null,
  DateTime? ActualEndDate = null,
  string? Product=null,
  string? Notes = null,
  int? EngineerId = null,
  Levels? DifficultyLevel = null
  )

{
    public Task():this(0,"","",DateTime.Now)
    { }

}





