namespace Domain.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Features.Station;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CanCreateAStation : IntegrationTestBase
    {
        [TestMethod]
        public async Task Test()
        {
            string name = "106.8 The Bear" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            Create.Command request = new Create.Command
            {
                Name = name
            };

            await Mediator().Send(request);

            IEnumerable<Index.Model> stations = await Mediator().Send(new Index.Query());

            Assert.IsTrue(stations.Any(x => x.Name.Equals(name)));
        }
    }
}