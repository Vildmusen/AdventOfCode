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

        long[] IntCode;
        long output;
        long[] outputHistory = new long[0];
        long[] inputs;
        
        int pointer;
        int relativeBase = 0;
        int memSpace = 2000000;
        
        public int name;
        public int exitCode;
        bool running = true;

        public void Start(string day)
        {
            file = Utils.ReadFromFile(day);
        }
        public void Reset()
        {
            IntCode = Utils.StringToLongList(file);
            int mem = memSpace - IntCode.Length;
            List<long> temp = IntCode.ToList();
            while (mem > 0)
            {
                temp.Add(0);
                mem--;
            }
            IntCode = temp.ToArray();
            pointer = 0;
        }

        public long ReadOutput()
        {
            return output;
        }

        public void PrintOutPut()
        {
            foreach(long l in outputHistory)
            {
                Console.Write(l + ": ");
            }
        }

        public long ReadValueAt(int address)
        {
            return IntCode[address];
        }

        public void Run(long[] inputs)
        {
            this.inputs = inputs;
            Run();
        }
        public void Run()
        {
            running = true;
            exitCode = 0;
            while (running)
            {
                int opcode;
                int[] parameters;
                int cur = (int) IntCode[pointer];
                if(cur > 100)
                {
                    opcode = (int) IntCode[pointer] % 100;
                    parameters = new int[] { (int)IntCode[pointer] / 100 % 10, (int)IntCode[pointer] / 1000 % 10, (int)IntCode[pointer] / 10000 % 10};
                } else
                {
                    opcode = cur;
                    parameters = new int[] { 0,0,0 };
                }

                //Console.WriteLine("System[" + name + "]: [OPCODE]:{0} [ADRESS]:{1} [PARAMS]:{2} {3} {4}", opcode, pointer, parameters[0], parameters[1], parameters[2]);
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
                        Input(parameters);
                        if(exitCode == 3) { return; }
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
                    case 9:
                        AdjustRelativeBase(parameters);
                        pointer += 2;
                        break;
                    case 99:
                        exitCode = 99;
                        running = false;
                        break;
                    default:
                        running = false;
                        Console.WriteLine("System: [ERROR]: Unknown opcode, exiting");
                        break;
                }
            }
        }

        private void AdjustRelativeBase(int[] parameters)
        {
            int[] values = GetValues(parameters);
            relativeBase += (int) IntCode[values[0]];
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
            pointer = IntCode[values[0]] != 0 ? (int) IntCode[values[1]] - 3 : pointer;
        }

        private void JumpIfFalse(int[] parameters)
        {
            int[] values = GetValues(parameters);
            pointer = IntCode[values[0]] == 0 ? (int) IntCode[values[1]] - 3 : pointer;
        }

        private void Output(int[] parameters)
        {
            int[] values = GetValues(parameters);
            output = IntCode[values[0]];
            AddHistory(output);
        }

        private void AddHistory(long output)
        {
            List<long> list = outputHistory.ToList();
            list.Add(output);
            outputHistory = list.ToArray();
        }

        private void Input(int[] parameters)
        {
            int[] values = GetValues(parameters);
            long input;
            if (inputs.Length > 0)
            {
                input = inputs[0];
                inputs = inputs.Skip(1).ToArray();
                IntCode[values[0]] = input;
            } else
            {
                exitCode = 3;
            }
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
                try
                {
                    switch (parameters[i])
                    {
                        case 0:
                            values[i] = (int) IntCode[pointer + i + 1];
                            break;
                        case 1:
                            values[i] = pointer + i + 1;
                            break;
                        case 2:
                            values[i] = relativeBase + (int) IntCode[pointer + i + 1];
                            break;
                    }
                } catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Value was not inside the program code. [INDEX]: {0}", pointer + i + 1);
                }
            }
            return values;
        }

    }
}
