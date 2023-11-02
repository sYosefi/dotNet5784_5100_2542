namespace Dal;
using DalApi;
using DO;


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
        Engineer eng = DataSource.Engineers.FirstOrDefault(e => e.IdEngineer == id)!;
        if (eng == null)
            throw new Exception($" Engineer with ID={id} is not exist ");

        else
        {
            DataSource.Engineers. Remove(eng);
        }
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.FirstOrDefault(e => e.IdEngineer == id);
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer eng = DataSource.Engineers.FirstOrDefault(e => e.IdEngineer == item.IdEngineer)!;
        if (eng == null)
            throw new Exception($" Enginerr with ID={item.IdEngineer} is not exist ");
        else
        {
            DataSource.Engineers.Remove(eng);
            DataSource.Engineers.Add(eng);
        }
    }
}
