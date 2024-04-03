using Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        try
        {
            List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
            if (allEng.FirstOrDefault(e => e.IdEngineer == item.IdEngineer) != null)
                throw new DalAlreayExistException($" Engineer with ID={item.IdEngineer} already exists");

            allEng.Add(item);
            XMLTools.SaveListToXMLSerializer(allEng, "engineers");

            return int.Parse(item.IdEngineer.Value.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during creation: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    public void Delete(int id)
    {
        try
        {
            List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
            Engineer eng = allEng.FirstOrDefault(e => e.IdEngineer == id);
            if (eng == null)
                throw new DalDoesNotExistException($" Engineer with ID={id} does not exist");

            allEng.Remove(eng);
            XMLTools.SaveListToXMLSerializer(allEng, "engineers");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during deletion: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    public Engineer? Read(Func<Engineer, bool>? filter)
    {
        try
        {
            List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
            return allEng.FirstOrDefault(filter!);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during reading: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        try
        {
            List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
            return filter != null ? allEng.Where(filter) : allEng;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during reading all: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    public void Reset()
    {
        try
        {
            List<Engineer> emptyEngineerList = new List<Engineer>();
            XMLTools.SaveListToXMLSerializer<Engineer>(emptyEngineerList, "engineers");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during reset: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }

    public void Update(Engineer item)
    {
        try
        {
            List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
            Engineer eng = allEng.FirstOrDefault(e => e.IdEngineer == item.IdEngineer)!;
            if (eng == null)
                throw new DalDoesNotExistException($" Engineer with ID={item.IdEngineer} does not exist");

            allEng.Remove(eng);
            allEng.Add(item);
            XMLTools.SaveListToXMLSerializer(allEng, "engineers");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during update: {ex.Message}");
            throw; // Re-throw the exception to propagate it
        }
    }
}




//namespace Dal;
//using DalApi;
//using DO;
//using System;
//using System.Collections.Generic;

//internal class EngineerImplementation : IEngineer
//{
//    public int Create(Engineer item)
//    {

//        List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        if (allEng.FirstOrDefault(e => e.IdEngineer == item.IdEngineer) != null)
//           throw new DalAlreayExistException($" Engineer with ID={item.IdEngineer} already exist ");

//        else
//        {
//            allEng.Add(item);
//            XMLTools.SaveListToXMLSerializer(allEng, "engineers");
//        }
//        return int.Parse(item.IdEngineer.Value.ToString()) ; 

//    }

//    public void Delete(int id)
//    {
//        List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        Engineer eng=allEng.FirstOrDefault(e=>e.IdEngineer == id);
//        if(eng == null)
//            throw new DalDoesNotExistException($" Engineer with ID={id} is not exist ");
//        else
//        {
//            allEng.Remove(eng);
//            XMLTools.SaveListToXMLSerializer(allEng, "engineers");

//        }

//    }

//    public Engineer? Read(Func<Engineer, bool>? filter)
//    {
//        List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        return allEng.FirstOrDefault(filter!);
//    }

//    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
//    {
//        List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");

//        if (filter != null)
//        {
//            return from item in allEng
//                   where filter(item)
//                   select item;
//        }
//        return from item in allEng
//               select item;
//    }

//    public void Reset()
//    {
//        //יוצר רשימה ריקה ומכניס אותה במקום הרשימה הנוכחית
//        List<Engineer> emptyEngineerList = new List<Engineer>();
//        XMLTools.SaveListToXMLSerializer<Engineer>(emptyEngineerList, "engineers");

//    }

//    public void Update(Engineer item)
//    {
//        List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
//        Engineer eng = allEng.FirstOrDefault(e => e.IdEngineer == item.IdEngineer)!;
//        if (eng == null)
//            throw new DalDoesNotExistException($" Enginerr with ID={item.IdEngineer} is not exist ");
//        else
//        {
//            allEng.Remove(eng);
//            allEng.Add(item);
//            XMLTools.SaveListToXMLSerializer(allEng, "engineers");
//        }
//    }
//}
