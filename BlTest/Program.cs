
using BlApi;
using BO;
using DalApi;
using DO;
using System.ComponentModel.Design;
using System.Reflection.Emit;

namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    private static void Exit()
    {
        Console.WriteLine("You have successfully exited the program");
    }

    /// <summary>
    /// a function which gets id of task and return all the task details by creating new task in BO.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static BO.Task TaskDetails(int id)
    {
        BO.Levels level;
        string desc, nick, product, note;
        DateTime production, start, end, finalDate;
        int requiredEffortTime;
    
        Console.WriteLine("Enter description of the task");
        desc = Console.ReadLine();
        Console.WriteLine("Enter a nickname for the task");
        nick = Console.ReadLine();
        List<BO.TaskOnList> DependenciesList = inputDependencyList();
        Console.WriteLine("Enter estimated start date for the task");
        start = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter required effort time for the task in days");
        requiredEffortTime = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter a ptoduct of the task ");
        product = Console.ReadLine();
        Console.WriteLine("Enter notes on the task");
        note = Console.ReadLine();
        Console.WriteLine("Enter level of the task");
        level = (BO.Levels)Enum.Parse(typeof(BO.Levels), Console.ReadLine());

        return new BO.Task()
        {
            TaskNumber = id,
            Description = desc,
            Nickname = nick,
            Status = BO.Status.Unscheduled,
            DependenciesList = DependenciesList,
            ProductionDate = DateTime.Now,
            EstimatedStartDate = start,
            ActualStartDate = null,
            RequiredEffortTime =requiredEffortTime,
            EstimatedCompletionDate = s_bl.Task.GetEstimatedCompletionDate(start, DateTime.Now, requiredEffortTime),
            ActualEndDate = null,
            Product = product,
            Notes = note,
            eng = null,
            DifficultyLevel = level
        };
    }


    /// <summary>
    /// a function which gets id of task and gets all the propreties of the task that the user want to change.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static BO.Task UpdateDetails(int id)
    {

        BO.Task tempTask = s_bl!.Task.GetTaskDetails(id);
        BO.Levels level;
        string desc, nick, product, note;
        Status s;
        DateTime  start, end, finalDate;
        int requiredEffortTime;

        Console.WriteLine("Enter description of the task");
        desc = Console.ReadLine();
        Console.WriteLine("Enter a nickname for the task");
        nick = Console.ReadLine();
        List<BO.TaskOnList> DependenciesList = inputDependencyList();
        Console.WriteLine("Enter estimated start date for the task");
        start = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter required effort time for the task in days");
        requiredEffortTime = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter a ptoduct of the task ");
        product = Console.ReadLine();
        Console.WriteLine("Enter notes on the task");
        note = Console.ReadLine();
        Console.WriteLine("Enter level of the task");
        level = (BO.Levels)Enum.Parse(typeof(BO.Levels), Console.ReadLine());

        return new BO.Task()
        {
            TaskNumber = id,
            Description = desc,
            Nickname = nick,
            Status = BO.Status.Unscheduled,
            DependenciesList = DependenciesList,
            ProductionDate = tempTask.ProductionDate,
            EstimatedStartDate = start,
            ActualStartDate = tempTask.ActualStartDate,
            RequiredEffortTime = requiredEffortTime,
            EstimatedCompletionDate = s_bl.Task.GetEstimatedCompletionDate(start, DateTime.Now, requiredEffortTime),
            ActualEndDate = tempTask.ActualEndDate,
            Product = product,
            Notes = note,
            eng =tempTask.eng,
            DifficultyLevel = level
        };

    }

    /// <summary>
    /// helper function for the dependent tasks of the task that the user want to add.
    /// </summary>
    /// <returns></returns>
    private static List<BO.TaskOnList> inputDependencyList()
    {
        int idDep;
        List<BO.TaskOnList> tasksOnList = new List<BO.TaskOnList>();
        Console.WriteLine("Enter a dependency number, in the end press 0");
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


    /// <summary>
    /// a function that calls the function AddTask from the BO.
    /// </summary>
    private static void CreateTask()
    {
        BO.Task taskToCreate = TaskDetails(0);
        s_bl.Task.AddTask(taskToCreate);
    }

    /// <summary>
    /// a function that gets from the user number of task prints all of it details.
    /// </summary>
    private static void ReadTask()
    {
        Console.WriteLine("Enter task number");
        int taskNumber = int.Parse(Console.ReadLine());
        Console.WriteLine( s_bl!.Task.GetTaskDetails(taskNumber)?.ToString()); 
    }

    /// <summary>
    /// a function that gets number of task to update and send it to UpdateTask.
    /// </summary>
    private static void UpdateTask()
    {
        Console.WriteLine("Enter task number");
        int taskNumber = int.Parse(Console.ReadLine());
        BO.Task tempTask = UpdateDetails(taskNumber);
        s_bl!.Task.UpdateTask(tempTask);
    }

    /// <summary>
    /// a function that gets task number from the user and sends it to RemoveTask.
    /// </summary>
    private static void DeleteTask()
    {
        Console.WriteLine("Enter task number");
        int taskNumber = int.Parse(Console.ReadLine());
         s_bl.Task.RemoveTask(taskNumber);

    }

    /// <summary>
    /// a function that gets id and gets all the other details from the user.
    /// the function craetes new engineer in BO.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private static BO.Engineer EnginerrDetails(int id)
    {
        int salary;
        string name, mail, e;
        Console.WriteLine("Enter name engineer");
        name = Console.ReadLine();
        Console.WriteLine("Enter engineer mail");
        mail = Console.ReadLine();
        Console.WriteLine("Enter engineer experience");
        e = Console.ReadLine();
        BO.Experience exp = (BO.Experience)Enum.Parse(typeof(BO.Experience), e.ToString());
        Console.WriteLine("Enter price per hour");
        salary = int.Parse(Console.ReadLine());
        return new BO.Engineer()
        {
            IdEngineer = id,
            Name = name,
            Email = mail,
            EngineerLevel = exp,
            SalaryPerHour = salary,
            CurrentTask = null,
        };
    }


    /// <summary>
    /// a function that grts engineer id from the user and use other function for the rest of the details.
    /// the function sends the new task to the function AddTask.
    /// </summary>
    private static void CreateEngineer()
    {
        int id;
        Console.WriteLine("Enter ID engineer");
        id = int.Parse(Console.ReadLine());
        BO.Engineer tempEng = EnginerrDetails(id);
        s_bl!.Engineer.AddEngineer(tempEng);
        Console.WriteLine(tempEng);
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
        s_bl!.Engineer.RemoveEngineer(idEngineer);

    }

    private static Dependence DependenceDetails(int id)
    {
        int NumberDependence, NuberPrevious;
        Console.WriteLine("Enter a number dependence task");
        NumberDependence = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter a number previous task");
        NuberPrevious = int.Parse(Console.ReadLine());
        Dependence dep = new(id, NumberDependence, NuberPrevious);
        return dep;
    }

    private static void ShowEngineers()
    {
        Console.WriteLine("Enter 0 to exist the main menu\nEnter 1 to create a new engineer \nEnter 2 to display a engineer\nEnter 3 to update a engineer \nEnter 4 to delete a engineer");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 0: { Menu(); break; };
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
                case 0: { Menu(); break; };
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
        Console.WriteLine("Enter 0 to exist the main menu\nEnter 1 to Engineers\nEnter 2 to Tasks");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            try
            {
                switch (choice)
                {
                    case 0: { Exit(); break; };
                    case 1: { ShowEngineers(); break; };
                    case 2: { ShowTasks(); break; };
                    //case 3: { showMilestone(); break; };
                    default: { Console.WriteLine("Incorrect input"); break; };
                };
                Console.WriteLine("Enter 0 to exist the main menu\nEnter 1 to Engineers\nEnter 2 to Tasks");
                choice = int.Parse(Console.ReadLine());
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        
        }
    }
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Would you like to create Initial data? (Y/N)"); 
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
            {
                DalTest.Initialization.Do(); //stage 4
                Menu();
            }

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

