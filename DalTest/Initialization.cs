namespace DalTest;
using DalApi;
using DO;


public static class Initialization
{
    private static ITask? t_dalTask;
    private static IEngineer? e_dalEngineer;
    private static IDependence? d_dalDependence;
    private static readonly Random s_rand = new();

    public static void Do(ITask? dalTask, IEngineer? dalEngineer, IDependence? dalDependence)
    {
        e_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        t_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        d_dalDependence = dalDependence ?? throw new NullReferenceException("DAL can not be null!");
        createTasks();
        createEngineers();
        createDependences();
    }

    private static void createTasks()
    {
      
    }

    private static void createEngineers()
    {
        /* string[] engineerNames = { "Elad Arison", "Aviran Asor", "Alon Barnea", "Carmela Avner", "Ravit Bohan" };
         foreach(var engineerName in engineerNames) 
         {
             int id;
             do id = s_rand.Next(MIN_ID, MAX_ID);
             while (e_dalEngineer!.Read(id) != null);

             int index = s_rand.Next(Enum.GetValues(typeof(Experience)).Length);
             Experience experience = (Experience)Enum.GetValues(typeof(Experience)).GetValue(index);


         }*/
        Engineer e1 = new(227355056, "Elad Arison", "eladA@gmai.com", Experience.Expert, 50);
        e_dalEngineer!.Create(e1);
        Engineer e2 = new(218796347, "Aviran Asor", "avirn2187@gmai.com", Experience.Novice, 30);
        e_dalEngineer!.Create(e2);
        Engineer e3 = new(219975145, "Carmela Avner", "Carmela@gmai.com", Experience.AdvancedBeginner, 32);
        e_dalEngineer!.Create(e3);
        Engineer e4 = new(354871125, "Alon Barnea", "aBarnea@gmai.com", Experience.Expert, 55);
        e_dalEngineer!.Create(e4);
        Engineer e5 = new(375942451, "Ravit Bohan", "RB451@gmail.com", Experience.Proficient, 40);
        e_dalEngineer!.Create(e5);

    }

    private static void createDependences()
    { }
}
