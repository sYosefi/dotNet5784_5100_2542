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
            XElement root = XMLTools.LoadListFromXMLElement("tasks");
            int nextId = Config.NextTaskId;
            XElement newTask = ConvertTaskToElement(item, nextId);
            //XElement newTask = new XElement("tasks",
            //new XElement("TaskNumber", nextId),
            //new XElement("Description", item.Description),
            //new XElement("Nickname", item.Nickname),
            ////new XElement("Milestone", item.Milestone),
            //new XElement("CreatedAtDate", item.CreatedAtDate),
            //new XElement("StartDate", item.StartDate),
            //new XElement("EstimatedStartDate", item.EstimatedStartDate),
            //new XElement("RequiredEffortTime", item.RequiredEffortTime),
            //new XElement("FinalDateForCompletion", item.FinalDateForCompletion),
            //new XElement("ActualEndDate", item.ActualEndDate),
            //new XElement("Product", item.Product),
            //new XElement("Notes", item.Notes),
            //new XElement("EngineerId", item.EngineerId),
            //new XElement("DifficultyLevel", item.DifficultyLevel));
            root.Add(newTask);
            XMLTools.SaveListToXMLElement(root, "tasks");
            return nextId;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during creation: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    //public int Create(DO.Task item)
    //{
    //    XElement root = XMLTools.LoadListFromXMLElement("tasks");
    //    int nextId = Config.NextTaskId;
    //    XElement newTask = new XElement("tasks",
    //        new XElement("TaskNumber", nextId),
    //        new XElement("Description", item.Description),
    //        new XElement("Nickname", item.Nickname),
    //        //new XElement("Milestone", item.Milestone),
    //        new XElement("CreatedAtDate", item.CreatedAtDate),
    //        new XElement("StartDate", item.StartDate),
    //        new XElement("EstimatedStartDate", item.EstimatedStartDate),
    //        new XElement("RequiredEffortTime", item.RequiredEffortTime),
    //        new XElement("FinalDateForCompletion", item.FinalDateForCompletion),
    //        new XElement("ActualEndDate", item.ActualEndDate),
    //        new XElement("Product", item.Product),
    //        new XElement("Notes", item.Notes),
    //        new XElement("EngineerId", item.EngineerId),
    //        new XElement("DifficultyLevel", item.DifficultyLevel));
    //    root.Add(newTask);
    //    XMLTools.SaveListToXMLElement(root, "tasks");
    //    return nextId;
    //}


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
    //public void Delete(int id)
    //{
    //    XElement root = XMLTools.LoadListFromXMLElement("tasks");
    //    XElement taskToDel = root.Elements("tasks").FirstOrDefault(t => (int)t.Element("TaskNumber") == id);
    //    if (taskToDel == null)
    //    {
    //        throw new DalDoesNotExistException($" Task with ID={id} is not exist ");

    //    }
    //    taskToDel.Remove();
    //    XMLTools.SaveListToXMLElement(root, "tasks");
    //}

    public DO.Task? Read(Func<DO.Task, bool>? filter)
    {
        try
        {
            // Implementation of Read method
            XElement root = XMLTools.LoadListFromXMLElement("tasks");

            // If a filter is provided, use it to filter the tasks
            IEnumerable<XElement> filteredTasks = filter != null
                ? root.Elements("tasks").Where(t => filter(CreateTaskFromElement(t)))
                : root.Elements("tasks");

            XElement taskElem = filteredTasks.FirstOrDefault();

            if (taskElem != null)
            {
                return CreateTaskFromElement(taskElem);
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during reading: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    //public DO.Task? Read(Func<DO.Task, bool>? filter)
    //{
    //    XElement root = XMLTools.LoadListFromXMLElement("tasks");

    //    // If a filter is provided, use it to filter the tasks
    //    IEnumerable<XElement> filteredTasks = filter != null
    //        ? root.Elements("tasks").Where(t => filter(CreateTaskFromElement(t)))
    //        : root.Elements("tasks");

    //    XElement taskElem = filteredTasks.FirstOrDefault();

    //    if (taskElem != null)
    //    {
    //        return CreateTaskFromElement(taskElem);
    //    }

    //    return null;
    //}

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        try
        {
            // Implementation of ReadAll method
            XElement root = XMLTools.LoadListFromXMLElement("tasks");
            IEnumerable<XElement> allTasks = root.Elements("tasks");
            if (filter != null)
            {
                allTasks = allTasks.Where(t => filter(CreateTaskFromElement(t)));
            }
            return allTasks.Select(t => CreateTaskFromElement(t));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during reading all: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }


    //public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    //{
    //    XElement root = XMLTools.LoadListFromXMLElement("tasks");
    //    IEnumerable<XElement> allTasks = root.Elements("tasks");
    //    if (filter != null)
    //    {
    //        allTasks = allTasks.Where(t => filter(CreateTaskFromElement(t)));
    //    }
    //    return allTasks.Select(t => CreateTaskFromElement(t));
    //}



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
            // Implementation of Update method
            XElement root = XMLTools.LoadListFromXMLElement("tasks");

            XElement taskToUpdate = root.Elements("tasks").FirstOrDefault(t => (int)t.Element("TaskNumber") == item.TaskNumber);
            if (taskToUpdate != null)
            {
                taskToUpdate.ReplaceWith(ConvertTaskToElement(item, item.TaskNumber));
                XMLTools.SaveListToXMLElement(root, "tasks");
            }
            else
            {
                throw new DalDoesNotExistException($" Task with ID={item.TaskNumber} is not exist ");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during update: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    //public void Update(DO.Task item)
    //{
    //    XElement root = XMLTools.LoadListFromXMLElement("tasks");

    //   XElement taskToUpdate = root.Elements("tasks").FirstOrDefault(t => (int)t.Element("TaskNumber") == item.TaskNumber);
    //    if(taskToUpdate != null) 
    //    {
    //        taskToUpdate.ReplaceWith(ConvertTaskToElement(item,item.TaskNumber));
    //        XMLTools.SaveListToXMLElement(root,"tasks");
    //    }
    //    else
    //    {
    //        throw new DalDoesNotExistException($" Task with ID={item.TaskNumber} is not exist ");
    //    }
    //}

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
    //public void Reset()
    //{
    //    //יוצר רשימה ריקה ומכניס אותה במקום הרשימה הנוכחית
    //    List<Task> emptyTaskList = new List<Task>();
    //    XMLTools.SaveListToXMLSerializer<Task>(emptyTaskList, "tasks");
    //    XMLTools.ResetConfig("data-config","TaskNumber");

    //}
}
