using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BO;

public class MilestoneOnList
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Nickname { get; set; }
    public DateTime ProductionDate { get; set; }
    public Status Status { get; set; }
    public int progressPercentage { get; set; }

}
