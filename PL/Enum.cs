using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;

internal class ExperienceCollection:IEnumerable
{
    static readonly IEnumerable<BO.Experience> s_enums = (Enum.GetValues(typeof(BO.Experience)) as IEnumerable<BO.Experience>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
