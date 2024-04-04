using BlApi;
using BO;
using DO;
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
                    // Convert BO.Experience to DO.Experience
                    (DO.Experience?)Enum.Parse(typeof(DO.Experience), eng.EngineerLevel.ToString()),
                    eng.SalaryPerHour
                    );
                int idEng = _dal.Engineer.Create(doEng);
            }
            else
            {
                throw new BlAlreadyExistException($" Engineer with ID={eng.IdEngineer} already exist ");
            }

        }
        catch (BO.BlAlreadyExistException)
        {
            //זריקת חריגה של מהנדס קיים מה-BO 
            // throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }
    }
    public BO.Engineer? GetEngineerDetails(int? idEng)
    {
        if(idEng == 0) return null;
        DO.Engineer? doEng = _dal.Engineer.Read(e => e.IdEngineer == idEng);
        if(doEng==null) 
        {
            //throw new Exception
            // Handle the case where the engineer is not found, for example, throw an exception
            throw new BlNullPropertyException("Engineer not found");
        }
        return new BO.Engineer()
        {
            IdEngineer = idEng??0,
            Name = doEng.NameEngineer,
            Email = doEng.MailEnginerr,
            EngineerLevel = (BO.Experience)(int)Enum.Parse(typeof(BO.Experience), doEng.EngineerRank.ToString()),
            SalaryPerHour = doEng.PricePerHour ?? 0   
        };
    }
    public BO.EngineerInTask? GetEngineerInTask(int? idEng)
    {
        if (idEng == 0) return null;
        DO.Engineer? doEng = _dal.Engineer.Read(e => e.IdEngineer == idEng);
        if (doEng == null)
        {
            //throw new Exception
            // Handle the case where the engineer is not found, for example, throw an exception
            throw new BlNullPropertyException("Engineer not found");
        }
        return new BO.EngineerInTask()
        {
            IdEngineer = idEng ?? 0,
            Name = doEng.NameEngineer,
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
            SalaryPerHour = doEngineer.PricePerHour??0,
            CurrentTask = null
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
                throw new BlDoesNotExistException($"Engineer with id={idEng} not exist");
            }

        }
        catch (BO.BlDoesNotExistException)
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
                    (DO.Experience?)Enum.Parse(typeof(DO.Experience), eng.EngineerLevel.ToString()),
                    eng.SalaryPerHour
                    );
                _dal.Engineer.Update(doEng);
            }
            else
            {
                throw new BlAlreadyExistException($" Enginerr with ID={eng.IdEngineer} is not exist ");
            }

        }
        catch (BO.BlAlreadyExistException)
        {
        }
    }
}
