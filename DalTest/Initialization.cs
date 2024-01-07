namespace DalTest;
using DalApi;
using DO;
using System;


public static class Initialization
{
    //private static ITask? t_dalTask;
    //private static IEngineer? e_dalEngineer;
    //private static IDependence? d_dalDependence;
    private static readonly Random s_rand = new();
    private static IDal? s_dal;

    //public static void Do(IDal dal) //stage 2
    public static void Do()//stage 4
    {
        //e_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //t_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //d_dalDependence = dalDependence ?? throw new NullReferenceException("DAL can not be null!");
        //s_dal=dal?? throw new NullReferenceException("DAL object can not be null!");//Stage 2
        s_dal = DalApi.Factory.Get; //stage 4
        createTasks();
        createEngineers();
        createDependences();
    }

    private static void createTasks()
    {
        Task t1 = new(1, "ניתוח סיכונים בפרויקט", "Risk Analysis", false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null, null, null, Levels.Proficient);
        Task t2 = new(2, "אופטימיזיציה של המערכת", "BM#", false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null, null, null, Levels.AdvancedBeginner);
        Task t3 = new(3, "ביקורת קוד", "AA", false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null, null, null, Levels.Expert);
        Task t4 = new(4, "מחקר ויישום טכנולוגיות חדשות", "Research", false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null, null, null, Levels.Novice);
        Task t5 = new(5, "הטמעת אמצעי אבטחה", "Implementation-SM", false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null, null, null, Levels.Competent);
        Task t6 = new(6, "הכנת דרישות לא פונקציונאליות", "T8", false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null, null, null, Levels.Competent);
        s_dal!.Task.Create(t1);
        s_dal!.Task.Create(t2);
        s_dal!.Task.Create(t3);
        s_dal!.Task.Create(t4);
        s_dal!.Task.Create(t5);
        s_dal!.Task.Create(t6);
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
      
        Engineer e2 = new(218796347, "Aviran Asor", "avirn2187@gmai.com", Experience.Novice, 30);
        
        Engineer e3 = new(219975145, "Carmela Avner", "Carmela@gmai.com", Experience.AdvancedBeginner, 32);
       
        Engineer e4 = new(354871125, "Alon Barnea", "aBarnea@gmai.com", Experience.Expert, 55);

        Engineer e5 = new(375942451, "Ravit Bohan", "RB451@gmail.com", Experience.Proficient, 40);
        s_dal!.Engineer.Create(e1);
        s_dal!.Engineer.Create(e2);
        s_dal!.Engineer.Create(e3);
        s_dal!.Engineer.Create(e4);
        s_dal!.Engineer.Create(e5);

    }

    private static void createDependences()
    {
        Dependence d1 = new(0, 1, 2);
        Dependence d2 = new(0, 2, 3);
        Dependence d3 = new(0, 1, 6);
        Dependence d4 = new(0, 5, 4);
        Dependence d5 = new(0, 5, 3);
        s_dal!.Dependence.Create(d1);
        s_dal!.Dependence.Create(d2);
        s_dal!.Dependence.Create(d3);
        s_dal!.Dependence.Create(d4);
        s_dal!.Dependence.Create(d5);
    }
}
