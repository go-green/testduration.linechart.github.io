
using TestDuration.LineChart.Configuration;
using TestDuration.LineChart.Contracts;
using System;

namespace TestDuration.LineChart.TeamCityHandler
{
    public class TeamCityUrlBuilder : IUrlBuilder
    {
        private UriBuilder _builder;
        public string ProjectIdentifier { get; set; }
        public string BuildTypeIdentifier { get; set; }
        public string BuildIdentifier { get; set; }
        public string BranchIdentifier { get; set; }

        public TeamCityUrlBuilder(RestApiUrlDetails teamCityUrl)
        {
            _builder = new UriBuilder
            {
                Scheme = teamCityUrl.Scheme,
                Host = teamCityUrl.Host,
                Path = teamCityUrl.Path,
                Port = teamCityUrl.Port
            };
        }

        public Uri Projects()
        {
            return new Uri(_builder.Uri, "projects");
        }

        public Uri BuildConfigurations()
        {
            if (ProjectIdentifier == null)
            {
                throw new ArgumentNullException("ProjectIdentifier parameter has not been set");
            }
            return new Uri(_builder.Uri, $"projects/id:{ProjectIdentifier}/buildTypes");
        }

        public Uri Builds()
        {
            if (BuildTypeIdentifier == null || BranchIdentifier == null)
            {
                throw new ArgumentNullException("BuildTypeIdentifier or BranchIdentifier parameters have not been set");
            }
            return new Uri(_builder.Uri, $"buildTypes/id:{BuildTypeIdentifier}/builds/?locator=branch:{BranchIdentifier}");
        }

        public Uri Branches()
        {
            if (BuildTypeIdentifier == null)
            {
                throw new ArgumentNullException("BuildTypeIdentifier parameter has not been set");
            }
            return new Uri(_builder.Uri, $"buildTypes/id:{BuildTypeIdentifier}/branches");
        }

        public Uri Tests()
        {
            if (BuildIdentifier != null)
            {
                return new Uri(_builder.Uri, $"testOccurrences?locator=build:(id:{BuildIdentifier})");
            }
            else if (BuildTypeIdentifier != null && BranchIdentifier != null)
            {
                return new Uri(_builder.Uri, $"testOccurrences?locator=build:(buildType:{BuildTypeIdentifier},count:1,branch:{BranchIdentifier})");
            }
            else 
            {
                throw new ArgumentNullException("BuildIdentifier,BuildTypeIdentifier or BranchIdentifier parameters have not been set");
            }     
        }
    }
}
