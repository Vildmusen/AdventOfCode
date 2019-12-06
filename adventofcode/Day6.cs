using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day6
    {
        string[] input;
        int steps = 0;
        List<string> nodes;
        List<Orbit> orbits;

        public struct Orbit
        {
            public string name;
            public string friend;

            public Orbit(string name, string friend)
            {
                this.name = name;
                this.friend = friend;
            }

            public override string ToString()
            {
                return name + " ) " + friend;
            }
        }

        public void Start()
        {
            input = Utils.ReadFromFileV2("6");
            //input = File.ReadAllText("C:\\Users\\Vildmusen\\source\\repos\\adventofcode\\resources\\day6test.txt").Split('\n');
            nodes = new List<string>();
            nodes.AddRange(input);
            orbits = new List<Orbit>();
            BuildOrbits();
            foreach (Orbit o in orbits)
            {
                steps++;
                CountSteps(o);
            }
            Console.WriteLine(steps);
        }
        
        private void CountSteps(Orbit o)
        {
            for (int i = 0; i < orbits.Count; i++)
            {
                if(o.friend.Equals(orbits[i].name))
                {
                    steps++;
                    CountSteps(orbits[i]);
                }
            }
        }

        private void BuildOrbits()
        {
            string[] current;
            for (int i = 0; i < nodes.Count; i++)
            {
                current = GetParts(i);
                orbits.Add(new Orbit(current[0].Trim('\r'), current[1].Trim('\r')));
            }
        }

        private string[] GetParts(int i)
        {
            return nodes[i].Split(')');
        }
    }
}
