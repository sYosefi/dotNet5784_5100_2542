namespace BlApi;

public interface IEngineer
{
    public IEnumerable<BO.Engineer> GetListOfEngineers();
    public BO.Engineer GetEngineerDetails(int? idEng);
    public void AddEngineer(BO.Engineer eng);
    public void RemoveEngineer(int idEng);
    public void UpdateEngineerDetails(BO.Engineer eng);




}
