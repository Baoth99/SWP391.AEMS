using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.Simulator.Core
{
    public class GenerateRandom
    {
        public static Random Random = new Random();


        public static float RandomNum(int max, int min)
        {
            return Random.Next(max, min);
        }

    }
}
