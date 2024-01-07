
namespace Dal;
using DalApi;
using DO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{

    public int Create(DO.Task item)
    {
        XElement root = XMLTools.LoadListFromXMLElement("tasks");
        int nextId = Config.NextTaskId;
        XElement newTask = new XElement("tasks",
            new XElement("TaskNumber", nextId),
            new XElement("Description", item.Description),
            new XElement("Nickname", item.Nickname),
            new XElement("Milestone", item.Milestone),
            new XElement("ProductionDate", item.ProductionDate),
            new XElement("StartDate", item.StartDate),
            new XElement("EstimatedCompletionDate", item.EstimatedCompletionDate),
            new XElement("FinalDateForCompletion", item.FinalDateForCompletion),
            new XElement("ActualEndDate", item.ActualEndDate),
            new XElement("Product", item.Product),
            new XElement("Notes", item.Notes),
            new XElement("EngineerId", item.EngineerId),
            new XElement("DifficultyLevel", item.DifficultyLevel));
        root.Add(newTask);
        XMLTools.SaveListToXMLElement(root, "tasks");
        return nextId;

    }
    private static Task CreateTaskFromElement(XElement taskElem)
    {
        //Task newTask = new Task(
        //    taskElem.Element("TaskNumber") != null ? (int)int.Parse(taskElem.Element("TaskNumber").Value) : 0,
        //    taskElem.Element("Description")?.Value,
        //    taskElem.Element("Nickname")?.Value,
        //    (bool)taskElem.Element("Milestone"),
        //    taskElem.Element("ProductionDate") != null ? (DateTime?)DateTime.Parse(taskElem.Element("ProductionDate").Value) : DateTime.Now,
        //     taskElem.Element("StartDate") != null ? (DateTime?)DateTime.Parse(taskElem.Element("StartDate").Value) : DateTime.Now,
        //     taskElem.Element("EstimatedCompletionDate") != null ? (DateTime?)DateTime.Parse(taskElem.Element("EstimatedCompletionDate").Value) : DateTime.Now,
        //    taskElem.Element("FinalDateForCompletion") != null ? (DateTime?)DateTime.Parse(taskElem.Element("FinalDateForCompletion").Value) : DateTime.Now,
        //    taskElem.Element("ActualEndDate") != null ? (DateTime?)DateTime.Parse(taskElem.Element("ActualEndDate").Value) : DateTime.Now,
        //    taskElem.Element("Product")?.Value,
        //    taskElem.Element("Notes")?.Value,
        //    taskElem.Element("EngineerId") != null ? (int?)int.Parse(taskElem.Element("EngineerId").Value) : null,
        //    taskElem.Element("DifficultyLevel") != null ? (Levels)Enum.Parse(typeof(Levels), taskElem.Element("DifficultyLevel").Value) : null

        //);

        //return newTask;
        return new Task()
        {
            TaskNumber = int.Parse(taskElem.Element("TaskNumber").Value),
            Description = taskElem.Element("Description").Value,
            Nickname = taskElem.Element("Nickname").Value,
            Milestone = bool.Parse(taskElem.Element("Milestone").Value),
            ProductionDate = taskElem.ToDateTimeNullable("ProductionDate"),
            StartDate = taskElem.ToDateTimeNullable("StartDate"),
            EstimatedCompletionDate = taskElem.ToDateTimeNullable("EstimatedCompletionDate"),
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
        XElement root = XMLTools.LoadListFromXMLElement("tasks");
        XElement taskToDel = root.Elements("tasks").FirstOrDefault(t => (int)t.Element("TaskNumber") == id);
        if (taskToDel == null)
        {
            throw new DalDoesNotExistException($" Task with ID={id} is not exist ");

        }
        taskToDel.Remove();
        XMLTools.SaveListToXMLElement(root, "tasks");
    }

    public DO.Task? Read(Func<DO.Task, bool>? filter)
    {
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

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        XElement root = XMLTools.LoadListFromXMLElement("tasks");
        IEnumerable<XElement> allTasks = root.Elements("tasks");
        if (filter != null)
        {
            allTasks = allTasks.Where(t => filter(CreateTaskFromElement(t)));
        }
        return allTasks.Select(t => CreateTaskFromElement(t));
    }

    private static XElement ConvertTaskToElement(DO.Task task, int? taskNum) 
    {
        return new XElement("Task",
            new XElement("TaskNumber", taskNum),
            new XElement("Description",task.Description),
            new XElement("Nickname",task.Nickname),
            new XElement("Milestone",task.Milestone),
            new XElement("ProductionDate",task.ProductionDate),
            new XElement("StartDate",task.StartDate),
            new XElement("EstimatedCompletionDate",task.EstimatedCompletionDate),
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
        XElement root = XMLTools.LoadListFromXMLElement("tasks");

       XElement taskToUpdate = root.Elements("tasks").FirstOrDefault(t => (int)t.Element("TaskNumber") == item.TaskNumber);
        if(taskToUpdate != null) 
        {
            taskToUpdate.ReplaceWith(ConvertTaskToElement(item,item.TaskNumber));
            XMLTools.SaveListToXMLElement(root,"tasks");
        }
        else
        {
            throw new DalDoesNotExistException($" Task with ID={item.TaskNumber} is not exist ");
        }
    //    if (taskToUpdate != null)
    //    {
    //        int taskNum=int.Parse(taskToUpdate.Element("TaskNumber").Value);
    //        taskToUpdate.Remove();
    //        taskToUpdate.Element("TaskNumber").SetValue(taskNum);
    //        taskToUpdate.Element("Description").SetValue(item.Description);
    //        taskToUpdate.Element("Nickname").SetValue(item.Nickname);
    //        taskToUpdate.Element("Milestone").SetValue(item.Milestone);
    //        taskToUpdate.Element("ProductionDate").SetValue(item.ProductionDate);
    //        taskToUpdate.Element("StartDate").SetValue(item.StartDate);
    //        taskToUpdate.Element("EstimatedCompletionDate").SetValue(item.EstimatedCompletionDate);
    //        taskToUpdate.Element("FinalDateForCompletion").SetValue(item.FinalDateForCompletion);
    //        taskToUpdate.Element("ActualEndDate").SetValue(item.ActualEndDate);
    //        taskToUpdate.Element("Product").SetValue(item.Product);
    //        taskToUpdate.Element("Notes").SetValue(item.Notes);
    //        taskToUpdate.Element("EngineerId").SetValue(item.EngineerId);
    //        taskToUpdate.Element("DifficultyLevel").SetValue(item.DifficultyLevel);
    //        XMLTools.SaveListToXMLElement(taskToUpdate, "tasks");

    //    }
    //    else
    //    {
    //        throw new DalDoesNotExistException($" Task with ID={item.TaskNumber} is not exist ");
    //    }
    //
    }
}
