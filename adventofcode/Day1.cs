using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day1
    {
        string[] input;
        int[] ints;

        public void Start()
        {
            input = Utils.ReadFromFileV2("1");
            ints = Utils.StringToIntList(input);
            int result = CalculateFuel(ints);
            Console.WriteLine(result);
        }

        private int CalculateFuel(int[] ints)
        {
            int result = 0;
            for (int i = 0; i < ints.Length; i++)
            {
                int temp = ints[i];
                int sum = 0;
                while(temp > 0)
                {
                    temp /= 3;
                    temp -= 2;
                    if(temp < 0)
                    {
                        break;
                    }
                    sum += temp;
                }
                result += sum;
            }
            return result;
        }
    }
}
