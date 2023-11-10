
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {
        int newNum = DataSource.Config.NextIdDependence;
        Dependence newDep = item with { IdDependence = newNum };
        DataSource.Dependences.Add(newDep);
        return newNum;
    }

    public void Delete(int id)
    {
        Dependence dependence = DataSource.Dependences.FirstOrDefault(d => d.IdDependence == id)!; ;
        if (dependence == null)
            throw new Exception($" Dependence with ID={id} is not exist ");

        else
        {
            DataSource.Dependences.Remove(dependence);
        }
    }

    //public Dependence? Read(int id)//satge 1
    //{
    //    return DataSource.Dependences.FirstOrDefault(d => d.IdDependence == id)!; 
    //}

    public Dependence? Read(Func<Dependence, bool>? filter)//stage 2
    {
        return DataSource.Dependences.FirstOrDefault(filter!);
    }

    //public List<Dependence> ReadAll()
    //{
    //    return new List<Dependence>(DataSource.Dependences);
    //}

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Dependences
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependences
               select item;
    }

    public void Update(Dependence item)
    {
        Dependence dependence = DataSource.Dependences.FirstOrDefault(d => d.IdDependence == item.IdDependence)!;
        if (dependence== null)
            throw new Exception($" Dependence with ID={item.IdDependence} is not exist ");
        else
        {
            DataSource.Dependences.Remove(dependence);
            DataSource.Dependences.Add(item);
        }
    }
}
