namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class TaskImplementation : ITask
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
        Task task = DataSource.Tasks.FirstOrDefault(t => t.TaskNumber == id)!;
        if (task==null)
            throw new Exception($" Task with ID={id} is not exist ");
        
        else
        {
            DataSource.Tasks.Remove(task);
        }
    }

    //public Task? Read(int id)//stage 1
    //{
    //    Task task = DataSource.Tasks.FirstOrDefault(t => t.TaskNumber == id)!;
    //    return task;
    //}

    public Task? Read(Func<Task, bool>? filter)//stage 2
    {
        return DataSource.Tasks.FirstOrDefault(filter!);
    }

    //public List<Task> ReadAll()
    //{
    //    return new List<Task>(DataSource.Tasks);
    //}

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Tasks
               select item;
    }

    public void Update(Task item)
    {
        Task t = DataSource.Tasks.FirstOrDefault(t => t.TaskNumber == item.TaskNumber)!;
        if ( t== null)
            throw new Exception($" Task with ID={item.TaskNumber} is not exist ");
        else
        {
            DataSource.Tasks.Remove(t);
            DataSource.Tasks.Add(item);
        }   
    }
}
