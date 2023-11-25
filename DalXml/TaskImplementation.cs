
namespace Dal;
using DalApi;
using DO;
internal class TaskImplementation : ITask
{
    public int Create(DO.Task item)
    {
        List<Task> allTasks = XMLTools.LoadListFromXMLElement<Task>("task");
        int nextId = Config.NextTaskId;
        Task newTask = item with { TaskNumber = nextId };
        allTasks.Add(newTask);
        XMLTools.SaveListToXMLElement(allTasks, "tasks");
        return nextId;
    }

    public void Delete(int id)
    {
        List<Task> allTask = XMLTools.LoadListFromXMLElement<Task>("tasks");
        Task task = allTask.FirstOrDefault(t => t.TaskNumber == id)!;
        if (task == null)
            throw new DalDoesNotExistException($" Task with ID={id} is not exist ");

        else
        {
            allTask.Remove(task);
            XMLTools.SaveListToXMLElement(allTask, "tasks");
        }
    }

    public DO.Task? Read(Func<DO.Task, bool>? filter)
    {
        List<Task> allTask = XMLTools.LoadListFromXMLElement<Task>("tasks");
        return allTask.FirstOrDefault(filter!);
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<Task> allTask = XMLTools.LoadListFromXMLElement<Task>("tasks");
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
        List<Task> allTask = XMLTools.LoadListFromXMLElement<Task>("tasks");

        Task t = allTask.FirstOrDefault(t => t.TaskNumber == item.TaskNumber)!;
        if (t == null)
            throw new DalDoesNotExistException($" Task with ID={item.TaskNumber} is not exist ");
        else
        {
            allTask.Remove(t);
            allTask.Add(item);
            XMLTools.SaveListToXMLElement(allTask, "tasks");
        }
    }
}
