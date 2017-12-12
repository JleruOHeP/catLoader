using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using CatLoader;
using CatLoader.Services;
using CatLoader.Providers;
using CatLoader.Models;
using Moq;


namespace CatLoader.UnitTests.Services
{
    public class CatLoaderServiceTests
    {        
        [Fact]
        public async void NoPets_DoesNotThrow_Test()
        {
            ICollection<Person> people = new List<Person> {new Person { Name = "Bob"} };

            var personProviderMock = new Mock<IPersonProvider>();
            personProviderMock.Setup(c => c.LoadPeople())
                .Returns(() => Task.FromResult(people));
            var aggregationService = new AggregationService();

            var service = new CatLoaderService(personProviderMock.Object, aggregationService);
            await service.Run();
        }

        [Fact]
        public async void NoPeople_DoesNotThrow_Test()
        {
            ICollection<Person> people = null;

            var personProviderMock = new Mock<IPersonProvider>();
            personProviderMock.Setup(c => c.LoadPeople())
                .Returns(() => Task.FromResult(people));
            var aggregationService = new AggregationService();

            var service = new CatLoaderService(personProviderMock.Object, aggregationService);
            await service.Run();
        }

        [Fact]
        public async void GoodData_DoesNotThrow_Test()
        {
            ICollection<Person> people = new List<Person> {
                new Person 
                { 
                    Name = "Bob", 
                    Pets = new List<Pet> { new Pet {Name = "Cat", Type = PetType.Dog } } 
                } 
            };

            var personProviderMock = new Mock<IPersonProvider>();
            personProviderMock.Setup(c => c.LoadPeople())
                .Returns(() => Task.FromResult(people));
            var aggregationService = new AggregationService();

            var service = new CatLoaderService(personProviderMock.Object, aggregationService);
            await service.Run();
        }
    }
}

