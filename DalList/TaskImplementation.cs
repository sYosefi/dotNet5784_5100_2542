namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class TaskImplementation : ITask
{
    /// <summary>
    ///The function get a task and add it to the list of tasks.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(Task item)
    {
        int newNum = DataSource.Config.NextTaskNumber;
        Task newTask=item with { TaskNumber = newNum };
        DataSource.Tasks.Add(newTask);
        return newNum;
    }

    /// <summary>
    /// The function gets id and delete the task from the list according to the id 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        Task task = DataSource.Tasks.FirstOrDefault(t => t.TaskNumber == id)!;
        if (task==null)
            throw new DalDoesNotExistException($" Task with ID={id} is not exist ");
        
        else
        {
            DataSource.Tasks.Remove(task);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task? Read(Func<Task, bool>? filter)//stage 2
    {
        return DataSource.Tasks.FirstOrDefault(filter!);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
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

    /// <summary>
    /// The function gets Task and updated the task in the data source
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Task item)
    {
        Task t = DataSource.Tasks.FirstOrDefault(t => t.TaskNumber == item.TaskNumber)!;
        if ( t== null)
            throw new DalDoesNotExistException($" Task with ID={item.TaskNumber} is not exist ");
        else
        {
            DataSource.Tasks.Remove(t);
            DataSource.Tasks.Add(item);
        }   
    }
}
