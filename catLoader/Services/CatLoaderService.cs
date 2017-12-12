using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CatLoader.Models;
using CatLoader.Providers;

namespace CatLoader.Services
{
    public interface ICatLoaderService
    {
        Task Run();
    }

    public class CatLoaderService : ICatLoaderService
    {
        private readonly IPersonProvider _personProvider;
        private readonly IAggregationService _aggregationService;

        public CatLoaderService(IPersonProvider personProvider, IAggregationService aggregationService)
        {
            _personProvider = personProvider;
            _aggregationService = aggregationService;
        } 

        public async Task Run()
        {
            var people = await _personProvider.LoadPeople();
            
            if (people == null)
            {
                Console.WriteLine("No data available.");
                return;
            }

            var filteredData = _aggregationService.Aggregate(
                    people,
                    owner => owner.Gender, 
                    pet => pet.Type == PetType.Cat, 
                    pet => pet.Name
                    );

            foreach (var entry in filteredData)
            {
                Console.WriteLine(entry.Key);
                foreach (var pet in entry.Value.OrderBy(p => p))
                {
                    Console.WriteLine($"  * {pet}");
                }
            }
        }
    }
}