
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
        return item.TaskNumber;

    }
    public Task CreateTaskFromElement(XElement taskElem)
    {
        Task newTask = new Task(
            taskElem.Element("TaskNumber") != null ? (int)int.Parse(taskElem.Element("TaskNumber").Value) : 0,
            taskElem.Element("Description")?.Value,
            taskElem.Element("Nickname")?.Value,
            (bool)taskElem.Element("Milestone"),
            taskElem.Element("ProductionDate") != null ? (DateTime?)DateTime.Parse(taskElem.Element("ProductionDate").Value) : null,
            taskElem.Element("StartDate") != null ? (DateTime?)DateTime.Parse(taskElem.Element("StartDate").Value) : null,
            taskElem.Element("EstimatedCompletionDate") != null ? (DateTime?)DateTime.Parse(taskElem.Element("EstimatedCompletionDate").Value) : null,
            taskElem.Element("FinalDateForCompletion") != null ? (DateTime?)DateTime.Parse(taskElem.Element("FinalDateForCompletion").Value) : null,
            taskElem.Element("ActualEndDate") != null ? (DateTime?)DateTime.Parse(taskElem.Element("ActualEndDate").Value) : null,
            taskElem.Element("Product")?.Value,
            taskElem.Element("Notes")?.Value,
            taskElem.Element("EngineerId") != null ? (int?)int.Parse(taskElem.Element("EngineerId").Value) : null,
            taskElem.Element("DifficultyLevel") != null ? (Levels)Enum.Parse(typeof(Levels), taskElem.Element("DifficultyLevel").Value) : null
        );

        return newTask;
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

    public void Update(DO.Task item)
    {
        XElement root = XMLTools.LoadListFromXMLElement("tasks");
        XElement taskToUpdate = root.Elements("tasks").FirstOrDefault(t => (int)t.Element("TaskNumber") == item.TaskNumber);
        if (taskToUpdate != null)
        {
            taskToUpdate.Element("Description").SetValue(item.Description);
            taskToUpdate.Element("Nickname").SetValue(item.Nickname);
            taskToUpdate.Element("Milestone").SetValue(item.Milestone);
            taskToUpdate.Element("ProductionDate").SetValue(item.ProductionDate);
            taskToUpdate.Element("StartDate").SetValue(item.StartDate);
            taskToUpdate.Element("EstimatedCompletionDate").SetValue(item.EstimatedCompletionDate);
            taskToUpdate.Element("FinalDateForCompletion").SetValue(item.FinalDateForCompletion);
            taskToUpdate.Element("ActualEndDate").SetValue(item.ActualEndDate);
            taskToUpdate.Element("Product").SetValue(item.Product);
            taskToUpdate.Element("Notes").SetValue(item.Notes);
            taskToUpdate.Element("EngineerId").SetValue(item.EngineerId);
            taskToUpdate.Element("DifficultyLevel").SetValue(item.DifficultyLevel);

        }

        else
        {
            throw new DalDoesNotExistException($" Task with ID={item.TaskNumber} is not exist ");
        }
    }
}
