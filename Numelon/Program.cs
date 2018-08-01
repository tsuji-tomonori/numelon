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

        static void Main(string[] args)
        {
            bool[] list = nf.creatList(3);
            for(int i = 0; i < list.Length; i++)
            {
                if (list[i]) all(nf.ToNumeloValue(i, digit));
            }
        }

        private static string all(int[] ans)
        {
            int count = 0;
            int[] preQ = { 0, 1, 2 };
            int[] eatBite = nf.checkEatBite(preQ, ans);
            bool[] list = nf.creatList(digit);
            string str = "";

            print(list);
            str += "ans : " + nf.ToString(ans) + "\n";
            Console.WriteLine("ans : " + nf.ToString(ans));
            str += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            str += "que : " + nf.ToString(preQ) + "\n";
            Console.WriteLine("que : " + nf.ToString(preQ));
            str += "div : " + nf.ToString(eatBite) + "\n";
            Console.WriteLine("div : " + nf.ToString(eatBite));
            count++;

            while (true)
            {
                if (eatBite[0] == 3) break;
                if (count == 20) break;
                int question = 0;
                nf.deleteList(preQ, eatBite, list, digit);
                print(list);
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i])
                    {
                        question = i;
                        break;
                    }
                }
                preQ = nf.ToNumeloValue(question, digit);
                eatBite = nf.checkEatBite(preQ, ans);
                Console.WriteLine("que : " + nf.ToString(preQ));
                str += "que : " + nf.ToString(preQ) + "\n";
                Console.WriteLine("div : " + nf.ToString(eatBite));
                str += "div : " + nf.ToString(eatBite) + "\n";
                count++;

            }
            Console.WriteLine("ターン数 : " + count);
            str += "ターン数 : " + count + "\n";

            return str;
        }

        private static void print(bool[] list)
        {
            int count = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i])
                {
                    Console.WriteLine(nf.ToString(nf.ToNumeloValue(i, digit)));
                    count++;
                }
            }
            Console.WriteLine(count);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        }
    }
}
