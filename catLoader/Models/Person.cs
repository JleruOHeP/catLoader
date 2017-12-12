using System;
using System.Collections.Generic;

namespace CatLoader.Models
{    
    public class Person
    {
        public string Name {get; set;}
        public Gender Gender {get; set;}
        public int Age {get; set;}
        public ICollection<Pet> Pets {get; set;}
    }
}