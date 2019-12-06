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
        int totalsteps = 0;
        List<string> nodes;
        List<Orbit> orbits;

        public struct Orbit
        {
            public string name;
            public List<Orbit> friends;

            public Orbit(string name)
            {
                this.name = name;
                friends = new List<Orbit>();
            }

            public override string ToString()
            {
                string orbits = "";
                foreach(Orbit o in friends)
                {
                    orbits += o.name;
                }
                return name + " [ " + orbits + " ]";
            }
        }

        public void Start()
        {
            input = Utils.ReadFromFileV2("6");
            nodes = new List<string>();
            nodes.AddRange(input);
            orbits = new List<Orbit>();
            BuildOrbits();
            string start = "YOU";
            string end = "SAN";
            int result = CalculateSmallestDistance(start, end);
            Console.WriteLine("The minimum steps required to travel from " + start + " to " + end +" is: " + result);
            foreach (Orbit o in orbits)
            {
                totalsteps++;
                CountSteps(o);
            }
            Console.WriteLine("Totla number of steps in our tree is: " + totalsteps);
        }

        /// <summary>
        /// Calulates the minimun steps required to travel from node v1 -> v2.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private int CalculateSmallestDistance(string v1, string v2)
        {
            Orbit Santastart = GetOrbitOnName(v1);
            Orbit Playerstart = GetOrbitOnName(v2);
            
            List<Orbit> SantasPath = new List<Orbit>();
            List<Orbit> PlayerPath = new List<Orbit>();

            SantasPath = GeneratePath(Santastart);
            PlayerPath = GeneratePath(Playerstart);

            int path1 = SantasPath.Count;
            int path2 = PlayerPath.Count;
            
            for (int i = 1; i < SantasPath.Count; i++)
            {
                for (int j = 1; j < PlayerPath.Count; j++)
                {
                    if (SantasPath[i].Equals(PlayerPath[j]))
                    {
                        return i + j - 2;
                    }
                }
            }
            return -1;
        }

        private List<Orbit> GeneratePath(Orbit start)
        {
            List<Orbit> nextSteps = new List<Orbit>();
            List<Orbit> path = new List<Orbit>();
            
            nextSteps.Add(start);

            while (nextSteps.Count > 0)
            {
                path.AddRange(nextSteps);
                foreach (Orbit step in nextSteps)
                {
                    nextSteps = GetNextSteps(step);
                }
            }

            return path;
        }

        private List<Orbit> GetNextSteps(Orbit current)
        {
            List<Orbit> nextSteps = new List<Orbit>();
            foreach (Orbit o in orbits)
            {
                if (IsConnection(o, current))
                {
                    nextSteps.Add(o);
                }
            }
            return nextSteps;
        }

        private Orbit GetOrbitOnName(string v1)
        {
            foreach(Orbit o in orbits)
            {
                if (o.friends[0].name == v1)
                {
                    return o;
                }
            }
            return new Orbit();
        }

        private void CountSteps(Orbit o)
        {
            for (int i = 0; i < orbits.Count; i++)
            {
                if (IsConnection(o, orbits[i]))
                {
                    totalsteps++;
                    CountSteps(orbits[i]);
                }
            }
        }

        private bool IsConnection(Orbit o1, Orbit o2)
        {
            return o1.friends[0].name.Equals(o2.name);
        }

        private void BuildOrbits()
        {
            string[] current;
            for (int i = 0; i < nodes.Count; i++)
            {
                current = GetParts(i);
                Orbit o = new Orbit(current[0].Trim('\r'));
                o.friends.Add(new Orbit(current[1].Trim('\r')));
                orbits.Add(o);
            }
        }
        
        private string[] GetParts(int i)
        {
            return nodes[i].Split(')');
        }
    }
}
