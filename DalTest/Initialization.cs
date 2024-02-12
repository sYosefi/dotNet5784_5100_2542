namespace DalTest;
using DalApi;
using DO;
using System;


public static class Initialization
{

    private static readonly Random s_rand = new();
    private static IDal? s_dal;

    public static void Do()//stage 4
    {
        s_dal = DalApi.Factory.Get; //stage 4
        createTasks();
        createEngineers();
        createDependences();
    }

    private static void createTasks()
    {
        Task t1 = new(1, "ניתוח סיכונים בפרויקט", "Risk Analysis", DateTime.Now, DateTime.Now, DateTime.Now, 30, DateTime.Now, DateTime.Now, null, null, null, Levels.Proficient);
        Task t2 = new(2, "אופטימיזיציה של המערכת", "BM#", DateTime.Now, DateTime.Now, DateTime.Now, 30, DateTime.Now, DateTime.Now, null, null, null, Levels.AdvancedBeginner);
        Task t3 = new(3, "ביקורת קוד", "AA", DateTime.Now, DateTime.Now, DateTime.Now, 30, DateTime.Now, DateTime.Now, null, null, null, Levels.Expert);
        Task t4 = new(4, "מחקר ויישום טכנולוגיות חדשות", "Research", DateTime.Now, DateTime.Now, DateTime.Now, 30, DateTime.Now, DateTime.Now, null, null, null, Levels.Novice);
        Task t5 = new(5, "הטמעת אמצעי אבטחה", "Implementation-SM", DateTime.Now, DateTime.Now, DateTime.Now, 30, DateTime.Now, DateTime.Now, null, null, null, Levels.Competent);
        Task t6 = new(6, "הכנת דרישות לא פונקציונאליות", "T8", DateTime.Now, DateTime.Now, DateTime.Now, 30, DateTime.Now, DateTime.Now, null, null, null, Levels.Competent);
        s_dal!.Task.Create(t1);
        s_dal!.Task.Create(t2);
        s_dal!.Task.Create(t3);
        s_dal!.Task.Create(t4);
        s_dal!.Task.Create(t5);
        s_dal!.Task.Create(t6);
    }

    private static void createEngineers()
    {

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
        Dependence d1 = new(0, 122, 123);
        Dependence d2 = new(0,121, 122);
        Dependence d3 = new(0, 124, 123);
        Dependence d4 = new(0, 123, 122);
        Dependence d5 = new(0, 122, 125);
        s_dal!.Dependence.Create(d1);
        s_dal!.Dependence.Create(d2);
        s_dal!.Dependence.Create(d3);
        s_dal!.Dependence.Create(d4);
        s_dal!.Dependence.Create(d5);
    }
}
