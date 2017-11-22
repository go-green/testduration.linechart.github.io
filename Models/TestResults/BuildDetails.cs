
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TestDuration.LineChart.Models.TestResults
{
    public class BuildDetails
    {
        public IEnumerable<Item> Projects { get; set; }
        public IEnumerable<int> SelectedProject { get; set; }
        public IEnumerable<Item> BuildConfigurations { get; set; }
        public IEnumerable<int> SelectedBuildConfiguration { get; set; }
        public IEnumerable<Item> Builds { get; set; }
        public IEnumerable<int> SelectedBuilds { get; set; }
        public IEnumerable<Item> Branches { get; set; }
        public IEnumerable<int> SelectedBranch { get; set; }
        public IEnumerable<Item> Tests { get; set; }
        public IEnumerable<int> SelectedTests { get; set; }
        public int BuildCount { get; set; }
    }
}
