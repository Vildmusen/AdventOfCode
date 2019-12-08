using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day7
    {
        List<Computer> computers;
        List<int[]> permutations1;
        List<int[]> permutations2;
        int max = 0;

        public void Start()
        {
            Setup();
            
            int[] phase1 = new int[] { 0,1,2,3,4};
            permutations1 = GetPermutations(phase1);

            foreach (int[] perm in permutations1)
            {
                CalculateThrusters(perm);
            }
            Console.WriteLine(max);
        }

        private void Setup()
        {
            computers = new List<Computer>() { new Computer(), new Computer(), new Computer(), new Computer(), new Computer() };
            foreach(Computer c in computers)
            {
                c.Start("7");
            }
            permutations1 = new List<int[]>();
            permutations2 = new List<int[]>();
        }

        private void CalculateThrusters(int[] phases)
        {
            int sum = 0;
            for (int i = 0; i < computers.Count; i++)
            {
                sum = CalculateThruster(computers[i], phases[i], sum);
            }
            if(sum > max)
            {
                max = sum;
            }
        }

        private int CalculateThruster(Computer c, int phase, int sum)
        {
            c.Reset();
            c.Run(new int[] { phase, sum });
            return c.ReadValueAt(c.ReadOutput());
        }

        private List<int[]> GetPermutations(int[] phases)
        {
            List<int[]> result = new List<int[]>();
            if(phases.Length == 1)
            {
                result.Add(phases);
                return result;
            }
            for (int i = 0; i < phases.Length; i++)
            {
                foreach (int[] perms in GetPermutations(Remove(phases, phases[i])))
                {
                    List<int> temp = perms.ToList();
                    temp.Add(phases[i]);
                    result.Add(temp.ToArray());   
                }
            }
            return result;
        }

        private int[] Remove(int[] phases, int v)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < phases.Length; i++)
            {
                if(phases[i] != v)
                {
                    result.Add(phases[i]);
                }
            }
            return result.ToArray();
        }

    }
}
