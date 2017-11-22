
using System.Collections.Generic;
using TestDuration.LineChart.Configuration;
using TestDuration.LineChart.Models.TeamCity;
using TestDuration.LineChart.Contracts;
using System;
using TestDuration.LineChart.Models.TestResults;
using RestSharp;
using Newtonsoft.Json;

namespace TestDuration.LineChart.TeamCityHandler
{
    public class TeamCityRestApiHandler : IChartDataHandler
    {
        private TeamCityUrlBuilder _url;
        private TestDetailAdapter _testDetailAdapter;
        public IBuildFilterOptions BuildFilterOptions { get; set; }
        public TeamCityRestApiHandler(RestApiUrlDetails _tcUrlDetails)
        {
            _url = new TeamCityUrlBuilder(_tcUrlDetails);
            _testDetailAdapter = new TestDetailAdapter();
            BuildFilterOptions = new TeamCityBuildFilterOptions();
        }

        public IEnumerable<Item> GetProjects()
        {
            return GetDataFormTeamCityRestApi<ProjectRootObject>(_url.Projects());
        }

        public IEnumerable<Item> GetBuildConfigurations()
        {
            _url.ProjectIdentifier = BuildFilterOptions.ProjectIdentifier;
            return GetDataFormTeamCityRestApi<BuildTypeRootObject>(_url.BuildConfigurations());
        }

        public IEnumerable<Item> GetBranchs()
        {
            _url.BuildTypeIdentifier = BuildFilterOptions.BuildTypeIdentifier;
            return GetDataFormTeamCityRestApi<BranchRootObject>(_url.Branches());
        }

        public IEnumerable<Item> GetBuilds()
        {
            _url.BranchIdentifier = BuildFilterOptions.BranchIdentifier;
            _url.BuildTypeIdentifier = BuildFilterOptions.BuildTypeIdentifier;
            return GetDataFormTeamCityRestApi<BuildsRootObject>(_url.Builds());
            
        }

        public IEnumerable<Item> GetTests()
        {
            _url.BranchIdentifier = BuildFilterOptions.BranchIdentifier;
            _url.BuildTypeIdentifier = BuildFilterOptions.BuildTypeIdentifier;
            _url.BuildIdentifier = BuildFilterOptions.BuildIdentifier;
            return GetDataFormTeamCityRestApi<TestsRootObject>(_url.Tests());
        }

        private IEnumerable<Item> GetDataFormTeamCityRestApi<T>(Uri uri) where T : class
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response;
            try
            {
                response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var content =  JsonConvert.DeserializeObject<T>(response.Content);
                    return _testDetailAdapter.MapTestDetails<T>(content);
                }
                else
                {
                    throw response.ErrorException;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                client = null;
                request = null;
                response = null;
            }
        }
    }
}
