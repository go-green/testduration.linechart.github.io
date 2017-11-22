using TestDuration.LineChart.Configuration;
using TestDuration.LineChart.Contracts;
using TestDuration.LineChart.TeamCityHandler;

namespace TestDuration.LineChart.Controllers
{
    public class ChartDataHandlerFactory
    {
        public static IChartDataHandler GetHandler(string dataSource, RestApiUrlDetails urlDetails)
        {
            IChartDataHandler handler = null;
            if (dataSource.Equals("TeamCity"))
            {
                handler =  new TeamCityRestApiHandler(urlDetails);
            }
            return handler;
        }
    }
}
