using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Computer
    {
        public long Output { get; set; }
        public int Name { get; set; }
        private long[] IntCode;
        private long[] OutputHistory = new long[0];
        private long[] Inputs;
        private int pointer;
        private int relativeBase = 0;
        private int memSpace = 2000000;
        private int exitCode;
        public void Start(string day)
        {
            Reset(day);
        }
        public void Reset(string day)
        {
            IntCode = Utils.StringToLongList(Utils.ReadFromFile(day));
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
        public void PrintOutPut()
        {
            foreach(long l in OutputHistory) { Console.Write(l + ": "); }
        }
        public long ReadValueAt(int address)
        {
            return IntCode[address];
        }
        public void Run(long[] inputs)
        {
            Inputs = inputs;
            Run();
        }
        private void Run()
        {
            bool running = true;
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
                int[] values = GetValues(parameters);
                //Console.WriteLine("System[" + name + "]: [OPCODE]:{0} [ADRESS]:{1} [PARAMS]:{2} {3} {4}", opcode, pointer, parameters[0], parameters[1], parameters[2]);
                switch (opcode)
                {
                    case 1:
                        Add(values);
                        pointer += 4;
                        break;
                    case 2:
                        Multiply(values);
                        pointer += 4;
                        break;
                    case 3:
                        Input(values);
                        if(exitCode == 3) { return; }
                        pointer += 2;
                        break;
                    case 4:
                        SetOutput(values);
                        pointer += 2;
                        break;
                    case 5:
                        JumpIfTrue(values);
                        pointer += 3;
                        break;
                    case 6:
                        JumpIfFalse(values);
                        pointer += 3;
                        break;
                    case 7:
                        LessThan(values);
                        pointer += 4;
                        break;
                    case 8:
                        Equals(values);
                        pointer += 4;
                        break;
                    case 9:
                        AdjustRelativeBase(values);
                        pointer += 2;
                        break;
                    case 99:
                        exitCode = 99;
                        running = false;
                        break;
                    default:
                        Console.WriteLine("System: [ERROR]: Unknown opcode, exiting");
                        running = false;
                        break;
                }
            }
        }
        private void AdjustRelativeBase(int[] parameters)
        {
            relativeBase += (int) IntCode[parameters[0]];
        }
        private void Equals(int[] parameters)
        {
            IntCode[parameters[2]] = IntCode[parameters[0]] == IntCode[parameters[1]] ? 1 : 0;
        }
        private void LessThan(int[] parameters)
        {
            IntCode[parameters[2]] = IntCode[parameters[0]] < IntCode[parameters[1]] ? 1 : 0;
        }
        private void JumpIfTrue(int[] parameters)
        {
            pointer = IntCode[parameters[0]] != 0 ? (int) IntCode[parameters[1]] - 3 : pointer;
        }
        private void JumpIfFalse(int[] parameters)
        {
            pointer = IntCode[parameters[0]] == 0 ? (int) IntCode[parameters[1]] - 3 : pointer;
        }
        private void SetOutput(int[] parameters)
        {
            Output = IntCode[parameters[0]];
            AddHistory(Output);
        }
        private void Input(int[] parameters)
        {
            if (Inputs.Length > 0)
            {
                IntCode[parameters[0]] = Inputs[0];
                Inputs = Inputs.Skip(1).ToArray();
            } else
            {
                Console.WriteLine("Expected input but got nothing.");
                exitCode = 3;
            }
        }
        private void Add(int[] parameters)
        {
            IntCode[parameters[2]] = IntCode[parameters[1]] + IntCode[parameters[0]];
        }
        private void Multiply(int[] parameters)
        {
            IntCode[parameters[2]] = IntCode[parameters[1]] * IntCode[parameters[0]];
        }
        public int[] GetValues(int[] parameters)
        {
            int[] values = new int[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
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
            }
            return values;
        }
        private void AddHistory(long output)
        {
            List<long> list = OutputHistory.ToList();
            list.Add(output);
            OutputHistory = list.ToArray();
        }
    }
}