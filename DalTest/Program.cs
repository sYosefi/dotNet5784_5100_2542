using Dal;
using DalApi;
using DalList;
using DO;
using System.Transactions;

namespace DalTest
{
    internal class Program
    {
        private static IEngineer? e_dalIEngineer = new EngineerImplementation(); //Stage 1
        private static ITask? t_dalITask = new TaskImplementation(); //Stage 1
        private static IDependence? d_dalIDependence = new DependenceImplementation(); //Stage 1

        /// <summary>
        /// The function picks up what data is being used and creates a new task
        /// </summary>


        public static Task TaskDetails()
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
            DO.Task t = new(null, desc, nick, false, DateTime.Now, start, estimatedCompletion, finalDate, null, product, note, null, null);
            return t;
        }
        private static void CreateTask()
        {
            Task taskToCreate = TaskDetails();
            Console.WriteLine(t_dalITask!.Create(taskToCreate));

        }

        private static void ReadTask()
        {
            Console.WriteLine("Enter task number");
            int taskNumber=int.Parse(Console.ReadLine());
            t_dalITask!.Read(taskNumber);
        }
        private static void UpdateTask()
        {
            Console.WriteLine("Enter task number");
            int taskNumber = int.Parse(Console.ReadLine());
            Console.WriteLine(t_dalITask!.Read(taskNumber));
            Task tempTask = TaskDetails();
            t_dalITask.Update(tempTask);
        }
        private static void DeleteTask()
        {
            Console.WriteLine("Enter task number");
            int taskNumber = int.Parse(Console.ReadLine());
            t_dalITask!.Delete(taskNumber);

        }
        private static Engineer EnginerrDetails()
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
            Engineer eng = new(null, name, mail, exp, salary);
            return eng;
        }
        private static void CreateEngineer()
        {
            int id;
            Console.WriteLine("Enter ID engineer");
            id = int.Parse(Console.ReadLine());
            Engineer tempEng = EnginerrDetails();
            Engineer newEng = new(id, tempEng.NameEngineer, tempEng.MailEnginerr, tempEng.EngineerRank, tempEng.PricePerHour);
            Console.WriteLine(e_dalIEngineer!.Create(newEng));
        }

        private static void ReadEngineer()
        {
            Console.WriteLine("Enter engineer ID");
            int id = int.Parse(Console.ReadLine());
            e_dalIEngineer!.Read(id);
        }
        private static void UpdateEngineer()
        {
            Console.WriteLine("Enter Engineer id to update");
            int idEngineer = int.Parse(Console.ReadLine());
            Engineer eng = e_dalIEngineer!.Read(idEngineer);
            Console.WriteLine(eng);
            Engineer updatedEng = EnginerrDetails();
            updatedEng.IdEngineer = idEngineer;
            e_dalIEngineer!.Update(updatedEng);
        }

        private static void DeleteEngineer()
        {
            Console.WriteLine("Enter Engineer id: ");
            int idEngineer = int.Parse(Console.ReadLine());
            e_dalIEngineer!.Delete(idEngineer);

        }
        private static Dependence DependenceDetails()
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
            Dependence depTmp=DependenceDetails();
            Dependence newDep = (depTmp.IdDependence, depTmp.NumberDependenceTask, depTmp.NuberPreviousTask);
            d_dalIDependence!.Create(newDep);
        }


        private static void ReadDependence()
        {
            Console.WriteLine("Enter Id Dependence ");
            int id = int.Parse(Console.ReadLine());
            d_dalIDependence!.Read(id);
        }
        private static void DeleteDependence()
        {
            Console.WriteLine("Enter Id Dependence ");
            int IdDepend = int.Parse(Console.ReadLine());
            d_dalIDependence!.Delete(IdDepend);
        }
        private static void UpdateDependence()
        {
            Console.WriteLine("Enter Id Dependence ");
            int IdDepend = int.Parse(Console.ReadLine());
            d_dalIDependence!.Delete(IdDepend);
            Dependence depTmp = DependenceDetails();
            //Dependence newDep = (IdDepend, depTmp.NumberDependenceTask, depTmp.NuberPreviousTask);
            Dependence newDep = new Dependence
            {
                IdDependence = IdDepend,
                NumberDependenceTask = depTmp.NumberDependenceTask,
                NuberPreviousTask = depTmp.NuberPreviousTask
            };



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
                Initialization.Do(t_dalITask, e_dalIEngineer, d_dalIDependence);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
       } 
        
    }
}
 


