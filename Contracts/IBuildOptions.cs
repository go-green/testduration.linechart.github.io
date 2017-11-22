

namespace TestDuration.LineChart.Contracts
{
    public interface IBuildFilterOptions
    {
        string ProjectIdentifier { get; set; }
        string BuildTypeIdentifier { get; set; }
        string BuildIdentifier { get; set; }
        string BranchIdentifier { get; set; }
    }
}
