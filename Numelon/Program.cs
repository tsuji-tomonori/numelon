using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class Program
    {
        static NumelonFunction nf = new NumelonFunction();
        private static int digit = 3;
        static List<string> longList = new List<string>();
        static int times = 0;
        static int max = 0;
        static string maxValue = "";


        static void Main(string[] args)
        {

            Test test = new Test();
            test.allAttack();

        }

        public static long Factorial(int n)
        {
            if (n == 0)
                return 1L;
            return n * Factorial(n - 1);
        }

       

        
    }
}
