using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatLoader.Models;
using CatLoader.Configuration;
using Newtonsoft.Json;

namespace CatLoader.Providers
{
    public interface IPersonProvider
    {
        Task<ICollection<Person>> LoadPeople();
    }

    public class PersonProvider : IPersonProvider
    {
        private readonly IPersonSourceConfig _config;
        
        public PersonProvider(IPersonSourceConfig config)
        {
            _config = config;
        } 

        public async Task<ICollection<Person>> LoadPeople()
        {
            try
            {
                using (var client = new HttpClient())
                {                
                    var response = await client.GetAsync(_config.PersonSourceUrl);
                    if (response.IsSuccessStatusCode)
                    {                        
                        var json = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ICollection<Person>>(json);            
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }            
        }
    }
}