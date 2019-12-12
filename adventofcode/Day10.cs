using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day10
    {
        string[] input;
        string values;
        int sizeX = 10;
        int sizeY = 10;
        List<Coordinat> AsteroidCoords = new List<Coordinat>();
        
        public struct Coordinat
        {
            public int x;
            public int y;

            public override string ToString()
            {
                return "(" + x + ", " + y + ")";
            }
        }

        public void Start()
        {
            input = Utils.ReadFromFile("10");

            getValues();
            GetAsteroidCoordinates();
            CalculateBestPosition();

            foreach(Coordinat c in AsteroidCoords)
            {
                Console.WriteLine(c);
            }
        }

        private Coordinat CalculateBestPosition()
        {
            int best = 0;
            Coordinat bestCoord = new Coordinat { x = 0, y = 0 };
            int temp = 0;
            foreach(Coordinat c in AsteroidCoords)
            {
                temp = CalculalteAsteroidsInSight(c);
                if(temp > best)
                {
                    best = temp;
                    bestCoord = c;
                }
            }
            return bestCoord;
        }

        private int CalculalteAsteroidsInSight(Coordinat current)
        {
            int count = 0;
            foreach(Coordinat c in AsteroidCoords)
            {
                int deltaX = current.x - c.x;
                int deltaY = current.y - c.y;
                while(deltaX != 0 && deltaY != 0)
                {
                    
                }
            }
            return count;
        }

        private void getValues()
        {
            string[] temp = input[0].Split(new char[] { '\n', '\r' });
            values = "";
            foreach (string s in temp)
            {
                if (s.Length == sizeX)
                {
                    values += s;
                }
            }
        }

        private void GetAsteroidCoordinates()
        {
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if(values[j + (i * sizeY)] == '#')
                    {
                        AsteroidCoords.Add(new Coordinat() { x = i, y = j });
                    }
                }
            }
        }
    }
}
