
namespace TestDuration.LineChart.Models.TeamCity
{
    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string href { get; set; }
        public string webUrl { get; set; }
        public string parentProjectId { get; set; }
        public bool? archived { get; set; }
    }
}
