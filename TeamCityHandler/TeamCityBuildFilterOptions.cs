using TestDuration.LineChart.Contracts;


namespace TestDuration.LineChart.TeamCityHandler
{
    public class TeamCityBuildFilterOptions : IBuildFilterOptions
    {
        public string ProjectIdentifier { get; set; }
        public string BuildTypeIdentifier { get; set; }
        public string BuildIdentifier { get; set; }
        public string BranchIdentifier { get; set; }
    }
}
