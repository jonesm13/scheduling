namespace Domain.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Features.Template;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CanCreateATemplate : IntegrationTestBase
    {
        [TestMethod]
        public async Task Test()
        {
            string templateName = "Template" + DateTime.UtcNow.Ticks;

            Create.Command command = new Create.Command
            {
                Name = templateName
            };

            await Mediator().Send(command);

            IEnumerable<Index.Model> results = await Mediator().Send(new Index.Query());

            Assert.IsTrue(results.Any(x => x.Name.Equals(templateName)));
        }
    }
}