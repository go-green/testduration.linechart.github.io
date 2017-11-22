
using TestDuration.LineChart.Models.TestResults;
using System.Collections.Generic;

namespace TestDuration.LineChart.Contracts
{
    public interface IChartDataHandler
    {
        IEnumerable<Item> GetProjects();
        IEnumerable<Item> GetBuildConfigurations();
        IEnumerable<Item> GetBuilds();
        IEnumerable<Item> GetBranchs();
        IEnumerable<Item> GetTests();
        IBuildFilterOptions BuildFilterOptions { get; set; }
    }
}
