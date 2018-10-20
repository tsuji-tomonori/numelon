using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class Test : IAction
    {
        List<string> longList = new List<string>();
        int times = 0;
        string maxValue = "";
        string log = "";
        string PATH = "C:\\Users\\Owner\\Desktop\\";
        int max = 0;
        NumelonFunction nf = new NumelonFunction();

        public void action()
        {
            allCall();
        }

        private void allCall()
        {
            Console.WriteLine("作成するフォルダー名を入力してください");
            string folder = Console.ReadLine();
            System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(PATH + folder);
            nf.fileWrite(listCheck(new int[3] { 2, 5, 6 }, false), "C:\\Users\\Owner\\Desktop\\" + folder, "test");
        }

        /// <summary>
        /// 指定された手に対して
        /// nf.deleteListを実行してlistがどのように変化するかを調べる
        /// その状況を文字列にしたレポート返す
        /// </summary>
        /// <param name="ans">調べる手</param>
        /// <param name="digit">桁数</param>
        /// <param name="OF">OutputFlag(表示する場合はtrue)</param>
        /// <returns>作成したレポート</returns>
        private string listCheck(int[] ans,  bool OF)
        {
            /*宣言 + 1回目のcall */
            int digit = 3;
            int count = 0;
            int[] preQ = { 0, 1, 2 };
            int[] eatBite = nf.checkEatBite(preQ, ans);
            bool[] list = nf.creatList(digit);
            string str = "";
            string head = "";

            /*表示*/
            str += print(list,digit,false);
            str += "ans : " + nf.ToString(ans) + "\n";
            head += "ans : " + nf.ToString(ans) + "\n";
            if (OF) Console.WriteLine("ans : " + nf.ToString(ans));
            str += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            head += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            if (OF) Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            str += "que : " + nf.ToString(preQ) + "\n";
            head += "que : " + nf.ToString(preQ) + "\n";
            if (OF) Console.WriteLine("que : " + nf.ToString(preQ));
            str += "div : " + nf.ToString(eatBite) + "\n";
            head += "div : " + nf.ToString(eatBite) + "\n";
            if (OF) Console.WriteLine("div : " + nf.ToString(eatBite));

            count++;

            /*解析が終わるまで*/
            while (true)
            {
                //解析終了
                if (eatBite[0] == digit) break;
                //20ターン以上かかったとき
                if (count == 20)
                {
                    longList.Add(nf.ToString(ans));
                    break;
                }
                int question = 0;
                nf.deleteList(preQ, eatBite, list, digit);
                str += print(list,digit,false);
                //リストの要素数だけループ
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
                if (OF) Console.WriteLine("que : " + nf.ToString(preQ));
                str += "que : " + nf.ToString(preQ) + "\n";
                head += "que : " + nf.ToString(preQ) + "\n";
                if (OF) Console.WriteLine("div : " + nf.ToString(eatBite));
                str += "div : " + nf.ToString(eatBite) + "\n";
                head += "div : " + nf.ToString(eatBite) + "\n";
                count++;

            }
            if (OF) Console.WriteLine("ターン数 : " + count);
            str += "ターン数 : " + count + "\n";
            head += "ターン数 : " + count + "\n";
            head += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            log += nf.ToString(preQ) + " ターン数 : " + count + "\n";
            times += count;
            if (count > max)
            {
                max = count;
                maxValue = nf.ToString(ans);

            }
            str = head + str;
            return str;
        }

        /// <summary>
        /// 与えられたリストの中身を文字列化して返す(表示する)
        /// リストはbool型, 中身がtrueのもののみ
        /// </summary>
        /// <param name="list">使用するリスト</param>
        /// <param name="digit">桁数</param>
        /// <param name="OF">OutputFlag(表示する場合はtrue)</param>
        /// <returns>中身がtrueのインデックスをヌメロン値にしたもの</returns>
        public string print(bool[] list, int digit, bool OF)
        {
            string str = "";
            int count = 0;
            /*リストの要素数だけループ*/
            for (int i = 0; i < list.Length; i++)
            {
                /*リストの中身がtrue*/
                if (list[i])
                {
                    if(OF) Console.WriteLine(nf.ToString(nf.ToNumeloValue(i, digit)));
                    str += nf.ToString(nf.ToNumeloValue(i, digit)) + "\n";
                    count++;
                }
            }
            if(OF) Console.WriteLine(count);
            str += count + "\n";
            if(OF) Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            str += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";

            return str;
        }
    }
}
