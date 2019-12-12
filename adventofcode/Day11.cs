using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day11
    {
        public struct Panel
        {
            public int x;
            public int y;
            public bool paint;

            public override string ToString()
            {
                return "(" + x + ", " + y + ")";
            }

        }

        int RobotX = 0;
        int RobotY = 0;
        int dir = 0;
        char[][] registration = new char[6][];
        List<Panel> History = new List<Panel>();
        public void Start()
        {
            Computer brain = new Computer();
            brain.Start("11");
            long[] input = new long[] { 1 };
            for (int i = 0; i < registration.Length; i++)
            {
                registration[i] = new char[50];
                for (int j = 0; j < 50; j++)
                {
                    registration[i][j] = ' ';
                }
            }

            while(brain.exitCode != 99)
            {
                brain.Run(input);
                Panel current = new Panel { x = RobotX, y = RobotY, paint = brain.GetOutPutAt(1) == 1 };
                UpdateHistory(current);
                MakeItPretty();
                switch (brain.GetOutPutAt(0))
                {
                    case 0:
                        dir = dir - 1 >= 0 ? dir - 1 : 3;
                        break;
                    case 1:
                        dir = dir + 1 <= 3 ? dir + 1 : 0;
                        break;
                }
                switch (dir)
                {
                    case 0:
                        RobotX += 1;
                        break;
                    case 1:
                        RobotY += 1;
                        break;
                    case 2:
                        RobotX -= 1;
                        break;
                    case 3:
                        RobotY -= 1;
                        break;
                }
                input = GetCurrentPanel();
                System.Threading.Thread.Sleep(40);
            }
        }

        private void MakeItPretty()
        {
            Console.Clear();
            bool robotDrawn = false;
            for (int i = 0; i < registration.Length; i++)
            {
                for (int j = 0; j < 43; j++)
                {
                    foreach(Panel p in History)
                    {
                        if(Math.Abs(p.x) == i && Math.Abs(p.y) == j && p.paint)
                        {
                            registration[i][j] = '#';
                        }
                        else if (Math.Abs(p.x) == i && Math.Abs(p.y) == j && p.x == RobotX && p.y == RobotY)
                        {
                            Console.Write('@');
                            robotDrawn = true;
                        } 
                    }
                    if(!robotDrawn) Console.Write(registration[i][j]);
                }
                robotDrawn = false;
                Console.WriteLine();
            }
        }

        private void UpdateHistory(Panel current)
        {
            bool updated = false;
            for (int i = 0; i < History.Count; i++)
            {
                if (History[i].x == current.x && History[i].y == current.y)
                {
                    History[i] = current;
                    updated = true;
                }
            }
            if (!updated)
            {
                History.Add(current);
            }
        }

        private long[] GetCurrentPanel()
        {
            foreach (Panel p in History)
            {
                if (p.x == RobotX && p.y == RobotY)
                {
                    return p.paint ? new long[] { 1 } : new long[] { 0 };
                }
            }
            return new long[] { 0 };
        }
    }
}
