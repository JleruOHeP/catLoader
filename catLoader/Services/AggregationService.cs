using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CatLoader.Models;
using CatLoader.Providers;

namespace CatLoader.Services
{
    public interface IAggregationService
    {
        Dictionary<TKey, List<TValue>> Aggregate<TKey, TValue>(ICollection<Person> people, 
                                                               Func<Person,TKey> grouping, 
                                                               Func<Pet,bool> filtering, 
                                                               Func<Pet,TValue> projection);
    }

    public class AggregationService : IAggregationService
    {
        public Dictionary<TKey, List<TValue>> Aggregate<TKey, TValue>(ICollection<Person> people,
                                                                      Func<Person,TKey> grouping, 
                                                                      Func<Pet,bool> filtering,
                                                                      Func<Pet,TValue> projection)
        {
            var pets = people
                    .Where(p => p.Pets != null)
                    .SelectMany(p => p.Pets,
                        (person, pet) => new 
                        { 
                            Grouping = grouping(person), 
                            IsValid = filtering(pet), 
                            Projection = projection(pet) 
                        }
                    )
                    .Where(p => p.IsValid)
                    .GroupBy(p => p.Grouping)
                    .ToDictionary(grp => grp.Key, 
                                  grp => grp.Select(g => g.Projection)
                                            .ToList()
                    );

            return pets;
        }
    }
}