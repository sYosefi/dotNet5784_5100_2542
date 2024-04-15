
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {
        try
        {
            int newNum = DataSource.Config.NextIdDependence;
            Dependence newDep = item with { IdDependence = newNum };
            DataSource.Dependences.Add(newDep);
            return newNum;
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw;
        }
    }

    public void Delete(int id)
    {
        try
        {
            Dependence dependence = DataSource.Dependences.FirstOrDefault(d => d.IdDependence == id)!;
            if (dependence == null)
                throw new DalDoesNotExistException($" Dependence with ID={id} does not exist ");
            else
            {
                DataSource.Dependences.Remove(dependence);
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw;
        }
    }

    public Dependence? Read(Func<Dependence, bool>? filter)
    {
        try
        {
            return DataSource.Dependences.FirstOrDefault(filter!);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw;
        }
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        try
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
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw;
        }
    }

    public void Reset()
    {
        try
        {
            DataSource.Dependences.Clear();
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw;
        }
    }

    public void Update(Dependence item)
    {
        try
        {
            Dependence dependence = DataSource.Dependences.FirstOrDefault(d => d.IdDependence == item.IdDependence)!;
            if (dependence == null)
                throw new DalDoesNotExistException($" Dependence with ID={item.IdDependence} does not exist ");
            else
            {
                DataSource.Dependences.Remove(dependence);
                DataSource.Dependences.Add(item);
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw;
        }
    }
   
}
