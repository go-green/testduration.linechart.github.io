
using System.Collections.Generic;

namespace TestDuration.LineChart.Models.TeamCity
{
    public class BuildsRootObject
    {
        public int count { get; set; }
        public string href { get; set; }
        public string nextHref { get; set; }
        public List<Build> build { get; set; }
    }
}
