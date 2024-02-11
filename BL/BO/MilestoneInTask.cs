

namespace BO;

public class MilestoneInTask
{
    public int MilestoneNum { get; set; }
    public string Nickname { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
