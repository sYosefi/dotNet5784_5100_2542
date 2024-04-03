
namespace Dal;
using DalApi;
using DO;
using System;

internal class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {
        try
        {
            int nextId = Config.NextDependenceId;
            List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
            Dependence newDep = item with { IdDependence = nextId };
            allDep.Add(newDep);
            XMLTools.SaveListToXMLSerializer<Dependence>(allDep, "dependences");
            return nextId;
        }
        catch (Exception ex)
        {
            // Handle or log the exception
            Console.WriteLine($"An error occurred during creation: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }
    //public int Create(Dependence item)
    //{
    //    int nextId = Config.NextDependenceId;
    //    List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
    //    Dependence newDep = item with { IdDependence = nextId };
    //    allDep.Add(newDep);
    //    XMLTools.SaveListToXMLSerializer<Dependence>(allDep, "dependences");
    //    return nextId;
    //}

    public void Delete(int id)
    {
        try
        {
            List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
            Dependence dependence = allDep.FirstOrDefault(d => d.IdDependence == id)!;
            if (dependence == null)
                throw new DalDoesNotExistException($" Dependence with ID={id} does not exist ");
            else
            {
                allDep.Remove(dependence);
                XMLTools.SaveListToXMLSerializer<Dependence>(allDep, "dependences");
            }
        }
        catch (Exception ex)
        {
            // Handle or log the exception
            Console.WriteLine($"An error occurred during deletion: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    //public void Delete(int id)
    //{
    //    List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
    //    Dependence dependence = allDep.FirstOrDefault(d => d.IdDependence == id)!; 
    //    if (dependence == null)
    //        throw new DalDoesNotExistException($" Dependence with ID={id} is not exist ");

    //    else
    //    {
    //        allDep.Remove(dependence);
    //        XMLTools.SaveListToXMLSerializer<Dependence>(allDep, "dependences");
    //    }

    //}

    public Dependence? Read(Func<Dependence, bool>? filter)
    {
        try
        {
            List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
            return allDep.FirstOrDefault(filter!);
        }
        catch (Exception ex)
        {
            // Handle or log the exception
            Console.WriteLine($"An error occurred during reading: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    //public Dependence? Read(Func<Dependence, bool>? filter)
    //{
    //    List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
    //    return allDep.FirstOrDefault(filter!);
    //}

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        try
        {
            List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
            if (filter != null)
            {
                return from item in allDep
                       where filter(item)
                       select item;
            }
            return allDep;
        }
        catch (Exception ex)
        {
            // Handle or log the exception
            Console.WriteLine($"An error occurred during reading all: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    //public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    //{
    //    List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
    //    if (filter != null)
    //    {
    //        return from item in allDep
    //               where filter(item)
    //               select item;
    //    }
    //    return from item in allDep
    //           select item;
    //}

    public void Reset()
    {
        try
        {
            List<Dependence> emptyDependenceList = new List<Dependence>();
            XMLTools.SaveListToXMLSerializer<Dependence>(emptyDependenceList, "dependences");
            XMLTools.ResetConfig("data-config", "NextDependenceId");
        }
        catch (Exception ex)
        {
            // Handle or log the exception
            Console.WriteLine($"An error occurred during reset: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }
    //public void Reset()
    //{
    //    //יוצר רשימה ריקה ומכניס אותה במקום הרשימה הנוכחית
    //    List<Dependence> emptyDependenceList = new List<Dependence>();
    //    XMLTools.SaveListToXMLSerializer<Dependence>(emptyDependenceList, "dependences");
    //    XMLTools.ResetConfig("data-config", "NextDependenceId");
    //}

    public void Update(Dependence item)
    {
        try
        {
            List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
            Dependence dependence = allDep.FirstOrDefault(d => d.IdDependence == item.IdDependence)!;
            if (dependence == null)
                throw new DalDoesNotExistException($" Dependence with ID={item.IdDependence} does not exist ");
            else
            {
                allDep.Remove(dependence);
                allDep.Add(item);
                XMLTools.SaveListToXMLSerializer<Dependence>(allDep, "dependences");
            }
        }
        catch (Exception ex)
        {
            // Handle or log the exception
            Console.WriteLine($"An error occurred during update: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    //public void Update(Dependence item)
    //{
    //    List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
    //    Dependence dependence = allDep.FirstOrDefault(d => d.IdDependence == item.IdDependence)!;
    //    if (dependence == null)
    //        throw new DalDoesNotExistException($" Dependence with ID={item.IdDependence} is not exist ");
    //    else
    //    {
    //        allDep.Remove(dependence);
    //        allDep.Add(item);
    //        XMLTools.SaveListToXMLSerializer<Dependence>(allDep, "dependences");
    //    }
    //}
}
