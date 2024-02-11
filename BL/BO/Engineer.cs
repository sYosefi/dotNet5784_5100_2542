using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BO;

public class Engineer
{
    public int IdEngineer { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Experience EngineerLevel { get; set; }
    public int SalaryPerHour { get; set; }
    public Task? CurrentTask { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
