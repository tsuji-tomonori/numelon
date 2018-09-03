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
        string aName = "";
        string bName = "";
        string maxValue = "";
        string log = "";
        string PATH = "C:\\Users\\Owner\\Desktop\\log\\7\\";
        int aWin = 0;
        int bWin = 0;
        int equal = 0;
        int max = 0;
        NumelonFunction nf = new NumelonFunction();

        public void action()
        {
            Console.WriteLine("Test");
        }

        /// <summary>
        /// ログレポートを排出
        /// ログレポートはコンピュータ間の対戦のみに対応する
        /// ログレポート上部にすべての対戦結果に対するデータ
        /// その下に各対戦のデータを出力する
        /// 先攻後攻を変更する機能はない
        /// 指定した回数対戦を実施しログレポートを出力する
        /// </summary>
        /// <param name="num">対戦させる回数</param>
        /// <param name="digit">桁数</param>
        /// <returns>ログレポート</returns>
        public string getLog(int num, int digit)
        {
            string log = "";
            // Stopwatchクラス生成
            var sw = new System.Diagnostics.Stopwatch();
            // 計測開始
            sw.Start();
            for(int i = 0; i < num; i++)
            {
                log += startGame(digit);
            }
            // 計測停止
            sw.Stop();
            DateTime dt = DateTime.Now;
            string head = dt.ToString("yyyy年MM月dd日 HH時mm分ss秒") + 
                    "  処理時間 " +$"　{sw.ElapsedMilliseconds}ミリ秒\n";
            head += aName + "勝利数 : " + aWin + "  " + bName + "勝利数 : " + bWin + "\n";
            head += "総回数 : " + num + "  平均ターン数 : " + ((double)times / (double)num) + "\n";
            head += "設定した数が一致した回数 : " + equal + "\n";
            log = head + log;
            return log;
        }

        /// <summary>
        /// Testクラスのみ使用するゲーム関数
        /// ログレポートを排出することに特化している
        /// </summary>
        /// <param name="digit">桁数</param>
        /// <returns>ログレポート</returns>
        private string startGame(int digit)
        {
            /*宣言*/
            int turn = 0;
            NumelonFunction nf = new NumelonFunction();
            NumelonValueList nlv1 = new NumelonValueList(digit);
            NumelonValueList nlv2 = new NumelonValueList(digit);
            IPrayer a = new CPU2(digit, "2a",nlv1);
            IPrayer b = new CPU2(digit, "2b",nlv2);
            int[][] eatBite = new int[2][]
            {
                new[]{0,0},
                new[]{0,0}
            };
            int[] call = new int[digit];
            string winner = "";
            string log = "";

            /*ゲーム開始前処理*/
            a.Start();
            b.Start();
            aName = a.getName();
            bName = b.getName();

            log += a.getName() + ": " + nf.ToString(a.getAns()) + "  " +
                    b.getName() + " : " + nf.ToString(b.getAns()) + "\n";

            /*設定した値が一致しているか確認*/
            int[] aans = a.getAns();
            int[] bans = b.getAns();
            bool eqFlag = true;
            for (int i = 0; i < digit; i++)
            {
                if(aans[i] != bans[i])
                {
                    eqFlag = false;
                    break;
                }
            }
            if (eqFlag) equal++;

            /*どちらかがゲームに勝利するまでループ*/
            while (true)
            {
                turn++;
                /*Player a*/
                a.Call(eatBite[0]).CopyTo(call, 0);
                b.Div(call).CopyTo(eatBite[0], 0);
                log += "(" + a.getName() + ")の入力値 " + nf.ToString(call) + "  " +
                        "判定 " + nf.ToString(eatBite[0]) + "  ";
                /*勝利したとき*/
                if (eatBite[0][0] == digit)
                {
                    winner = a.getName();
                    aWin++;
                    log += "\n";
                    break;
                }

                /*Player b*/
                b.Call(eatBite[1]).CopyTo(call, 0);
                a.Div(call).CopyTo(eatBite[1], 0);
                log += "(" + b.getName() + ")の入力値 " + nf.ToString(call) + "  " +
                        "判定 " + nf.ToString(eatBite[1]) + "\n";
                /*勝利したとき*/
                if (eatBite[1][0] == digit)
                {
                    winner = b.getName();
                    bWin++;
                    break;
                }
            }
            times += turn;
            log += winner + "の勝利  ターン数" + turn + "\n";
            log += "++++++++++++++++++++++++++++++++++++++++++++++\n";
            return log;
        }

        /// <summary>
        /// (現在3桁のみ)listCheckを全数に適応
        /// それぞれログファイルを出力する
        /// </summary>
        /// <param name="digit"></param>
        /// <param name="path"></param>
        public void allAttack()
        {
            int digit = 3;
            bool[] list = nf.creatList(digit);
            for(int i = 0; i < list.Length; i++)
            {
                if (list[i])
                {
                    string str = listCheck(nf.ToNumeloValue(i, digit),false);
                    fileWrite(str, i);
                }
            }
            for(int i = 0; i < longList.Count; i++)
            {
                Console.WriteLine(longList[i]);
            }
            DateTime dt = DateTime.Now;
            string head = dt.ToString("yyyy年MM月dd日 HH時mm分ss秒") + "\n";
            head += "平均ターン数 : " + (double)times / (double)list.Length + "\n";
            Console.WriteLine("平均ターン数 : " + (double)times / (double)list.Length);
            head += "最大ターン数 : " + max + "  " + maxValue + "\n";
            Console.WriteLine("最大ターン数 : " + max);
            fileWrite(head + log, 0);
            Console.WriteLine(maxValue);
            log = "";

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

            /*表示*/
            str += print(list,digit,false);
            str += "ans : " + nf.ToString(ans) + "\n";
            if(OF) Console.WriteLine("ans : " + nf.ToString(ans));
            str += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
            if (OF) Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            str += "que : " + nf.ToString(preQ) + "\n";
            if (OF) Console.WriteLine("que : " + nf.ToString(preQ));
            str += "div : " + nf.ToString(eatBite) + "\n";
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
                if (OF) Console.WriteLine("div : " + nf.ToString(eatBite));
                str += "div : " + nf.ToString(eatBite) + "\n";
                count++;

            }
            if (OF) Console.WriteLine("ターン数 : " + count);
            str += "ターン数 : " + count + "\n";
            log += nf.ToString(preQ) + " ターン数 : " + count + "\n";
            times += count;
            if (count > max)
            {
                max = count;
                maxValue = nf.ToString(ans);

            }

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

        /// <summary>
        /// txtファイルに出力する
        /// UTF-8のテキスト用
        /// C:\Users\Owner\Desktop\log\5\digit
        /// </summary>
        /// <param name="str">ファイルに出力する内容</param>
        /// <param name="path">パス</param>
        public void fileWrite(string str, int path)
        {
            string file_path = System.IO.Path.Combine(@PATH + path + ".txt");
            // ファイルへテキストデータを書き込み
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(file_path))   // UTF-8のテキスト用
                                                                                        //using (System.IO.StreamWriter sw = new System.IO.StreamWriter(file_path, Encoding.GetEncoding("shift-jis")))  // シフトJISのテキスト用
            {
                sw.Write(str); // ファイルへテキストデータを出力する
            }
        }
    }
}
