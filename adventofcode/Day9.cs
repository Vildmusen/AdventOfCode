using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day9
    {
        public void Start()
        {
            Computer c = new Computer();
            c.Start("9");
            c.Reset();
            c.Run(new long[] { 2 });
            c.PrintOutPut();
        }
    }
}
