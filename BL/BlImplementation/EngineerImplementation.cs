using BlApi;
using System.Xml.Linq;

namespace BlImplementation;

internal class EngineerImplementation:IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void AddEngineer(BO.Engineer eng)
    {
        //חסר בדיקת תקינות של מייל
        if(eng.IdEngineer>=0&&eng.Name!=""&&eng.SalaryPerHour>0)
        {
            DO.Engineer doEng = new DO.Engineer(eng.IdEngineer, eng.Name, eng.Email, eng.EngineerLevel, eng.SalaryPerHour);
        }
        else
        {
            throw new Exception();
        }
       
        try 
        {
            int idEng = _dal.Engineer.Create(doEng);
        }
        catch (DO.DalAlreayExistException) 
        {
            //זריקת חריגה של מהנדס קיים מה-BO 
           // throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }
    }

    public BO.Engineer GetEngineerDetails(int idEng)
    {
        DO.Engineer? doEng = _dal.Engineer.Read(????);
        if(doEng==null) 
        {
            //throw new Exception
        }
        return new BO.Engineer()
        {
            IdEngineer = idEng,
            Name = doEng.NameEngineer,
            Email = doEng.MailEnginerr,
            EngineerLevel = doEng.EngineerRank,
            SalaryPerHour = doEng.PricePerHour
        };
    }

    public IEnumerable<BO.Engineer> GetListOfEngineers()
    {
        throw new NotImplementedException();
    }

    public void RemoveEngineer(BO.Engineer eng)
    {
        throw new NotImplementedException();
    }

    public void UpdateEngineerDetails(BO.Engineer eng)
    {
        throw new NotImplementedException();
    }
}
