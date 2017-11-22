
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestDuration.LineChart.Configuration;
using TestDuration.LineChart.Contracts;
using TestDuration.LineChart.Models.TestResults;
using System.Collections.Generic;
using System.Linq;

namespace TestDuration.LineChart.Controllers
{

    public class HomeController : Controller
    {
        private IChartDataHandler _chartDataHandler;
        private ChartModel _chartModel;
        private RestApiUrlDetails _urlDetails;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IOptions<RestApiUrlDetails> urlDetails,
                              IOptions<DataSource> dataSource,
                              ILogger<HomeController> logger)
        {
            _chartDataHandler = ChartDataHandlerFactory.GetHandler(dataSource.Value.Name, urlDetails.Value);
            _chartModel = new ChartModel();
            _urlDetails = urlDetails.Value;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var projects = _chartDataHandler.GetProjects();
            _chartModel.Build.Projects = projects;
            return View(_chartModel);
        }

        [HttpPost]
        public IActionResult Index(Identifier identifiers)
        {
            _chartDataHandler.BuildFilterOptions.BranchIdentifier = identifiers.branch;
            _chartDataHandler.BuildFilterOptions.BuildTypeIdentifier = identifiers.buildConfig;
            var buildCountFromConfig = _urlDetails.BuildCount ?? 25;
            var buildCount = identifiers.buildCount  == 0 ? buildCountFromConfig : identifiers.buildCount;
            var builds = _chartDataHandler.GetBuilds().ToList().Take(buildCount);

            var testsDetailsPerBuild = new Dictionary<int, IEnumerable<Item>>();
            foreach (var build in builds)
            {
                _chartDataHandler.BuildFilterOptions.BuildIdentifier = build.BuildNumber.ToString();
                var selectedTestsDetailsforTheBuild = _chartDataHandler.GetTests().Where(test => identifiers.tests.Contains(test.Name));
                testsDetailsPerBuild.Add(build.BuildNumber, selectedTestsDetailsforTheBuild);
            }

            var chartData = new List<Item>();
            foreach (var build in builds)
            {
                var testsPerBuild = testsDetailsPerBuild.FirstOrDefault(testsDetail => testsDetail.Key.Equals(build.BuildNumber)).Value;
                if (testsPerBuild != null)
                {
                    foreach (var testName in identifiers.tests)
                    {
                        var test = testsPerBuild.FirstOrDefault(tperbuild => tperbuild.Name.Equals(testName));
                        if (test != null)
                        {
                            chartData.Add(
                            new Item
                            {
                                BuildNumber = build.BuildNumber,
                                Name = test.Name,
                                Duration = test.Duration
                            });
                        }
                    }
                }
            }

            _chartModel.ChartName = $"Test Duration Chart for {identifiers.buildConfig}";
            _chartModel.ChartSubTitle = $"Build Number Vs Test time in milliseconds";
            _chartModel.DataTable = ConstructDataTable(chartData);
            return Json(_chartModel);
        }

        public IActionResult FillBuildConfigurations(Identifier identifiers)
        {
            _chartDataHandler.BuildFilterOptions.ProjectIdentifier = identifiers.project;
            var buildConfigurations = _chartDataHandler.GetBuildConfigurations();
            return Json(buildConfigurations);
        }

        public IActionResult FillBranches(Identifier identifiers)
        {
            _chartDataHandler.BuildFilterOptions.BuildTypeIdentifier = identifiers.buildConfig;
            var branches = _chartDataHandler.GetBranchs();
            return Json(branches);
        }

        public IActionResult FillTests(Identifier identifiers)
        {
            _chartDataHandler.BuildFilterOptions.BuildTypeIdentifier = identifiers.buildConfig;
            _chartDataHandler.BuildFilterOptions.BranchIdentifier = identifiers.branch;
           var tests = _chartDataHandler.GetTests();
            return Json(tests);
        }

        private GoogleVisualizationDataTable ConstructDataTable(List<Item> chartData)
        {
            var dataTable = new GoogleVisualizationDataTable();

            // Get distinct testNames from the data
            var testNames = chartData.Select(x => x.Name).Distinct();

            // Get buildNumbers from the data
            var buildNumbers = chartData.Select(x => x.BuildNumber).Distinct();

            // Specify the columns for the DataTable.
            // In this example, it is BuildNumber and then a testName column for test.
            dataTable.AddColumn("Build Number", "number");
            foreach (var testName in testNames)
            {
                dataTable.AddColumn(testName, "number");
            }

            // Specify the rows for the DataTable.
            // Each test will be its own row
            foreach (var buildNumber in buildNumbers)
            {
                var values = new List<object>();
                values.Add(buildNumber);
                foreach (var testName in testNames)
                {
                    var test = chartData.FirstOrDefault(x => x.BuildNumber.Equals(buildNumber) && x.Name.Equals(testName));
                    if (test != null)
                    {
                        values.Add(test.Duration);
                    }
                }
                dataTable.AddRow(values);
            }
            return dataTable;
        }
    }

    public class Identifier
    {
        public string project { get; set; }
        public string buildConfig { get; set; }
        public string branch { get; set; }
        public int buildCount { get; set; }
        public string[] tests { get; set; }
    }
}
