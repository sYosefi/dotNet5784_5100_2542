namespace Dal;
using DalApi;
using DO;


public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newNum = DataSource.Config.NextTaskNumber;
        Task newTask=item with { TaskNumber = newNum };
        DataSource.Tasks.Add(newTask);
        return newNum;
    }

    public void Delete(int id)
    {
        Task task = DataSource.Tasks.FirstOrDefault(t => t.TaskNumber == id);
        if (task==null)
            throw new Exception($" Task with ID={id} is not exist ");
        
        else
        {
            DataSource.Tasks.Remove(task);
        }
    }

    public Task? Read(int id)
    {
        Task task = DataSource.Tasks.FirstOrDefault(t => t.TaskNumber == id)!;
        return task;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Task t = DataSource.Tasks.FirstOrDefault(t => t.TaskNumber == item.TaskNumber);
        if ( t== null)
            throw new Exception($" Task with ID={item.TaskNumber} is not exist ");
        else
        {
            DataSource.Tasks.Remove(t);
            DataSource.Tasks.Add(item);
        }
            
    }
}
