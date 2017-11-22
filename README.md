# testduration.linechart.github.io
Line chart to monitor test duration 

A DotNetCore2 MVC web app to plot line charts for your test suites using google chart Api. Current implementation get test data from team city rest API.

The user is able to select Team City Project > Build Configuration > Branch > Number of past builds user wants to plot the chart for (default to past 25 builds) and Multiselect the test(s).

# Required Libraries
1. Microsoft.AspNetCore.All (V 2.0.0)
2. NLog.Web.AspNetCore
3. RestSharp
4. jQuery Select plugin [Sumoselect](https://github.com/HemantNegi/jquery.sumoselect)
5. [Goole Charts](https://developers.google.com/chart/)

# Feel free to extend
The current implementation uses Team City as the data source. Feel free to extend the app for other repositories like Jenkins and MS Team Services. You need to implement a handler for your CI server using IChartDataHandler, IUrlBuilder and IBuildFilterOptions interfaces. Then change the data source in appsettings.json file and Initialise your handler in ChartDataHandlerFactory.cs file.

Reference : 

Pursley, B. (2016,02,16). Visualize Data Using Google Charts and ASP.NET MVC. Retrieved from https://blog.cinlogic.com/2016/02/26/visualize-data-using-google-charts-and-aspnet-mvc/
