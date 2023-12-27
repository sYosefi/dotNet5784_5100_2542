
namespace Dal;
using DalApi;
using DO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
    //public int Create(DO.Task item)
    //{
    //    List<Task> allTasks = XMLTools.LoadListFromXMLElement<Task>("task");
    //    int nextId = Config.NextTaskId;
    //    Task newTask = item with { TaskNumber = nextId };
    //    allTasks.Add(newTask);
    //    XMLTools.SaveListToXMLElement(allTasks, "tasks");
    //    return nextId;
    //}

    public List<Task> makeTaskList()
    {
        XElement element = XMLTools.LoadListFromXMLElement("task");
        List<Task> allTasks = new List<Task>();
        if (element.HasElements)
           
        {
            allTasks = element.Elements("tasks")
                              .Select(e => new Task(
                                  TaskNumber: (int)e.Element("TaskNumber"),
                                  Description: e.Element("Description").ToString(),
                                  Nickname: e.Element("Nickname").ToString(),
                                  Milestone: (bool)e.Element("Milestone"),
                                  ProductionDate: DateTime.Parse(e.Element("ProductionDate").ToString()),
                                  StartDate: DateTime.Parse(e.Element("StartDate").ToString()),
                                  EstimatedCompletionDate: DateTime.Parse(e.Element("EstimatedCompletionDate").ToString()),
                                  FinalDateForCompletion: DateTime.Parse(e.Element("FinalDateForCompletion").ToString()),
                                  ActualEndDate: DateTime.Parse(e.Element("ActualEndDate").ToString()),
                                  Product: e.Element("Product").ToString(),
                                  Notes: e.Element("Notes").ToString(),
                                  EngineerId: (int)e.Element("EngineerId"),
                                  DifficultyLevel: (Levels?)Enum.Parse(typeof(Levels), e.Element("DifficultyLevel").Value)
                              // Map other properties similarly
                              ))
                              .ToList();
        }
        return allTasks;
    }

    public void saveList(List<Task> allTasks,string entity)
    {
        XElement rootElem = new XElement(entity,
        allTasks.Select(task => new XElement("task",
            new XElement("TaskNumber", task.TaskNumber),
            new XElement("Description", task.Description),
            new XElement("Nickname", task.Nickname),
            new XElement("Milestone", task.Milestone),
            new XElement("ProductionDate", task.ProductionDate),
            new XElement("StartDate", task.StartDate),
            new XElement("EstimatedCompletionDate", task.EstimatedCompletionDate),
            new XElement("FinalDateForCompletion", task.FinalDateForCompletion),
            new XElement("ActualEndDate", task.ActualEndDate),
            new XElement("Product", task.Product),
            new XElement("Notes", task.Notes),
            new XElement("EngineerId", task.EngineerId),
             new XElement("DifficultyLevel", task.DifficultyLevel)
        ))) ;

        try
        {
            rootElem.Save("task.xml");
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"Failed to save list");
        }
    }
    public int Create(DO.Task item)
    {
        //1. לקבל את הרשימה של כל המשימות
        //2. להוסיף 

        // Check if the XElement has elements and convert them to Task objects
        List<Task> allTasks = makeTaskList();
        int nextId = Config.NextTaskId;
        Task newTask = new Task(item.TaskNumber,item);
        allTasks.Add(newTask);

        // Save the updated list of tasks to the XML
        saveList(allTasks, "tasks");

        return nextId;
    }

    public void Delete(int id)
    {
        List<Task> allTask = makeTaskList();
        Task task = allTask.FirstOrDefault(t => t.TaskNumber == id)!;
        if (task == null)
            throw new DalDoesNotExistException($" Task with ID={id} is not exist ");

        else
        {
            allTask.Remove(task);
            saveList(allTask, "tasks");
        }
    }

    public DO.Task? Read(Func<DO.Task, bool>? filter)
    {
        List<Task> allTask = makeTaskList();
        return allTask.FirstOrDefault(filter!);
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<Task> allTask = makeTaskList();
        if (filter != null)
        {
            return from item in allTask
                   where filter(item)
                   select item;
        }
        return from item in allTask
               select item;
    }

    public void Update(DO.Task item)
    {
        List<Task> allTask = makeTaskList();

        Task t = allTask.FirstOrDefault(t => t.TaskNumber == item.TaskNumber)!;
        if (t == null)
            throw new DalDoesNotExistException($" Task with ID={item.TaskNumber} is not exist ");
        else
        {
            allTask.Remove(t);
            allTask.Add(item);
            saveList(allTask, "tasks");
        }
    }
}
