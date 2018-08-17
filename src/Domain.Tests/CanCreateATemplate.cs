namespace Domain.Tests
{
    using System;
    using System.Threading.Tasks;
    using Features.Template;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CanCreateATemplate : IntegrationTestBase
    {
        [TestMethod]
        public async Task Test()
        {
            Create.Command command = new Create.Command
            {
                Name = "Template" + DateTime.UtcNow.Ticks
            };

            await Mediator().Send(command);
        }
    }
}