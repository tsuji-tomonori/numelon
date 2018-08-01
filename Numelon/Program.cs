using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            int digit = 3;
            NumelonFunction nf = new NumelonFunction();
            bool[] list = nf.creatList(digit);
            for(int i = 0; i < list.Length; i++)
            {
                if (list[i])
                {
                    Console.WriteLine(nf.ToString(nf.ToNumeloValue(i, digit)));
                    count++;
                }
            }
            Console.WriteLine(count);
            
        }
    }
}
