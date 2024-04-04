
using BlApi;
using System;

namespace BlImplementation;

internal class Bl : IBl
{
    public ITask Task => new TaskImplementation(this);

    public IEngineer Engineer =>  new EngineerImplementation();

    private static DateTime s_Clock = DateTime.Now.Date;
    public DateTime Clock { get { return s_Clock; } private set { s_Clock = value; } }
    public void Reset()
    {
        DalApi.IDal _dal = DalApi.Factory.Get;
        _dal.Reset();
    }

    public void InsertYear(int years)
    {
        s_Clock = s_Clock.AddYears(years);
    }

    public void InsertMonth(int months)
    {
        s_Clock = s_Clock.AddMonths(months);
    }

    public void InsertDay(int days)
    {
        s_Clock = s_Clock.AddDays(days);
    }

    public void ResetTime()
    {
        s_Clock = s_Clock.Date;
    }
}
