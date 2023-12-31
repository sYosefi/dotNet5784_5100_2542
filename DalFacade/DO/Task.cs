namespace DO;

public record Task
 (
  int TaskNumber,
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
    public Task(int taskNumber, Task t): this(0, "", "", false)
    {
        this.TaskNumber = taskNumber;
        this.Description = t.Description;
        this.Nickname = t.Nickname;
        this.Milestone = t.Milestone;
        this.ProductionDate = t.ProductionDate;
        this.StartDate = t.StartDate;
        this.EstimatedCompletionDate= t.EstimatedCompletionDate;
        this.FinalDateForCompletion= t.FinalDateForCompletion;
        this.ActualEndDate = t.ActualEndDate;
        this.Product = t.Product;
        this.Notes = t.Notes;
        this.EngineerId= t.EngineerId;
        this.DifficultyLevel = t.DifficultyLevel;

    }
}





