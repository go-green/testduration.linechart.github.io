
using System.Collections.Generic;

namespace TestDuration.LineChart.Models.TestResults
{
    public class ChartModel
    {
        private BuildDetails _buildDetails;
        private GoogleVisualizationDataTable _dataTable;
        private IList<string> _errors;

        public ChartModel()
        {
            _buildDetails = new BuildDetails();
            _dataTable = new GoogleVisualizationDataTable();
        }
        public BuildDetails Build
        {
            get { return _buildDetails; }
            set { _buildDetails = value; }
        }
        public string ChartName { get; set; }
        public string ChartSubTitle { get; set; }
        public GoogleVisualizationDataTable DataTable
        {
            get { return _dataTable; }
            set { _dataTable = value; }
        }
        public IList<string> Errors
        {
            get { return _errors; }
            set { _errors = value;}
        }
    }
}
