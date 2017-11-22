
using System.Collections.Generic;

namespace TestDuration.LineChart.Models.TeamCity
{
    public class TestsRootObject
    {
        public int count { get; set; }
        public string href { get; set; }
        public List<TestOccurrence> testOccurrence { get; set; }
        public bool @default { get; set; }
    }
}