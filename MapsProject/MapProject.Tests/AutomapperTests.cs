using AutoMapper;
using MapsProject.WEB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MapProject.Tests
{
    [TestClass]
    public class AutomapperTests
    {
        [TestMethod]
        public void TheAutoMapperConfigurationShouldBeValid()
        {
            Mapper.Reset();
            MapperConfig.Configure();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
