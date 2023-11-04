namespace DO;

public record Task
 (
  int? TaskNumber,
  string Description,
  string Nickname,
  bool Milestone,
  DateTime? ProductionDat=null,
  DateTime? StartDate = null,
  DateTime? EstimatedCompletionDate = null  ,
  DateTime? FinalDateForCompletion = null,
  DateTime? ActualEndDate = null,
  string? product=null,
  string? Notes = null,
  int? EngineerId = null,
  Levels? DifficultyLevel = null
  );




