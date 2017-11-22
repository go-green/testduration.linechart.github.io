using System;

namespace TestDuration.LineChart.Models.TestResults
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int Duration { get; set; }
        public int BuildNumber { get; set; }
        public DateTime Date { get; set; }
    }
}