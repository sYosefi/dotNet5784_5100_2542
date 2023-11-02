namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (DataSource.Engineers.FirstOrDefault(e=>e.IdEngineer==item.IdEngineer)!=null)
            throw new Exception($" Engineer with ID={item.IdEngineer} is not already exist ");
        else
        {
            DataSource.Engineers.Add(item);
        }
        return item.IdEngineer;
    }

    public void Delete(int id)
    {
        Engineer eng = DataSource.Engineers.FirstOrDefault(e => e.IdEngineer == id);
        if (eng == null)
            throw new Exception($" Engineer with ID={id} is not exist ");

        else
        {
            DataSource.Engineers. Remove(eng);
        }
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
