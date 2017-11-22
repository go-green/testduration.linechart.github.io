
using System.Collections.Generic;

namespace TestDuration.LineChart.Models.TeamCity
{
    public class BuildTypeRootObject
    {
        public int count { get; set; }
        public List<BuildType> buildType { get; set; }
    }
}
