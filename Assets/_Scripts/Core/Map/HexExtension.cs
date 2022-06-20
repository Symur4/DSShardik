using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Map
{
    public static class HexExtensions
    {
        public static Hex Add(this Hex a, Hex b)
        {
            a.q += b.q;
            a.r += b.r;
            return a;
        }

        public static Hex Scale(this Hex a, int k)
        {
            return new Hex(a.q * k, a.r * k);
        }

        public static Hex Subtract(this Hex a, Hex b)
        {
            return new Hex(a.q - b.q, a.r - b.r);
        }

        public static int Length(this Hex a)
        {
            return (int)((Math.Abs(a.q) + Math.Abs(a.r)) / 2);
        }

        public static int Distance(this Hex a, Hex b)
        {
            return a.Subtract(b).Length();
        }

        static public List<Hex> directions = new List<Hex> {
            new Hex(1, 0)
            ,new Hex(1, -1)
            ,new Hex(0, -1)
            ,new Hex(-1, 0)
            ,new Hex(-1, 1)
            ,new Hex(0, 1) };

        static public Hex Direction(int direction)
        {
            return HexExtensions.directions[direction];
        }

        public static Hex Neighbor(this Hex a, int direction)
        {
            return a.Add(HexExtensions.Direction(direction));
        }
    }
}
