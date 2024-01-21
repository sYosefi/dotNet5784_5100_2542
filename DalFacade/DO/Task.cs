namespace DO;

public record Task
 (
  int TaskNumber,
  string Description,
  string Nickname,
  bool Milestone,
  //TimeSpan RequiredEffortTime,
  DateTime? ProductionDate,
  DateTime? StartDate,
  DateTime? EstimatedCompletionDate  ,
  DateTime? FinalDateForCompletion,
  DateTime? ActualEndDate,
  string? Product=null,
  string? Notes = null,
  int? EngineerId = null,
  Levels? DifficultyLevel = null
  )

{
    public Task():this(0,"","",false,DateTime.Now, DateTime.Now, DateTime.Now
        , DateTime.Now, DateTime.Now)
    { }
    //public Task(int taskNumber, Task t) : this(0, "", "", false)
    //{
    //    this.TaskNumber = taskNumber;
    //    this.Description = t.Description;
    //    this.Nickname = t.Nickname;
    //    this.Milestone = t.Milestone;
    //    this.ProductionDate = t.ProductionDate;
    //    this.StartDate = t.StartDate;
    //    this.EstimatedCompletionDate = t.EstimatedCompletionDate;
    //    this.FinalDateForCompletion = t.FinalDateForCompletion;
    //    this.ActualEndDate = t.ActualEndDate;
    //    this.Product = t.Product;
    //    this.Notes = t.Notes;
    //    this.EngineerId = t.EngineerId;
    //    this.DifficultyLevel = t.DifficultyLevel;

    //}
}





