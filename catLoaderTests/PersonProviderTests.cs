using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using CatLoader;
using CatLoader.Services;
using CatLoader.Providers;
using CatLoader.Models;
using CatLoader.Configuration;
using Moq;


namespace CatLoader.IntegrationTests.Services
{
    public class PersonProviderTests
    {        
        [Fact(Skip="Test server is required!")]
        //[Fact]
        public async void LoadDataFromTestService_Test()
        {
            var personSourceConfigMock = new Mock<IPersonSourceConfig>();
            personSourceConfigMock.Setup(c => c.PersonSourceUrl).Returns("http://localhost:3000");
            var provider = new PersonProvider(personSourceConfigMock.Object);

            var people = await provider.LoadPeople();

            Assert.Single(people, p => p.Name == "Steve");
        }
    }
}

