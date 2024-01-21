
using BlApi;
using BO;
using DalApi;
using DO;
using System.ComponentModel.Design;

namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    private static void Exit()
    {
        Console.WriteLine("You have successfully exited the program");
    }
  
    public static BO.Task TaskDetails(int id)
    {
    BO.Levels level;
        string desc, nick, product, note;
        DateTime production, start, end, estimatedCompletion, finalDate, actualEndDate, actualStartDate;
        int engId, milstoneId;
        Status s;
        Console.WriteLine("Enter description of the task");
        desc = Console.ReadLine();
        Console.WriteLine("Enter a nickname for the task");
        nick = Console.ReadLine();
        Console.WriteLine("Enter production date");
        production =DateTime.Parse( Console.ReadLine());  
        Console.WriteLine("Enter status of the task");
        s = (BO.Status)Enum.Parse(typeof(BO.Status), Console.ReadLine());
        List<BO.TaskOnList> DependenciesList = inputDependencyList();
        Console.WriteLine("Enter number of related milstone");
        milstoneId= int.Parse(Console.ReadLine());
        BO.MilestoneInTask relatedMilestone = s_bl.Task.getRelatedMilestoneInTask(milstoneId);//getRelaedMilestone(milstoneId);
        Console.WriteLine("Enter estimated start date for the task");
        start = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter actual start date");
        actualStartDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter an estimated completion date date");
        estimatedCompletion = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter Final date for the task");
        finalDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter a actual end date of the task product");
        actualEndDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter a ptoduct of the task product");
        product = Console.ReadLine();
        Console.WriteLine("Enter notes on the task");
        note = Console.ReadLine();
        Console.WriteLine("Enter engineer id");
        engId = int.Parse(Console.ReadLine());
        BO.Engineer eng=s_bl.Engineer.GetEngineerDetails(engId);
        Console.WriteLine("Enter level of the task");
        level = (BO.Levels)Enum.Parse(typeof(BO.Levels), Console.ReadLine());
        //BO.Task t = new (id, desc, nick, production, s, DependenciesList, relatedMilestone, start, actualStartDate, estimatedCompletion,
        //    finalDate, actualEndDate, product, note, eng, level);
        return new BO.Task()
        {
            TaskNumber = id,
            Description = desc,
            Nickname = nick,
            ProductionDate = production,
            Status = s,
            DependenciesList = DependenciesList,
            RelatedMileStone = relatedMilestone,
            EstimatedStartDate = start,
            ActualStartDate = actualStartDate,
            EstimatedCompletionDate = estimatedCompletion,
            FinalDateForCompletion = finalDate,
            ActualEndDate = actualEndDate,
            Product = product,
            Notes = note,
            eng = eng,
            DifficultyLevel = level
        };

    }

  

    private static List<BO.TaskOnList> inputDependencyList()
    {
        int idDep;
        List<BO.TaskOnList> tasksOnList = new List<BO.TaskOnList>();
        Console.WriteLine("Enter a dependency number, press 0 to finish");
        idDep = int.Parse(Console.ReadLine());
        while (idDep != 0)
        {
            BO.TaskOnList currentTask= new BO.TaskOnList();
            currentTask = s_bl.Task.GetTaskOnListDetails(idDep);
            tasksOnList.Add(currentTask);
            idDep = int.Parse(Console.ReadLine());
        }
        return tasksOnList;

    }

    private static void CreateTask()
    {
        BO.Task taskToCreate = TaskDetails(0);
        s_bl.Task.AddTask(taskToCreate);
    }

    private static void ReadTask()
    {
        Console.WriteLine("Enter task number");
        int taskNumber = int.Parse(Console.ReadLine());
       s_bl!.Task.GetTaskDetails(taskNumber); 
    }
    private static void UpdateTask()
    {
        Console.WriteLine("Enter task number");
        int taskNumber = int.Parse(Console.ReadLine());
        //Console.WriteLine(s_bl!.Task.GetAllTasks());
        BO.Task tempTask = TaskDetails(taskNumber);
        s_bl!.Task.UpdateTask(tempTask);
    }
    private static void DeleteTask()
    {
        Console.WriteLine("Enter task number");
        int taskNumber = int.Parse(Console.ReadLine());
         s_bl.Task.RemoveTask(taskNumber);

    }
    private static Engineer EnginerrDetails(int id)
    {
        int salary;
        string name, mail, e;
        Console.WriteLine("Enter name engineer");
        name = Console.ReadLine();
        Console.WriteLine("Enter engineer mail");
        mail = Console.ReadLine();
        Console.WriteLine("Enter engineer experience");
        e = Console.ReadLine();
        Experience exp = (Experience)Enum.Parse(typeof(Experience), e, true);
        Console.WriteLine("Enter price per hour");
        salary = int.Parse(Console.ReadLine());
        Engineer eng = new(id, name, mail, exp, salary);
        return eng;
    }
    private static void CreateEngineer()
    {
        int id;
        Console.WriteLine("Enter ID engineer");
        id = int.Parse(Console.ReadLine());
        Engineer tempEng = EnginerrDetails(id);
        Engineer newEng = new(id, tempEng.NameEngineer, tempEng.MailEnginerr, tempEng.EngineerRank, tempEng.PricePerHour);
        Console.WriteLine(s_bl!.Engineer.Create(newEng));
    }

    private static void ReadEngineer()
    {
        Console.WriteLine("Enter engineer ID");
        int id = int.Parse(Console.ReadLine());
        //Func<DO.Engineer, bool> filter = (engineer) => engineer.IdEngineer == id;
        Console.WriteLine(s_bl!.Engineer.GetEngineerDetails(id));
    }
    private static void UpdateEngineer()
    {
        Console.WriteLine("Enter Engineer id to update");
        int idEngineer = int.Parse(Console.ReadLine());
        BO.Engineer eng = s_bl!.Engineer.GetEngineerDetails(idEngineer);
        Console.WriteLine(eng);
        BO.Engineer updatedEng = EnginerrDetails(idEngineer);
        s_bl!.Engineer.UpdateEngineerDetails(updatedEng);
    }

    private static void DeleteEngineer()
    {
        Console.WriteLine("Enter Engineer id: ");
        int idEngineer = int.Parse(Console.ReadLine());
        s_dal!.Engineer.Delete(idEngineer);

    }
    private static void ShowEngineer()
    {
        Console.WriteLine("Enter 0 to exist the main menu\nEnter 1 to create a new engineer \nEnter 2 to display a engineer\nEnter 3 to update a engineer \nEnter 4 to delete a engineer");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {

                case 1: { CreateEngineer(); break; };
                case 2: { ReadEngineer(); break; };
                case 3: { UpdateEngineer(); break; };
                case 4: { DeleteEngineer(); break; };
            }
            Console.WriteLine("Enter 0 to exist the main menu\nEnter 1 to create a new engineer \nEnter 2 to display a engineer\nEnter 3 to update a engineer \nEnter 4 to delete a engineer");
            choice = int.Parse(Console.ReadLine());
        }
    }

    private static void ShowTasks()
    {
        Console.WriteLine("Enter 0 to exist the main menu\nEnter 1 to create a new task \nEnter 2 to display a task\nEnter 3 to update a task \nEnter 4 to delete a task");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1: { CreateTask(); break; };
                case 2: { ReadTask(); break; };
                case 3: { UpdateTask(); break; };
                case 4: { DeleteTask(); break; };
            }
            Console.WriteLine("Enter 0 to exist the main menu\nEnter 1 to create a new task \nEnter 2 to display a task\nEnter 3 to update a task \nEnter 4 to delete a task");
            choice = int.Parse(Console.ReadLine());
        }

    }
    private static void Menu()
    {
        Console.WriteLine("Enter 0 to exist the main menu\nEnter 1 to Engineers\nEnter 2 to Tasks\nEnter 3 to Dependences");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 0: { Exit(); break; };
                case 1: { ShowEngineers(); break; };
                case 2: { ShowTasks(); break; };
                 //case 3: { showDependence(); break; };
                default: { Console.WriteLine("Incorrect input"); break; };
            };
            Console.WriteLine("Enter 0 to exist the main menu\nEnter 1 to Engineers\nEnter 2 to Tasks\nEnter 3 to Dependences");
            choice = int.Parse(Console.ReadLine());
        }
    }
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Would you like to create Initial data? (Y/N)"); 
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y") 
                DalTest.Initialization.Do(); //stage 4
            else
            {
                Menu();
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}

