namespace Dal;
using DalApi;
using DO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class TaskImplementation : ITask
{

    public int Create(DO.Task item)
    {
        try
        {
            List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
            Task copy = item with { TaskNumber = Config.NextTaskId };
            Tasks.Add(copy);
            XMLTools.SaveListToXMLSerializer(Tasks, "tasks");
            return copy.TaskNumber;
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during creation: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    


    private static Task CreateTaskFromElement(XElement taskElem)
    {
        return new Task()
        {
            TaskNumber = int.Parse(taskElem.Element("TaskNumber").Value),
            Description = taskElem.Element("Description").Value,
            Nickname = taskElem.Element("Nickname").Value,
            //Milestone = bool.Parse(taskElem.Element("Milestone").Value),
            CreatedAtDate = (DateTime)taskElem.ToDateTimeNullable("CreatedAtDate"),
            StartDate = taskElem.ToDateTimeNullable("StartDate"),
            EstimatedStartDate = taskElem.ToDateTimeNullable("EstimatedStartDate"),
            RequiredEffortTime = int.Parse(taskElem.Element("RequiredEffortTime")?.Value),
            FinalDateForCompletion = taskElem.ToDateTimeNullable("FinalDateForCompletion"),
            ActualEndDate = taskElem.ToDateTimeNullable("ActualEndDate"),
            Product = taskElem.Element("Product").Value,
            Notes = taskElem.Element("Notes").Value,
            EngineerId = taskElem.ToIntNullable("EngineerId"),
            DifficultyLevel = (Levels)Enum.Parse(typeof(Levels), taskElem.Element("DifficultyLevel").Value),
        };
    }

    public void Delete(int id)
    {
        try
        {
            XElement root = XMLTools.LoadListFromXMLElement("tasks");
            XElement taskToDel = root.Elements("tasks").FirstOrDefault(t => (int)t.Element("TaskNumber") == id);
            if (taskToDel == null)
            {
                throw new DalDoesNotExistException($" Task with ID={id} does not exist ");
            }
            taskToDel.Remove();
            XMLTools.SaveListToXMLElement(root, "tasks");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during deletion: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }
   

    public DO.Task? Read(Func<DO.Task, bool>? filter)
    {
        try
        {
            List<DO.Task> allTask = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
            return filter != null ? allTask.Where(filter).FirstOrDefault() : allTask.FirstOrDefault();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during reading: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

  

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        try
        {
            List<DO.Task> allTask = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
            return filter != null ? allTask.Where(filter) : allTask;
           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during reading all: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }




    private static XElement ConvertTaskToElement(DO.Task task, int? taskNum) 
    {
        return new XElement("Task",
            new XElement("TaskNumber", taskNum),
            new XElement("Description",task.Description),
            new XElement("Nickname",task.Nickname),
            //new XElement("Milestone",task.Milestone),
            new XElement("CreatedAtDate", task.CreatedAtDate),
            new XElement("StartDate",task.StartDate),
            new XElement("EstimatedStartDate", task.EstimatedStartDate),
            new XElement("RequiredEffortTime",task.RequiredEffortTime),
            new XElement("FinalDateForCompletion",task.FinalDateForCompletion),
            new XElement("ActualEndDate",task.ActualEndDate),
            new XElement("Product",task.Product),
            new XElement("Notes",task.Notes),
            new XElement("EngineerId",task.EngineerId),
            new XElement("DifficultyLevel",task.DifficultyLevel)
            );
    }

    public void Update(DO.Task item)
    {
        try
        {
            List<Task> allEng = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
            Task eng = allEng.FirstOrDefault(e => e.TaskNumber == item.TaskNumber)!;
            if (eng == null)
                throw new DalDoesNotExistException($" Engineer with ID={item.TaskNumber} does not exist");

            allEng.Remove(eng);
            allEng.Add(item);
            XMLTools.SaveListToXMLSerializer(allEng, "tasks");
            
       
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during update: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    
    public void Reset()
    {
        try
        {
            // Implementation of Reset method
            //יוצר רשימה ריקה ומכניס אותה במקום הרשימה הנוכחית
            List<Task> emptyTaskList = new List<Task>();
            XMLTools.SaveListToXMLSerializer<Task>(emptyTaskList, "tasks");
            XMLTools.ResetConfig("data-config", "TaskNumber");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during reset: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }
    
}
