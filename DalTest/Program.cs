using Dal;
using DalApi;
using DO;




namespace DalTest;

internal class Program
{
    //private static IEngineer? e_dalIEngineer = new EngineerImplementation(); //Stage 1
    //private static ITask? t_dalITask = new TaskImplementation(); //Stage 1
    //private static IDependence? d_dalIDependence = new DependenceImplementation(); //Stage 1
     //static readonly IDal s_dal=new DalList();//stage 2
    static readonly IDal s_dal = new Dal.DalXml(); //stage 3


    /// <summary>
    /// The function picks up what data is being used and creates a new task
    /// </summary>


    public static DO.Task TaskDetails(int id)
    {
        string desc, nick, product, note;
        DateTime start, end, estimatedCompletion, finalDate;
        Console.WriteLine("Enter descibtion of the task");
        desc = Console.ReadLine();
        Console.WriteLine("Enter a nickName for the task");
        nick = Console.ReadLine();
        Console.WriteLine("Enter start date for the task");
        start = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter an estimated end date");
        estimatedCompletion = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter Final date for the task");
        finalDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter a description of the task product");
        product = Console.ReadLine();
        Console.WriteLine("Enter notes on the task");
        note = Console.ReadLine();
        DO.Task t = new(id, desc, nick, false, DateTime.Now, start, estimatedCompletion, finalDate, null, product, note, null, null);
        return t;
    }
    private static void CreateTask()
    {
        DO.Task taskToCreate = TaskDetails(0);
        Console.WriteLine(s_dal!.Task.Create(taskToCreate));

    }

    private static void ReadTask()
    {
        Console.WriteLine("Enter task number");
        int taskNumber=int.Parse(Console.ReadLine());
        s_dal!.Task.Read(t => t.TaskNumber == taskNumber); // Use a lambda expression to define the filter
    }
    private static void UpdateTask()
    {
        Console.WriteLine("Enter task number");
        int taskNumber = int.Parse(Console.ReadLine());
        Console.WriteLine(s_dal!.Task.Read(t => t.TaskNumber == taskNumber));
        DO.Task tempTask = TaskDetails(taskNumber);
        s_dal!.Task.Update(tempTask);
    }
    private static void DeleteTask()
    {
        Console.WriteLine("Enter task number");
        int taskNumber = int.Parse(Console.ReadLine());
        s_dal!.Task.Delete(taskNumber);

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
        Console.WriteLine(s_dal!.Engineer.Create(newEng));
    }

    private static void ReadEngineer()
    {
        Console.WriteLine("Enter engineer ID");
        int id = int.Parse(Console.ReadLine());
        Func<DO.Engineer, bool> filter = (engineer) => engineer.IdEngineer == id;
        s_dal!.Engineer.Read(filter);
    }
    private static void UpdateEngineer()
    {
        Console.WriteLine("Enter Engineer id to update");
        int idEngineer = int.Parse(Console.ReadLine());
        Engineer eng = s_dal!.Engineer.Read(e => e.IdEngineer == idEngineer); 
        Console.WriteLine(eng);
        Engineer updatedEng = EnginerrDetails(idEngineer);
        s_dal!.Engineer.Update(updatedEng);
    }

    private static void DeleteEngineer()
    {
        Console.WriteLine("Enter Engineer id: ");
        int idEngineer = int.Parse(Console.ReadLine());
        s_dal!.Engineer.Delete(idEngineer);

    }
    private static Dependence DependenceDetails(int id)
    {
        int NumberDependence, NuberPrevious;
        Console.WriteLine("Enter a number dependence task");
        NumberDependence = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter a number previous task");
        NuberPrevious = int.Parse(Console.ReadLine());
        Dependence dep = new(null, NumberDependence, NuberPrevious);
        return dep;
    }

    private static void CreateDependence()
    {
        Dependence depTmp = DependenceDetails(0);
        Dependence newDep = new (depTmp.IdDependence,depTmp.NumberDependenceTask, depTmp.NuberPreviousTask);
        s_dal!.Dependence!.Create(newDep); 

    }


    private static void ReadDependence()
    {
        Console.WriteLine("Enter Id Dependence ");
        int id = int.Parse(Console.ReadLine());
        Func<DO.Dependence, bool> filter = (dependence) => dependence.IdDependence == id;
        s_dal!.Dependence!.Read(filter);
    }
    private static void DeleteDependence()
    {
        Console.WriteLine("Enter Id Dependence ");
        int IdDepend = int.Parse(Console.ReadLine());
        s_dal!.Dependence!.Delete(IdDepend);
    }
    private static void UpdateDependence()
    {
        Console.WriteLine("Enter Id Dependence ");
        int idDepend = int.Parse(Console.ReadLine());
        Dependence depTmp = DependenceDetails(idDepend);
        s_dal!.Dependence!.Update(depTmp);
    }

    private static void ShowTask()
    {
        Console.WriteLine("Enter 0 to exist the main menu\n Enter 1 to create a new engineer\nEnter 2 to display a engineer\nEnter 3 to update a engineer \nEnter 4 to delete a engineer");
        int choice= int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1: { CreateTask(); break; };
                case 2: { ReadTask(); break; };
                case 3: { UpdateTask(); break; };
                case 4: { DeleteTask(); break; };
            }
            Console.WriteLine("Enter 0 to exist the main menu\n Enter 1 to create a new task \nEnter 2 to display a task\nEnter 3 to update a task \nEnter 4 to delete a task");
            choice = int.Parse(Console.ReadLine());
        }

    }
    private static void ShowEngineer()
    {
        Console.WriteLine("Enter 0 to exist the main menu\n Enter 1 to create a new task \nEnter 2 to display a task\nEnter 3 to update a task \nEnter 4 to delete a task");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
               //Miriam 20:49

                case 1: { CreateEngineer(); break; };
                case 2: { ReadEngineer(); break; };
                case 3: { UpdateEngineer(); break; };
                case 4: { DeleteEngineer(); break; };
            }
            Console.WriteLine("Enter 0 to exist the main menu\n Enter 1 to create a new task \nEnter 2 to display a task\nEnter 3 to update a task \nEnter 4 to delete a task");
            choice = int.Parse(Console.ReadLine());
        }
    }
    private static void showDependence()
    {
        Console.WriteLine("Enter 0 to exist the main menu\n Enter 1 to create a dependence \nEnter 2 to display a dependence\nEnter 3 to update a dependence \nEnter 4 to delete a dependence");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {

                case 1: { CreateDependence(); break; };
                case 2: { ReadDependence(); break; };
                case 3: { UpdateDependence(); break; };
                case 4: { DeleteDependence(); break; };
            }
            Console.WriteLine("Enter 0 to exist the main menu\n Enter 1 to create a dependence \nEnter 2 to display a dependence\nEnter 3 to update a dependence \nEnter 4 to delete a dependence");
            choice = int.Parse(Console.ReadLine());
        }
    }
    private static void Menu()
    {
        Console.WriteLine("Enter 0 to exist the main menu\n Enter 1 to Engineers\n Enter 2 to Tasks\n Enter 3 to Dependences");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1: { ShowTask(); break; };
                case 2: { ShowEngineer(); break; };
                case 3: { showDependence(); break; };
            }
            Console.WriteLine("Enter 0 to exist the main menu\n Enter 1 to Engineers\n Enter 2 to Tasks\n Enter 3 to Dependences");
            choice = int.Parse(Console.ReadLine());
        }
    }
    static void Main(string[] args)
   {
        try
        {
            Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
            if (ans == "Y") //stage 3
                Initialization.Do(s_dal);//stage 2 
        }            

        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
   } 
    
}

