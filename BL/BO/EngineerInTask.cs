using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BO;

public class EngineerInTask
{
    public int IdEngineer { get; init; }
    public string Name { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }

}
