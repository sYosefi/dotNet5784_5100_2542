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
        Task t1 = new(1, "ניתוח סיכונים בפרויקט", "Risk Analysis", false, DateTime.Now, null, null, null, null, null, null, null, Levels.Proficient);
        Task t2 = new(2, "אופטימיזיציה של המערכת", "BM#", false, DateTime.Now, null, null, null, null, null, null, null, Levels.AdvancedBeginner);
        Task t3 = new(3, "ביקורת קוד", "AA", false, DateTime.Now, null, null, null, null, null, null, null, Levels.Expert);
        Task t4 = new(4, "מחקר ויישום טכנולוגיות חדשות", "Research", false, DateTime.Now, null, null, null, null, null, null, null, Levels.Novice);
        Task t5 = new(5, "הטמעת אמצעי אבטחה", "Implementation-SM", false, DateTime.Now, null, null, null, null, null, null, null, Levels.Competent);
        Task t6 = new(6, "הכנת דרישות לא פונקציונאליות", "T8", false, DateTime.Now, null, null, null, null, null, null, null, Levels.Competent);
        t_dalTask.Create(t1);
        t_dalTask.Create(t1);
        t_dalTask.Create(t2);
        t_dalTask.Create(t3);
        t_dalTask.Create(t4);  
        t_dalTask.Create(t5);
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
    {
        Dependence d1 = new(null, 1, 2);
        Dependence d2 = new(null, 2, 3);
        Dependence d3 = new(null, 1, 6);
        Dependence d4 = new(null, 5, 4);
        Dependence d5 = new(null, 5, 3);
        d_dalDependence.Create(d1);
        d_dalDependence.Create(d2);
        d_dalDependence.Create(d3); 
        d_dalDependence.Create(d4);
        d_dalDependence.Create(d5);
    }
}
