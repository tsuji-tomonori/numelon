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
            bool[] list = nf.creatList(3);
            for(int i = 0; i < list.Length; i++)
            {
                if (list[i])
                {
                    string str = all(nf.ToNumeloValue(i, digit));
                    fileWrite(str, i);
                }
            }
            for(int i = 0; i < longList.Count; i++)
            {
                Console.WriteLine(longList[i]);
            }
            Console.WriteLine("平均ターン数 : " + times / list.Length);
            Console.WriteLine("最大ターン数 : " + max);
            Console.WriteLine(maxValue);
        }

        private static void fileWrite(string str, int path)
        {
            string file_path = System.IO.Path.Combine(@"C:\Users\Owner\Desktop\log\3\" + path+".txt");
            // ファイルへテキストデータを書き込み
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(file_path))   // UTF-8のテキスト用
                                                                                        //using (System.IO.StreamWriter sw = new System.IO.StreamWriter(file_path, Encoding.GetEncoding("shift-jis")))  // シフトJISのテキスト用
            {
                sw.Write(str); // ファイルへテキストデータを出力する
            }
        }

        private static string all(int[] ans)
        {
            int count = 0;
            int[] preQ = { 0, 1, 2 };
            int[] eatBite = nf.checkEatBite(preQ, ans);
            bool[] list = nf.creatList(digit);
            string str = "";
            

            str += print(list);
            str += "ans : " + nf.ToString(ans) + "\n";
            //Console.WriteLine("ans : " + nf.ToString(ans));
            str += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            //Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            str += "que : " + nf.ToString(preQ) + "\n";
            //Console.WriteLine("que : " + nf.ToString(preQ));
            str += "div : " + nf.ToString(eatBite) + "\n";
            //Console.WriteLine("div : " + nf.ToString(eatBite));
            count++;

            while (true)
            {
                if (eatBite[0] == 3) break;
                if (count == 20)
                {
                    longList.Add(nf.ToString(ans));
                    break;
                }
                int question = 0;
                nf.deleteList(preQ, eatBite, list, digit);
                str += print(list);
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
                //Console.WriteLine("que : " + nf.ToString(preQ));
                str += "que : " + nf.ToString(preQ) + "\n";
                //Console.WriteLine("div : " + nf.ToString(eatBite));
                str += "div : " + nf.ToString(eatBite) + "\n";
                count++;

            }
            //Console.WriteLine("ターン数 : " + count);
            str += "ターン数 : " + count + "\n";
            times += count;
            if(count > max)
            {
                max = count;
                maxValue = nf.ToString(ans);

            }

            return str;
        }

        private static string print(bool[] list)
        {
            string str = "";
            int count = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i])
                {
                    //Console.WriteLine(nf.ToString(nf.ToNumeloValue(i, digit)));
                    str += nf.ToString(nf.ToNumeloValue(i, digit)) + "\n";
                    count++;
                }
            }
            //Console.WriteLine(count);
            str += count + "\n";
            //Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            str += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";

            return str;
        }
    }
}
