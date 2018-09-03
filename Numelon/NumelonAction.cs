using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class NumelonAction : IAction
    {
        public void action()
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
            digit = nf.scanNum(2, 9);

            /*モード設定*/
            Console.WriteLine("モードを選んでください(Human : 1 , CPU : 2 , 終了 : 0)");
            mode = nf.scanNum(0, 2);
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
            opponent = nf.scanNum(0, 3);
            switch (opponent)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    prayer2 = new Human(digit);
                    break;
                case 2:
                    if (mode == 2) prayer2 = new CPU1(digit, "CPU2");
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
            switch (nf.scanNum(0, 1))
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
                switch (nf.scanNum(0, 2))
                {
                    case 0:
                        logFlag = false;
                        break;
                    case 1:
                        nf.logWriteToFile(log);
                        break;
                    case 2:
                        Console.WriteLine(log);
                        break;
                }
            }
        }
    }
}
