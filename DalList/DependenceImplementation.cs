
namespace Dal;
using DalApi;
using DO;

public class DependenceImplementation : IDependence
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

    public Dependence? Read(int id)
    {
        return DataSource.Dependences.FirstOrDefault(d => d.IdDependence == id)!; 
    }

    public List<Dependence> ReadAll()
    {
        return new List<Dependence>(DataSource.Dependences);
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
