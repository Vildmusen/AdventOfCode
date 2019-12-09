//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace adventofcode
//{
//    class Day7
//    {
//        List<Computer> computers;
//        List<int[]> permutations1;
//        List<int[]> permutations2;
//        int max = 0;

//        public void Start()
//        {
//            Setup();
            
//            int[] phase1 = new int[] { 0,1,2,3,4};
//            permutations1 = GetPermutations(phase1);

//            //foreach (int[] perm in permutations1)
//            //{
//            //    CalculateThrusters(perm);
//            //}
//            //Console.WriteLine(max);

//            int[] phase2 = new int[] { 5,6,7,8,9 };
//            permutations2 = GetPermutations(phase2);

//            foreach(int[] permutation in permutations2)
//            {
//                int output = 0;
//                bool isDone = false;
//                bool FirstRound = true;
//                while (!isDone)
//                {
//                    if (FirstRound)
//                    {
//                        computers[0].name = 0;
//                        computers[0].Run(new int[] { permutation[0], output });
//                        output = computers[0].ReadOutput();
//                        computers[1].name = 1;
//                        computers[1].Run(new int[] { permutation[1], output });
//                        output = computers[1].ReadOutput();
//                        computers[2].name = 2;
//                        computers[2].Run(new int[] { permutation[2], output });
//                        output = computers[2].ReadOutput();
//                        computers[3].name = 3;
//                        computers[3].Run(new int[] { permutation[3], output });
//                        output = computers[3].ReadOutput();
//                        computers[4].name = 4;
//                        computers[4].Run(new int[] { permutation[4], output });
//                        output = computers[4].ReadOutput();
//                        FirstRound = false;
//                    }
//                    else
//                    {
//                        computers[0].Run(new int[] { output });
//                        output = computers[0].ReadOutput();
//                        computers[1].Run(new int[] { output });
//                        output = computers[1].ReadOutput();
//                        computers[2].Run(new int[] { output });
//                        output = computers[2].ReadOutput();
//                        computers[3].Run(new int[] { output });
//                        output = computers[3].ReadOutput();
//                        computers[4].Run(new int[] { output });
//                        output = computers[4].ReadOutput();
//                    }
//                    if(computers.All(x => x.exitCode == 99))
//                    {
//                        isDone = true;
//                        output = computers[4].ReadOutput();
//                        //Console.WriteLine("Done with permutation " + permutation[0] + " " + permutation[1] + " " + permutation[2] + " " + permutation[3] + " " + permutation[4] + " - Output is " + output);
//                    }
//                }
//                foreach(Computer c in computers)
//                {
//                    c.Reset();
//                }
//                max = output > max ? output : max;
//            }
//            Console.WriteLine("Max is: " + max);
//        }

//        private void Setup()
//        {
//            computers = new List<Computer>() { new Computer(), new Computer(), new Computer(), new Computer(), new Computer() };
//            foreach(Computer c in computers)
//            {
//                c.Start("7");
//                c.Reset();
//            }
//            permutations1 = new List<int[]>();
//            permutations2 = new List<int[]>();
//        }

//        private void CalculateThrusters(int[] phases)
//        {
//            int sum = 0;
//            for (int i = 0; i < computers.Count; i++)
//            {
//                sum = CalculateThruster(computers[i], phases[i], sum);
//            }
//            if(sum > max)
//            {
//                max = sum;
//            }
//        }

//        private int CalculateThruster(Computer c, int phase, int sum)
//        {
//            c.Reset();
//            c.Run(new int[] { phase, sum });
//            return c.ReadValueAt(c.ReadOutput());
//        }

//        private List<int[]> GetPermutations(int[] phases)
//        {
//            List<int[]> result = new List<int[]>();
//            if(phases.Length == 1)
//            {
//                result.Add(phases);
//                return result;
//            }
//            for (int i = 0; i < phases.Length; i++)
//            {
//                foreach (int[] perms in GetPermutations(Remove(phases, phases[i])))
//                {
//                    List<int> temp = perms.ToList();
//                    temp.Add(phases[i]);
//                    result.Add(temp.ToArray());   
//                }
//            }
//            return result;
//        }

//        private int[] Remove(int[] phases, int v)
//        {
//            List<int> result = new List<int>();
//            for (int i = 0; i < phases.Length; i++)
//            {
//                if(phases[i] != v)
//                {
//                    result.Add(phases[i]);
//                }
//            }
//            return result.ToArray();
//        }

//    }
//}
