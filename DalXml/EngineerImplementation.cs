

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        //int nextId = Config.NextDependenceId;
        //List<Dependence> allDep = XMLTools.LoadListFromXMLSerializer<Dependence>("dependences");
        //Dependence newDep = item with { IdDependence = nextId };
        //allDep.Add(newDep);
        //XMLTools.SaveListToXMLSerializer<Dependence>(allDep, "dependences");
        //return nextId;



        List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        //if (allEng.FirstOrDefault(e => e.IdEngineer == item.IdEngineer) != null)
        //    throw new DalAlreayExistException($" Engineer with ID={item.IdEngineer} already exist ");

        //else
        //{
            allEng.Add(item);
            XMLTools.SaveListToXMLSerializer(allEng, "engineers");
      //  }
        return int.Parse(item.IdEngineer.Value.ToString()) ;
    }

    public void Delete(int id)
    {
        List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer eng=allEng.FirstOrDefault(e=>e.IdEngineer == id);
        if(eng == null)
            throw new DalDoesNotExistException($" Engineer with ID={id} is not exist ");
        else
        {
            allEng.Remove(eng);
            XMLTools.SaveListToXMLSerializer(allEng, "engineers");

        }

    }

    public Engineer? Read(Func<Engineer, bool>? filter)
    {
        List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return allEng.FirstOrDefault(filter!);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");

        if (filter != null)
        {
            return from item in allEng
                   where filter(item)
                   select item;
        }
        return from item in allEng
               select item;
    }

    public void Update(Engineer item)
    {
        List<Engineer> allEng = XMLTools.LoadListFromXMLSerializer<Engineer>("Engineer");
        Engineer eng = allEng.FirstOrDefault(e => e.IdEngineer == item.IdEngineer)!;
        if (eng == null)
            throw new DalDoesNotExistException($" Enginerr with ID={item.IdEngineer} is not exist ");
        else
        {
            allEng.Remove(eng);
            allEng.Add(eng);
        }
    }
}
