using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Moon
    {
        public long x;
        public long y;
        public long z;
        public long velX = 0;
        public long velY = 0;
        public long velZ = 0;

        public Moon(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void UpdatePos()
        {
            x += velX;
            y += velY;
            z += velZ;
        }

        public void ApplyGravityX(long x)
        {
            velX += x;
        }
        public void ApplyGravityY(long y)
        {
            velY += y;
        }
        public void ApplyGravityZ(long z)
        {
            velZ += z;
        }

        public long GetTotalEnergy()
        {
            return (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) * (Math.Abs(velX) + Math.Abs(velY) + Math.Abs(velZ));
        }

        public override bool Equals(object obj)
        {
            Moon temp = (Moon)obj;
            return temp.x == x && temp.y == y && temp.z == z;
        }

        public override string ToString()
        {
            return x + " " + y + " " + z;
        }
    }
}
