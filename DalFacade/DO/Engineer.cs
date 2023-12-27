

namespace DO;

public record Engineer
    ( 
    int? IdEngineer,
    string NameEngineer,
    string MailEnginerr,
    Experience? EngineerRank,
    int? PricePerHour
)
{
    //public Dependence() : this(null, 0, 0) { }
    //public Dependence(int? IdDependence, Dependence d) : this(IdDependence, d.NumberDependenceTask, d.NuberPreviousTask)
    //{
    //    this.IdDependence = IdDependence;
    //    this.NumberDependenceTask = d.NumberDependenceTask;
    //    this.NuberPreviousTask = d.NuberPreviousTask;
    //}
    public Engineer() : this(null, "", "", null, null)
    {
    }
    //public Engineer(int? IdEngineer, string NameEngineer, string MailEnginerr, Experience? EngineerRank, int? PricePerHour) :this()

    //{
    //    this.IdEngineer = IdEngineer;
    //    this.NameEngineer = NameEngineer;
    //    this.MailEnginerr = MailEnginerr;
    //    this.EngineerRank = EngineerRank;
    //    this.PricePerHour = PricePerHour;
    //}


}

