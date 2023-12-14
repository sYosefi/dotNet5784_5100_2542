

namespace DO;

public record Engineer
    ( 
    int? IdEngineer,
    string NameEngineer,
    string MailEnginerr,
    Experience? EngineerRank,
    int? PricePerHour
)
{
    public Engineer():this(null,"","",null,null)
    {
    }
}

