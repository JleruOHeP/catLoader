using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;
using CatLoader;
using CatLoader.Models;
using CatLoader.Services;


namespace CatLoader.UnitTests.Services
{
    public class AggregationServiceTests
    {
        private ICollection<Person> mockPeople = new List<Person> {
            new Person 
            { 
                Name = "Bob", 
                Gender = Gender.Male,
                Age = 30,
                Pets = new List<Pet> 
                {
                    new Pet { Type = PetType.Cat, Name = "Nope" }
                } 
            },
            new Person 
            { 
                Name = "John", 
                Gender = Gender.Male,
                Pets = new List<Pet> 
                {
                    new Pet { Type = PetType.Dog, Name = "Snappy" }
                } 
            },
            new Person 
            { 
                Name = "Alice", 
                Gender = Gender.Female,
                Age = 20,
                Pets = new List<Pet> 
                {
                    new Pet { Type = PetType.Fish, Name = "Talky" }
                } 
            },
            new Person 
            { 
                Name = "Ann", 
                Gender = Gender.Female,
                Age = 23                
            } 
        };  

        [Fact]
        public void GroupByGender_FilterByPetType_ProjectName_Test()
        {            
            var service = new AggregationService();
            
            var result = service.Aggregate(mockPeople,
                                           p => p.Gender,
                                           p => p.Type == PetType.Fish,
                                           p => p.Name
                                          );

            Assert.NotEmpty(result);
            Assert.Equal("Talky", result[Gender.Female].First());
        }

        [Fact]
        public void GroupByPersonName_FilterByPetName_ProjectType_Test()
        {            
            var service = new AggregationService();
            
            var result = service.Aggregate(mockPeople,
                                           p => p.Name,
                                           p => p.Name == "Snappy",
                                           p => p.Type
                                          );

            Assert.NotEmpty(result);
            Assert.Equal(PetType.Dog, result["John"].First());
        }

        [Fact]
        public void NoPets_NotIncludedInResults_Test()
        {            
            var service = new AggregationService();
            
            var result = service.Aggregate(mockPeople,
                                           p => p.Name,
                                           p => p.Name != null,
                                           p => p.Type
                                          );
            
            Assert.False(result.ContainsKey("Ann"));
        }
    }
}

