using TestDuration.LineChart.Models.TeamCity;
using TestDuration.LineChart.Models.TestResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDuration.LineChart.TeamCityHandler
{
    public class TestDetailAdapter
    {
        public IEnumerable<Item> MapTestDetails<T>(T content) where T : class
        {
            var type = content.GetType();
            if (type.Equals(typeof(ProjectRootObject)))
            {
                var list = ConvertValue<ProjectRootObject, T>(content);
                return list == null ? Enumerable.Empty<Item>()
                    : list.project.Skip(1).Select(p => new Item { Name = p.name, Id = p.id });
            }
            else if (type.Equals(typeof(BuildTypeRootObject)))
            {
                var list = ConvertValue<BuildTypeRootObject, T>(content);
                return list == null ? Enumerable.Empty<Item>()
                    : list.buildType.Select(bt => new Item { Name = bt.name, Id = bt.id });
            }
            else if (type.Equals(typeof(BranchRootObject)))
            {
                var list = ConvertValue<BranchRootObject, T>(content);
                return list == null ? Enumerable.Empty<Item>()
                    : list.branch.Select(b => new Item { Name = b.name, Id = b.name });
            }
            else if (type.Equals(typeof(BuildsRootObject)))
            {
                var list = ConvertValue<BuildsRootObject, T>(content);
                var filtered = list.build.Where(x => x.status.Equals("SUCCESS"));
                return filtered == null ? Enumerable.Empty<Item>()
                    : filtered.Select(b => new Item { BuildNumber = b.id });
            }
            else if (type.Equals(typeof(TestsRootObject)))
            {
                var list = ConvertValue<TestsRootObject, T>(content);
                return list == null || list.testOccurrence == null ? Enumerable.Empty<Item>()
                    : list.testOccurrence.Select(t => new Item { Name = t.name, Id = t.name, Duration = t.duration, Status = t.status });
            }
            else
            {
                throw new Exception($"Unsupported TeamCity REST API model object {type.ToString()}");
            }
        }

        private T ConvertValue<T, U>(U value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
