using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon.Primitives;

namespace PL;

internal class ExperienceCollection:IEnumerable
{
    static readonly IEnumerable<BO.Experience> s_enums = (Enum.GetValues(typeof(BO.Experience)) as IEnumerable<BO.Experience>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

public class StatusCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status> s_enum = (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;
    public IEnumerator GetEnumerator() => s_enum.GetEnumerator();
}
