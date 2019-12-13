using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day12
    {
        List<Moon> moons = new List<Moon>();
        Moon start1 = new Moon(-1, 0, 2);/* new Moon(4, 1, 1); */
        Moon start2 = new Moon(2, -10, -7); /* new Moon(11, -18, -1);*/
        Moon start3 = new Moon(4, -8, 8); /* new Moon(-2, -10, -4); */
        Moon start4 = new Moon(3, 5, -1); /*  new Moon(-7, -2, 14); */

        List<long[]> XHistory = new List<long[]>();
        List<long[]> YHistory = new List<long[]>();
        List<long[]> ZHistory = new List<long[]>();

        bool XDone = false;
        bool YDone = false;
        bool ZDone = false;

        int Xlength = 0;
        int Ylength = 0;
        int Zlength = 0;

        public void Start()
        {
            moons.Add(start1);
            moons.Add(start2);
            moons.Add(start3);
            moons.Add(start4);

            long count = 0;
            
            while (count < 2 || !PatternFound()) {
                count++;
                //totalEnergy = 0;
                for (int j = 0; j < moons.Count; j++)
                {
                    for (int k = 0; k < moons.Count; k++)
                    {
                        if(k != j)
                        {
                            if (moons[j].x > moons[k].x)
                            {
                                moons[j].ApplyGravityX(-1);
                            }
                            else if (moons[j].x < moons[k].x)
                            {
                                moons[j].ApplyGravityX(1);
                            }
                            if (moons[j].y > moons[k].y)
                            {
                                moons[j].ApplyGravityY(-1);
                            }
                            else if (moons[j].y < moons[k].y)
                            {
                                moons[j].ApplyGravityY(1);
                            }
                            if (moons[j].z > moons[k].z)
                            {
                                moons[j].ApplyGravityZ(-1);
                            }
                            else if (moons[j].z < moons[k].z)
                            {
                                moons[j].ApplyGravityZ(1);
                            }
                        }
                    }
                }
                foreach (Moon m in moons)
                {
                    m.UpdatePos();
                    //totalEnergy += m.GetTotalEnergy();
                }
            }
            Console.WriteLine(Xlength + " " + Ylength + " " + Zlength);
            List<int> patternLengths = new List<int>();
            patternLengths.Add(Xlength);
            patternLengths.Add(Ylength);
            patternLengths.Add(Zlength);
            Console.WriteLine(FindCommonMultiple(patternLengths));
        }

        private int FindCommonMultiple(List<int> patternLengths)
        {
            patternLengths.Sort();
            int temp = FindCommonMultiple(patternLengths[0], patternLengths[1]);
            temp = FindCommonMultiple(patternLengths[2], temp);
            return temp;
        }

        private int FindCommonMultiple(int a, int b)
        {
            for (int i = 2; i <= b; i++)
            {
                if(a * i % b == 0)
                {
                    return a * i;
                }
            }
            return -1;
        }

        private bool PatternFound()
        {
            return TestX() && TestY() && TestZ();
        }

        private bool TestZ()
        {
            if (ZDone) return true;
            else
            {
                long[] cur = new long[] { moons[0].z, moons[1].z, moons[2].z, moons[3].z };
                ZHistory.Add(cur);
                if (moons[0].velZ == 0 && moons[1].velZ == 0 && moons[2].velZ == 0 && moons[3].velZ == 0)
                {
                    ZDone = true;
                    Zlength = ZHistory.Count();
                    return true;
                }
            }
            return false;
        }

        private bool TestY()
        {
            if (YDone) return true;
            else
            {
                long[] cur = new long[] { moons[0].y, moons[1].y, moons[2].y, moons[3].y };
                YHistory.Add(cur);
                if (moons[0].velY == 0 && moons[1].velY == 0 && moons[2].velY == 0 && moons[3].velY == 0)
                {
                    YDone = true;
                    Ylength = YHistory.Count();
                    return true;
                }
            }
            return false;
        }

        private bool TestX()
        {
            if (XDone) return true;
            else
            {
                long[] cur = new long[] { moons[0].x, moons[1].x, moons[2].x, moons[3].x };
                XHistory.Add(cur);
                if(moons[0].velX == 0 && moons[1].velX == 0 && moons[2].velX == 0 && moons[3].velX == 0)
                {
                    XDone = true;
                    Xlength = XHistory.Count();
                    return true;
                }
            }
            return false;
        }
    }
}
