namespace DO;

public record Dependence
(
  
    int? IdDependence,
    int NumberDependenceTask,
    int NuberPreviousTask
 )
{
    public Dependence() : this(null, 0, 0) { }
    public Dependence(int? IdDependence, Dependence d) : this(IdDependence, d.NumberDependenceTask, d.NuberPreviousTask)
    {
    this.IdDependence = IdDependence;
    this.NumberDependenceTask = d.NumberDependenceTask;
    this.NuberPreviousTask = d.NuberPreviousTask;

    }
}




