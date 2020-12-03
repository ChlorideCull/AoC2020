using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class PasswordSet
    {
        public char CharacterRequirement { get; set; }
        public int CharacterOccurrencesMin { get; set; }
        public int CharacterOccurrencesMax { get; set; }
        public string Password { get; set; }

        public bool Validate()
        {
            var occurrences = Password.Count(x => x == CharacterRequirement);
            return occurrences >= CharacterOccurrencesMin && occurrences <= CharacterOccurrencesMax;
        }

        public bool ValidateBonus()
        {
            return 
                (Password[CharacterOccurrencesMin-1] == CharacterRequirement && Password[CharacterOccurrencesMax-1] != CharacterRequirement) ||
                (Password[CharacterOccurrencesMin-1] != CharacterRequirement && Password[CharacterOccurrencesMax-1] == CharacterRequirement);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Given a list of list of passwords and password policies, return the number of valid accounts.
            */
            var inputLines = File.ReadAllLines(args[0]).Select(line =>
            {
                var split = line.Split(' ');
                var occurenceLimits = split[0].Split('-').Select(x => Convert.ToInt32(x)).ToArray();
                var targetChar = split[1][0];
                var password = split[2];
                
                return new PasswordSet
                {
                    Password = password,
                    CharacterOccurrencesMax = occurenceLimits[1],
                    CharacterOccurrencesMin = occurenceLimits[0],
                    CharacterRequirement = targetChar
                };
            }).ToList();
            Console.WriteLine(inputLines.Count(x => x.Validate()));
            Console.WriteLine(inputLines.Count(x => x.ValidateBonus()));
        }
    }
}