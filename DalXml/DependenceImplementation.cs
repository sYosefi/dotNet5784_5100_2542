
namespace Dal;
using DalApi;
using DO;
using System;

internal class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {
        int nextId = Config.NextDependenceId;
        List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        Dependence newDep = item with { IdDependence = nextId };
        allDep.Add(newDep);
        XMLTools.SaveListToXMLSerializer<Dependence>(allDep, "dependences");
        return nextId;
    }

    public void Delete(int id)
    {
        List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        Dependence dependence = allDep.FirstOrDefault(d => d.IdDependence == id)!; 
        if (dependence == null)
            throw new DalDoesNotExistException($" Dependence with ID={id} is not exist ");

        else
        {
            allDep.Remove(dependence);
        }
    }

    public Dependence? Read(Func<Dependence, bool>? filter)
    {
        List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        return allDep.FirstOrDefault(filter!);


    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        if (filter != null)
        {
            return from item in allDep
                   where filter(item)
                   select item;
        }
        return from item in allDep
               select item;
    }

    public void Update(Dependence item)
    {
        List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        Dependence dependence = allDep.FirstOrDefault(d => d.IdDependence == item.IdDependence)!;
        if (dependence == null)
            throw new DalDoesNotExistException($" Dependence with ID={item.IdDependence} is not exist ");
        else
        {
            allDep.Remove(dependence);
            allDep.Add(item);
        }
    }
}
