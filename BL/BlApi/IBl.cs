namespace BlApi;

public interface IBl
{
    public ITask Task { get; }
    public IEngineer Engineer { get; }
    public DateTime Clock { get; }
    void Reset();
    void InsertYear(int years);
    void InsertMonth(int months);
    void InsertDay(int days);
    void ResetTime();
}
