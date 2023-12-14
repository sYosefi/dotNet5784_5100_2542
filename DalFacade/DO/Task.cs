namespace DO;

public record Task
 (
  int? TaskNumber,
  string Description,
  string Nickname,
  bool Milestone,
  DateTime? ProductionDate=null,
  DateTime? StartDate = null,
  DateTime? EstimatedCompletionDate = null  ,
  DateTime? FinalDateForCompletion = null,
  DateTime? ActualEndDate = null,
  string? Product=null,
  string? Notes = null,
  int? EngineerId = null,
  Levels? DifficultyLevel = null
  )

{
  public Task(int? taskNumber) : this(taskNumber, "", "", false){}
}





