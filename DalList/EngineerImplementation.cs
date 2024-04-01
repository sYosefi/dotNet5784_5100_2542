
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{

    /// <summary>
    /// The function accepts a variable of engineer type and checks if its id already exists in the system,
    /// if so - throw an exception, otherwise - add it to the list of engineers
    /// </summary>
    /// <param name="item">An object of type Engineer</param>
    /// <returns>id Engineer</returns>
    /// <exception cref="Exception"></exception>
    public int Create(Engineer item)
    {
       
        if (DataSource.Engineers.FirstOrDefault(e=>e.IdEngineer==item.IdEngineer)!=null)
            throw new DalAlreayExistException($" Engineer with ID={item.IdEngineer} already exist ");
        else
        {
            DataSource.Engineers.Add(item);
        }
        return int.Parse(item.IdEngineer.Value.ToString());
    }


    /// <summary>
    /// The function receives an id and checks whether there is an engineer in the system with the received id, 
    /// if so - it deletes the engineer found, if not - it will throw an exception.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>

    public void Delete(int id)
    {
        Engineer eng = DataSource.Engineers.FirstOrDefault(e => e.IdEngineer == id)!;
        if (eng == null)
            throw new DalDoesNotExistException($" Engineer with ID={id} is not exist ");

        else
        {
            DataSource.Engineers. Remove(eng);
        }
    }


    public Engineer? Read(Func<Engineer, bool>? filter)//stage 2
    {
        return DataSource.Engineers.FirstOrDefault(filter!);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)//stage 2
    {
        if(filter!=null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;
    }

    public void Reset()
    {
        DataSource.Engineers.Clear();
    }

    public void Update(Engineer item)
    {
        Engineer eng = DataSource.Engineers.FirstOrDefault(e => e.IdEngineer == item.IdEngineer)!;
        if (eng == null)
            throw new DalDoesNotExistException($" Enginerr with ID={item.IdEngineer} is not exist ");
        else
        {
            DataSource.Engineers.Remove(eng);
            DataSource.Engineers.Add(eng);
        }
    }
}
