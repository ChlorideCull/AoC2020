using System;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Given a list of integers, find the pair that sums to 2020, and multiply them.
            Bonus: Given a list of integers, find the three that sums to 2020, and multiply them.
            */
            var inputInts = File.ReadAllLines(args[0]).Select(x => Convert.ToInt32(x)).ToList();
            for (var i = 0; i < inputInts.Count; i++)
            {
                for (var j = i + 1; j < inputInts.Count; j++)
                {
                    if (inputInts[i] + inputInts[j] == 2020)
                        Console.WriteLine($"Sum of pairs: {inputInts[i] * inputInts[j]}");
                    
                    for (var k = j + 1; k < inputInts.Count; k++)
                    {
                        if (inputInts[i] + inputInts[j] + inputInts[k] == 2020)
                            Console.WriteLine($"Sum of triplet: {inputInts[i] * inputInts[j] * inputInts[k]}");
                    }
                }
            }
        }
    }
}