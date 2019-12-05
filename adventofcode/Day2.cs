using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day2
    {
        int noun;
        int verb;
        string[] input;
        int[] ints;
        int pointer;
        bool running = true;

        public void Start()
        {
            input = Utils.ReadFromFile("2");
            Reset();
            CalculateValues();
        }

        private void CalculateValues()
        {
            for (noun = 0; noun < 100; noun++)
            {
                for (verb = 0; verb < 100; verb++)
                {
                    Replace(1, noun);
                    Replace(2, verb);
                    Run();
                    if(ints[0] == 19690720)
                    {
                        Console.WriteLine("Hej hopp snopp {0} {1} {2}", noun, verb, 100 * noun + verb);
                    }
                    Reset();
                }
            }
        }

        private void Run()
        {
            pointer = 0;
            running = true;
            while (running)
            {
                switch (ints[pointer])
                {
                    case 1:
                        Add();
                        pointer += 4;
                        break;
                    case 2:
                        Multiply();
                        pointer += 4;
                        break;
                    case 99:
                        running = false;
                        break;
                }
            }
        }

        private void Input()
        {
            throw new NotImplementedException();
        }
      
        private void Add()
        {
            ints[ints[pointer + 3]] = ints[ints[pointer + 2]] + ints[ints[pointer + 1]];
        }

        private void Multiply()
        {
            ints[ints[pointer + 3]] = ints[ints[pointer + 2]] * ints[ints[pointer + 1]];
        }

        private void Reset()
        {
            ints = Utils.StringToIntList(input);
        }

        private void Replace(int i, int value)
        {
            ints[i] = value;
        }

    }
}
