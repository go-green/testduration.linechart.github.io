using System;

namespace TestDuration.LineChart.Contracts
{
    public interface IUrlBuilder
    {
        Uri Projects();
        Uri BuildConfigurations();
        Uri Builds();
        Uri Branches();
        Uri Tests();
    }
}
