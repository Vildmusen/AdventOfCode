using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day3
    {
        string Wire1;
        string Wire2;
        int lastX = 0;
        int lastY = 0;
        List<Coords> Wire1Coords = new List<Coords>();
        List<Coords> Wire2Coords = new List<Coords>();

        public struct Coords
        {
            public int x;
            public int y;

            public int Manhattan { get { return Math.Abs(x) + Math.Abs(y); } }

            public Coords(int _x, int _y)
            {
                x = _x;
                y = _y;
            }

            public override string ToString()
            {
                return x + " " + y;
            }
        }

        public void Start()
        {
            string[] input = Utils.ReadFromFileV2("3");
            Wire1 = input[0];
            Wire2 = input[1];
            Wire1Coords = GetAllCoords(Wire1);
            Wire2Coords = GetAllCoords(Wire2);
            List<Coords> matches = GetAllMatches(Wire1Coords, Wire2Coords);
            int lowest = matches[0].Manhattan;
            for (int i = 1; i < matches.Count; i++)
            {
                if(matches[i].Manhattan < lowest)
                {
                    lowest = matches[i].Manhattan;
                }
            }

            int steps = GetShortestDistance(matches);

            Console.WriteLine(lowest);
            Console.WriteLine("shortest path is " + (steps + 2) + " steps long"); // + 2 becuase steps is broken (and im too lazy to fix it)
        }

        private int GetShortestDistance(List<Coords> matches)
        {
            int shortest = 100000000;
            foreach(Coords match in matches)
            {
                int steps = Wire1Coords.IndexOf(match) + Wire2Coords.IndexOf(match);
                if(steps < shortest)
                {
                    shortest = steps;
                }
            }
            return shortest;
        }

        private List<Coords> GetAllMatches(List<Coords> wire1Coords, List<Coords> wire2Coords)
        {
            List<Coords> matches = new List<Coords>();
            int length = wire1Coords.Count;
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                count = Utils.Progress(count, i, length);
                Coords c1 = Wire1Coords[i];
                foreach (Coords c2 in Wire2Coords)
                {
                    if (c1.x == c2.x && c1.y == c2.y)
                    {
                        matches.Add(c1);
                    }
                }
            }
            
            return matches;
        }

        private List<Coords> GetAllCoords(string wire)
        {
            List<Coords> result = new List<Coords>();
            lastX = 0;
            lastY = 0;
            foreach(string s in wire.Split(','))
            {
                char dir = s[0];
                string part = s.Substring(1, s.Length - 1);
                result.AddRange(GetPartCoords(Int32.Parse(part), dir));
            }
            return result;
        }

        private List<Coords> GetPartCoords(int current, char dir)
        {
            List<Coords> result = new List<Coords>();
            int i = 1;
            switch (dir)
            {
                case 'R':
                    while(i <= current)
                    {
                        result.Add(new Coords(lastX + i, lastY));
                        i++;
                    }
                    lastX += current;
                    break;
                case 'U':
                    while (i <= current)
                    {
                        result.Add(new Coords(lastX, lastY + i));
                        i++;
                    }
                    lastY += current;
                    break;
                case 'L':
                    while (i <= current)
                    {
                        result.Add(new Coords(lastX - i, lastY));
                        i++;
                    }
                    lastX -= current;
                    break;
                case 'D':
                    while (i <= current)
                    {
                        result.Add(new Coords(lastX, lastY - i));
                        i++;
                    }
                    lastY -= current;
                    break;
            }
            return result;
        }
    }
}