using BlApi;
using BO;
using System;
using System.Xml.Linq;
namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private List<EngineerInTask> EngineerInTaskList = new List<EngineerInTask>();
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void AddEngineer(BO.Engineer eng)
    {
        try
        {
            if (eng.IdEngineer >= 0 && eng.Name != "" && eng.SalaryPerHour > 0 && eng.Email?.Contains("@") == true)
            {
                DO.Engineer doEng = new DO.Engineer(
                    eng.IdEngineer,
                    eng.Name,
                    eng.Email,
                    // Convert BO.Experience to DO.Experience?
                    (DO.Experience?)eng.EngineerLevel,
                    eng.SalaryPerHour
                    );
                int idEng = _dal.Engineer.Create(doEng);
            }
            else
            {
                throw new Exception();
            }

        }
        catch (DO.DalAlreayExistException)
        {
            //זריקת חריגה של מהנדס קיים מה-BO 
            // throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }
    }
    public BO.Engineer GetEngineerDetails(int? idEng)
    {
        DO.Engineer? doEng = _dal.Engineer.Read(e => e.IdEngineer == idEng);
        if(doEng==null) 
        {
            //throw new Exception
            // Handle the case where the engineer is not found, for example, throw an exception
            throw new Exception("Engineer not found");
        }
        return new BO.Engineer()
        {
            IdEngineer = idEng??0,
            Name = doEng.NameEngineer,
            Email = doEng.MailEnginerr,
            EngineerLevel = (BO.Experience)(int)Enum.Parse(typeof(BO.Experience), doEng.EngineerRank.ToString()),
            // Use ?? with a default value, adjust as needed
            SalaryPerHour = doEng.PricePerHour ?? 0   
        };
    }

    public IEnumerable<BO.Engineer> GetListOfEngineers()
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll() select new BO.Engineer
        {
            IdEngineer = doEngineer.IdEngineer??0,
            Name = doEngineer.NameEngineer,
            Email = doEngineer.MailEnginerr,
            EngineerLevel = (BO.Experience)(int)Enum.Parse(typeof(BO.Experience), doEngineer.EngineerRank.ToString()),
            SalaryPerHour = doEngineer.PricePerHour??0
           // CurrentTask = null
        }) ;
    }

    public void RemoveEngineer(int idEng)
    {
        try
        {
            if (!EngineerInTaskList.Any(e => e.IdEngineer == idEng))
            {
                _dal.Engineer.Delete(idEng);
            }
            else
            {
                throw new Exception();
            }

        }
        catch (DO.DalAlreayExistException)
        {
            //זריקת חריגה של מהנדס קיים מה-BO 
            // throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }
    }

    public void UpdateEngineerDetails(BO.Engineer eng)
    {
        try
        {
            if (eng.IdEngineer >= 0 && eng.Name != "" && eng.SalaryPerHour > 0 && eng.Email?.Contains("@") == true)
            {
                DO.Engineer doEng = new DO.Engineer(
                    eng.IdEngineer,
                    eng.Name,
                    eng.Email,
                    // Convert BO.Experience to DO.Experience?
                    (DO.Experience?)eng.EngineerLevel,
                    eng.SalaryPerHour
                    );
                _dal.Engineer.Update(doEng);
            }
            else
            {
                throw new Exception();
            }

        }
        catch (DO.DalAlreayExistException)
        {
            //זריקת חריגה של מהנדס קיים מה-BO 
            // throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }
    }
}
