using System;
using System.IO;
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
            /*宣言*/
            int mode;
            int opponent;
            int digit;
            string log = "";
            bool logFlag = false;
            IPrayer prayer1 = null;
            IPrayer prayer2 = null;
            NumelonFunction nf = new NumelonFunction();
            // Stopwatchクラス生成
            var sw = new System.Diagnostics.Stopwatch();

            Console.WriteLine("Numelon Game");

            /*桁数設定*/
            Console.WriteLine("ゲームの桁数を入力してください(2 ～ 9)");
            digit = scanNum(2, 9);

            /*モード設定*/
            Console.WriteLine("モードを選んでください(Human : 1 , CPU : 2 , 終了 : 0)");
            mode = scanNum(0, 2);
            switch (mode)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    prayer1 = new Human(digit);
                    break;
                case 2:
                    prayer1 = new CPU1(digit, "CPU1");
                    break;
            }

            /*対戦相手の設定*/
            Console.WriteLine("対戦相手を選んでください(Human : 1 , CPU : 2 , 人工無能 : 3 , 終了 : 0)");
            opponent = scanNum(0, 3);
            switch (opponent)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    prayer2 = new Human(digit);
                    break;
                case 2:
                   if(mode == 2) prayer2 = new CPU1(digit, "CPU2");
                    else prayer2 = new CPU1(digit, "CPU");
                    break;
                case 3:
                    prayer2 = new ArtificialIncompetence(digit, "人工無能");
                    break;

            }

            /*ゲーム開始*/
            // 計測開始
            sw.Start();
            try
            {
                log = nf.GameStart(digit, prayer1, prayer2);
            }
            //引数にnullがあったとき
            catch (ArgumentNullException)
            {
                Console.WriteLine("エラーが発生しました プログラムを終了します");
            }
            sw.Stop();

            /*ログファイルの処理*/
            Console.WriteLine("ログを出力しますか? yes : 1 no : 0");
            switch (scanNum(0, 1))
            {
                case 0:
                    logFlag = false;
                    break;
                case 1:
                    logFlag = true;
                    break;
            }
            if (logFlag)
            {
                DateTime dt = DateTime.Now;
                string head = dt.ToString("yyyy年MM月dd日 HH時mm分ss秒") +
                        "  処理時間 " + $"　{sw.ElapsedMilliseconds}ミリ秒\n";
                head += "+++++++++++++++++++++++++++++++++++++++++++++++++++++\n";
                log = head + log;

                Console.WriteLine("ファイルに出力しますか? ");
                Console.WriteLine("ファイルに出力する : 1 , コンソール上に出力する : 2 , 出力しない : 0");
                switch (scanNum(0, 2))
                {
                    case 0:
                        logFlag = false;
                        break;
                    case 1:
                        logWriteToFile(log);
                        break;
                    case 2:
                        Console.WriteLine(log);
                        break;
                }
            }
        }

        /// <summary>
        /// ユーザーから整数値を受け取る
        /// ユーザーが適した整数値を入力するまで入力を促す
        /// 整数値の範囲は min 以上 max 以下
        /// </summary>
        /// <param name="min">指定する範囲の最小値</param>
        /// <param name="max">指定する範囲の最大値</param>
        /// <returns>ユーザーが入力した数</returns>
        private static int scanNum(int min, int max)
        {
            /*宣言*/
            bool finFlag = false;
            int buf = 0;
            NumelonFunction nf = new NumelonFunction();

            //ユーザーが正しく入力するまでループ
            while (!finFlag)
            {
                string str = Console.ReadLine();
                //入力されたものが整数であるとき
                if (int.TryParse(str, out buf))
                {
                    //入力された整数が指定された範囲内であるとき
                    if (nf.IsRange(buf, min, max))
                    {
                        finFlag = true;
                    }
                }
                //入力内容が正しくないとき
                if (!finFlag) Console.WriteLine("入力内容に誤りがあります.再度入力してください.");
            }
            return buf;
        }

        /// <summary>
        /// ログファイルをファイルに出力する
        /// ファイルパスがおかしいときは再度入力してもらう
        /// </summary>
        /// <param name="log">logの内容</param>
        private static void logWriteToFile(string log)
        {
            bool finflag = false;
            while (!finflag)
            {
                NumelonFunction nf = new NumelonFunction();
                Console.Write("ファイルパスを入力してください : ");
                string filePath = Console.ReadLine();
                Console.WriteLine();
                Console.Write("ファイル名を入力してください : ");
                string fileName = Console.ReadLine();
                try
                {
                    nf.fileWrite(log, filePath, fileName);
                    finflag = true;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("指定されたファイルは存在しません");
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("指定されたディレクトリが存在しません");
                }
            }
        }
    }
}
