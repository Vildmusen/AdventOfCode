using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day4
    {
        
        public void Start()
        {
            int count = 0;

            for (int i = 264360; i < 746325; i++)
            {
                if (NeverDecreasing(i) && LegitPairExists(i))
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        private bool NeverDecreasing(int number)
        {
            int digit1 = 0;
            int digit2 = 0;

            for (int i = 1; i < 6; i++)
            {
                digit1 = number % 10;
                number /= 10;
                digit2 = number % 10;

                if (digit1 < digit2)
                {
                    return false;
                }
            }
            
            return true;   
        }

        private bool LegitPairExists(int number)
        {
            List<int> ints = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                ints.Add(number % 10);
                number /= 10;
            }

            List<int> sizes = new List<int>();

            for (int i = 0; i < ints.Count; i++)
            {
                int count = 1;
                int temp = ints[i];
                while(i + count < ints.Count && temp == ints[i + count])
                {
                    count++;
                }
                i += count - 1;
                sizes.Add(count);
            }
            
            return sizes.Contains(2);
        }
    }
}
