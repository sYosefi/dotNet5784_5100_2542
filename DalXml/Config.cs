//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Dal;

internal static class Config
{
    static string s_data_config_xml = "data-config";
    static string s_task_xml = "tasks";
    static string s_engineer_xml = "engineers";
    static string s_dependence_xml = "dependences";

    internal static int NextDependenceId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependenceId"); }
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "TaskNumber"); }

}
