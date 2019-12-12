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
        int sizeX = 36;
        int sizeY = 36;
        List<Coordinat> AsteroidCoords = new List<Coordinat>();
        Coordinat best;
        List<double> angles = new List<double>();
        List<double> BestAngles = new List<double>();


        public struct Coordinat
        {
            public int x;
            public int y;
            public int AsteroidsInSight;
            public double Angle;

            public override string ToString()
            {
                return "(" + x + ", " + y + ") - " + Angle;
            }

        }

        public void Start()
        {
            input = Utils.ReadFromFile("10");

            getValues();
            GetAsteroidCoordinates();
            best = CalculateBestPosition();
            Console.WriteLine(best);

            Console.WriteLine(DestroyAsteroids());
        }

        private Coordinat DestroyAsteroids()
        {
            int count = 0;
            double currentAngle = 0;
            Coordinat current = new Coordinat();
            List<int> ToDestroy = new List<int>();
            for (int i = 0; i < AsteroidCoords.Count; i++)
            {
                AsteroidCoords[i] = new Coordinat() { x = AsteroidCoords[i].x, y = AsteroidCoords[i].y, Angle = BestAngles[i] };
            }
            AsteroidCoords = AsteroidCoords.OrderByDescending(x => x.Angle).ToList();
            while(AsteroidCoords.Count > 0)
            {
                for (int i = 0; i < AsteroidCoords.Count; i++)
                {
                    if (AsteroidCoords[i].Angle == currentAngle)
                    {
                        ToDestroy.Add(i);
                    }
                }
                if(ToDestroy.Count > 0)
                {
                    count++;
                    int index = FindClosest(ToDestroy);
                    ToDestroy.Clear();
                    current = AsteroidCoords[index];
                    Console.WriteLine("[" + count + "] ima bout to blow up " + current + " at angle " + currentAngle);
                    AsteroidCoords.RemoveAt(index);
                }
                currentAngle = currentAngle == -180 ? 180 : GetNextAngle(currentAngle);
            }
            return current;
        }

        private double GetNextAngle(double current)
        {
            foreach(Coordinat c in AsteroidCoords)
            {
                if(c.Angle < current)
                {
                    return c.Angle;
                }
            }
            return -180;
        }

        private int FindClosest(List<int> toDestroy)
        {
            int closestIndex = 0;
            int curClosestDistance = int.MaxValue;
            foreach(int i in toDestroy)
            {
                Coordinat c = AsteroidCoords[i];
                int distance = Math.Abs(c.x - best.x) + Math.Abs(c.y - best.y);
                if (distance < curClosestDistance && distance > 0)
                {
                    curClosestDistance = distance;
                    closestIndex = i;
                }
            }
            return closestIndex;
        }

        private Coordinat CalculateBestPosition()
        {
            int best = 0;
            Coordinat bestCoord = new Coordinat { x = 0, y = 0 };
            int temp;
            for (int i = 0; i < AsteroidCoords.Count; i++)
            {
                temp = CalculalteAsteroidsInSight(AsteroidCoords[i]);
                AsteroidCoords[i] = new Coordinat() { x = AsteroidCoords[i].x, y = AsteroidCoords[i].y, AsteroidsInSight = temp };
                if (temp >= best)
                {
                    best = temp;
                    bestCoord = AsteroidCoords[i];
                    BestAngles = angles;
                }
            }
            return bestCoord;
        }

        private int CalculalteAsteroidsInSight(Coordinat current)
        {
            angles = new List<double>();
            foreach(Coordinat c in AsteroidCoords)
            {
                int deltaX = current.x - c.x;
                int deltaY = current.y - c.y;
                angles.Add(Math.Atan2(deltaX, deltaY) * 180 / Math.PI);
            }
            return angles.Distinct().Count();
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
                        AsteroidCoords.Add(new Coordinat() { x = j, y = i });
                    }
                }
            }
        }
    }
}
