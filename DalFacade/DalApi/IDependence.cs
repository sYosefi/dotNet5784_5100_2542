namespace DalApi;
using DO;
public interface IDependence
{
    int Create(Dependence item);//Creates new entity object in DAL
    Dependence? Read(int id);//Reads entity object by its ID 
    List<Dependence> ReadAll();//Reads all entity objects
    void Update(Dependence item);//Updates entity objects
    void Delete(int id);//Deletes an object by is Id
}


