namespace DO;

public record Task
 (
  int TaskNumber,
  string Description,
  string Nickname,
  bool Milestone,
  DateTime ProductionDate,
  DateTime StartDate,
  DateTime EstimatedCompletionDate,
  DateTime FinalDateForCompletion,
  DateTime ActualEndDate,
  string product,
  string Notes,
  int EngineerId,
  Levels DifficultyLevel
  );




