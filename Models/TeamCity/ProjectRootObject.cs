
using System.Collections.Generic;


namespace TestDuration.LineChart.Models.TeamCity
{
    public class ProjectRootObject
    {
        public int count { get; set; }
        public string href { get; set; }
        public List<Project> project { get; set; }
    }
}
