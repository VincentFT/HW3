using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpaceGame
{
    class myRandom
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
                
        public static int RandomIntNumber(int min, int max)
        {
            lock (syncLock)
            { 
                return random.Next(min, max);
            }
        }

        
        public static double RandomDoubleNumber()
        {
            lock (syncLock)
            { 
                return random.NextDouble();
            }
        }
    }
}
