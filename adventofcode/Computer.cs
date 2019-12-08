using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Computer
    {
        string[] file;
        int[] IntCode;
        int pointer;
        int[] inputs;
        int output;
        bool running = true;

        public void Start(string day)
        {
            file = Utils.ReadFromFile(day);
        }
        public void Reset()
        {
            IntCode = Utils.StringToIntList(file);
        }

        public int ReadOutput()
        {
            return output;
        }

        public int ReadValueAt(int address)
        {
            return IntCode[address];
        }

        public void Run(int[] inputs)
        {
            this.inputs = inputs;
            Run();
        }
        public void Run()
        {
            pointer = 0;
            running = true;
            while (running)
            {
                int opcode;
                int[] parameters;
                int cur = IntCode[pointer];
                if(cur > 100)
                {
                    opcode = IntCode[pointer] % 100;
                    parameters = new int[] { (IntCode[pointer] / 100) % 10, (IntCode[pointer] / 1000) % 10, (IntCode[pointer] / 10000) % 10};
                } else
                {
                    opcode = cur;
                    parameters = new int[] { 0,0,0 };
                }
                
                //Console.WriteLine("System: [OPCODE]:{0} [ADRESS]:{1}", opcode, pointer);
                switch (opcode)
                {
                    case 1:
                        Add(parameters);
                        pointer += 4;
                        break;
                    case 2:
                        Multiply(parameters);
                        pointer += 4;
                        break;
                    case 3:
                        Input();
                        pointer += 2;
                        break;
                    case 4:
                        Output(parameters);
                        pointer += 2;
                        break;
                    case 5:
                        JumpIfTrue(parameters);
                        pointer += 3;
                        break;
                    case 6:
                        JumpIfFalse(parameters);
                        pointer += 3;
                        break;
                    case 7:
                        LessThan(parameters);
                        pointer += 4;
                        break;
                    case 8:
                        Equals(parameters);
                        pointer += 4;
                        break;
                    case 99:
                        running = false;
                        break;
                    default:
                        running = false;
                        Console.WriteLine("System: [ERROR]: Unknown opcode, exiting");
                        break;
                }
            }
        }

        private void Equals(int[] parameters)
        {
            int[] values = GetValues(parameters);
            IntCode[values[2]] = IntCode[values[0]] == IntCode[values[1]] ? 1 : 0;
        }

        private void LessThan(int[] parameters)
        {
            int[] values = GetValues(parameters);
            IntCode[values[2]] = IntCode[values[0]] < IntCode[values[1]] ? 1 : 0;
        }

        private void JumpIfTrue(int[] parameters)
        {
            int[] values = GetValues(parameters);
            pointer = IntCode[values[0]] != 0 ? IntCode[values[1]] - 3 : pointer;
        }

        private void JumpIfFalse(int[] parameters)
        {
            int[] values = GetValues(parameters);
            pointer = IntCode[values[0]] == 0 ? IntCode[values[1]] - 3 : pointer;
        }

        private void Output(int[] parameters)
        {
            int[] values = GetValues(parameters);
            output = values[0];
            //Console.WriteLine("System: [OUTPUT]:" + IntCode[values[0]]);
        }

        private void Input()
        {
            int input;
            if (inputs.Length > 0)
            {
                input = inputs[0];
                inputs = inputs.Skip(1).ToArray();
            } else
            {
                Console.Write("SYSTEM: [INPUT]: ");
                input = Int32.Parse(Console.ReadLine());
            }
            IntCode[IntCode[pointer + 1]] = input;
        }

        private void Add(int[] parameters)
        {
            int[] values = GetValues(parameters);
            IntCode[values[2]] = IntCode[values[1]] + IntCode[values[0]];
        }

        private void Multiply(int[] parameters)
        {
            int[] values = GetValues(parameters);
            IntCode[values[2]] = IntCode[values[1]] * IntCode[values[0]];
        }

        public int[] GetValues(int[] parameters)
        {
            int[] values = new int[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                values[i] = parameters[i] == 0 ? IntCode[pointer + i + 1] : pointer + i + 1;
            }
            return values;
        }

    }
}
