using System;
using System.Collections.Generic;
using System.Linq;

namespace Pirates
{
    class Program
    {
        static void Main(string[] args)            
        {
            Dictionary<string, int> peopleByCity = new Dictionary<string, int>(); // key City, value peopleCount
            Dictionary<string, int> goldByCity = new Dictionary<string , int>(); // key city , value Gold

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "Sail")
                {
                    //ToDo continue to the next while loop
                    break;
                }

                string[] parts = line.Split("||", StringSplitOptions.RemoveEmptyEntries);
                string nameCity = parts[0];
                int peopleCount = int.Parse(parts[1]);
                int goldKgs = int.Parse(parts[2]);

                if (!peopleByCity.ContainsKey(nameCity))
                {
                    peopleByCity.Add(nameCity, peopleCount);
                    goldByCity.Add(nameCity, goldKgs);
                    continue;
                }

                peopleByCity[nameCity] += peopleCount;
                goldByCity[nameCity] += goldKgs;
            }

            while (true)
            {
                string newLine = Console.ReadLine();

                if (newLine == "End")
                {
                    //ToSo
                    if (peopleByCity.Count > 0)
                    {
                        Dictionary<string, int> sortedGoldByCity = goldByCity
                            .OrderByDescending(x => x.Value)
                            .ThenBy(x => x.Key)
                            .ToDictionary(x => x.Key, x => x.Value);

                        Console.WriteLine($"Ahoy, Captain! There are {sortedGoldByCity.Count} wealthy settlements to go to:");

                        foreach (var city in sortedGoldByCity)
                        {
                            string currCity = city.Key;
                            int currPeople = peopleByCity[currCity];
                            int currGold = city.Value;
                           
                            Console.WriteLine($"{currCity} -> Population: {currPeople} citizens, Gold: {currGold} kg");
                        }
                    }
                    else // if (peopleByCity.Count <= 0)
                    {
                        Console.WriteLine($"Ahoy, Captain! All targets have been plundered and destroyed!");
                    }

                    break;
                }

                string[] comandsParts = newLine.Split("=>", StringSplitOptions.RemoveEmptyEntries);
                string command = comandsParts[0];
                string nameCity = comandsParts[1];                

                if (command == "Plunder")
                {                   
                    int peopleCount = int.Parse(comandsParts[2]);  // now reduce poepleCount
                                                                   // now reduce goldKgs
                    int goldKgs = int.Parse(comandsParts[3]);

                    Console.WriteLine($"{nameCity} plundered! {goldKgs} gold stolen, {peopleCount} citizens killed.");

                    peopleByCity[nameCity] -= peopleCount;
                    goldByCity[nameCity] -= goldKgs;

                    if ( (peopleByCity[nameCity] <= 0) || (goldByCity[nameCity] <= 0))
                    {
                        Console.WriteLine($"{nameCity} has been wiped off the map!");
                        peopleByCity.Remove(nameCity);
                        goldByCity.Remove(nameCity); //  Check this ? do i have to remove the Gold ?from this dictionary
                    }
                }
                else //if (command == "Prosper")
                {
                    int goldKgs = int.Parse(comandsParts[2]);

                    if (goldKgs < 0)
                    {
                        Console.WriteLine($"Gold added cannot be a negative number!");
                        continue;
                    }
                    //gold >=0
                    goldByCity[nameCity] += goldKgs;                  

                    Console.WriteLine($"{goldKgs} gold added to the city treasury. {nameCity} now has {goldByCity[nameCity]} gold.");
                }
            }
        }
    }
}
