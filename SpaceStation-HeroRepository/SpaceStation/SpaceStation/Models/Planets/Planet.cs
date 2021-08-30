using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;

namespace SpaceStation.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        public Planet(string name)
        {
            Name = name;
            Items = new List<string>();
        }
        public ICollection<string> Items { get; set; }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }
    }
}
