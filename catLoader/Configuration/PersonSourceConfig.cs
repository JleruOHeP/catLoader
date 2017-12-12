using System;
using Microsoft.Extensions.Configuration;

namespace CatLoader.Configuration
{
    public interface IPersonSourceConfig
    {
        string PersonSourceUrl { get; }
    }

    public class PersonSourceConfig: IPersonSourceConfig
    {
        private const string PeopleSourceUrlKey = "PeopleSourceUrl";
        public string PersonSourceUrl 
        {
            get
            {
                var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
                
                return config[PeopleSourceUrlKey];
            }
        }
    }
}