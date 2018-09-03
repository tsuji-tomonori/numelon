using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class NumelonFunction
    {
        //関数の外で定義すること
        Random rnd = new System.Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// 指定された桁数のヌメロンに適した数の作成
        /// </summary>
        /// <param name="digit">桁数 (正の整数値(1～9)/チェック未処理)</param>
        /// <returns>作成した数</returns>
        public int[] CreateNum(int digit)
        {
            /*宣言*/
            bool[] numFlag = new bool[10];
            bool finFlag = false;
            int count = 0;
            int[] num = new int[digit];

            //初期化
            for (int i = 0; i < numFlag.Length; i++) { numFlag[i] = true; }

            /*終了フラグが立っていない間以下の処理をループ*/
            while (!finFlag)
            {
                int rand = rnd.Next(10);
                /*乱数が他の桁と重複しないとき以下の処理*/
                if (numFlag[rand])
                {
                    num[count] = rand;
                    numFlag[rand] = false;
                    count++;
                    //指定された桁数まで数を作成したとき終了フラグを立てる
                    if (count == digit) { finFlag = true; }
                }
            }
            return num;
        }

        /// <summary>
        /// EatBiteの判定 入力値の内容のチェックはしていない
        /// </summary>
        /// <param name="question">質問</param>
        /// <param name="answer">答え</param>
        /// <returns>判定結果(eatBite[0] = Eat , eatBite[1] = Bite)</returns>
        public int[] checkEatBite(int[] question, int[] answer)
        {
            int[] eatBite = new int[2];
            for (int i = 0; i < question.Length; i++)
            {
                for (int j = 0; j < answer.Length; j++)
                {
                    if (question[i] == answer[j])
                    {
                        //Eat
                        if (i == j) { eatBite[0]++; }
                        //Bite
                        else { eatBite[1]++; }
                    }
                }
            }
            return eatBite;
        }

        /// <summary>
        /// int配列を文字列([,,]の形式)へ変換
        /// </summary>
        /// <param name="num">変換もとの配列</param>
        /// <returns>変換後の文字列</returns>
        public string ToString(int[] num)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            for (int i = 0; i < num.Length; i++)
            {
                sb.Append(num[i]);
                if (i != num.Length - 1) sb.Append(", ");
            }
            sb.Append(" ]");
            string str = sb.ToString();
            return str;
        }

        /// <summary>
        /// あるint配列の大きさが桁数と一致するか, 
        /// 各桁において重複する桁がないかをチェックする. 
        /// true : OK(numelonに適した値), false : NG(桁数と一致しない or 重複がある)
        /// </summary>
        /// <param name="num">チェックするint配列</param>
        /// <param name="digit">桁数</param>
        /// <returns>判定結果</returns>
        public bool IsNumelonValue(int[] num, int digit)
        {
            /*判定する配列の大きさと桁数が一致しないとき*/
            if (num.Length != digit)
                return false;
            /*判定する配列の大きさだけループ*/
            for (int i = 0; i < num.Length; i++)
            {
                /*jがi未満の間ループ*/
                for (int j = 0; j < i; j++)
                {
                    /*適当な各桁の二数が一致するとき*/
                    if (num[i] == num[j])
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 数値範囲チェック
        /// </summary>
        /// <param name="a">対象数値</param>
        /// <param name="from">範囲（開始）</param>
        /// <param name="to">範囲（終了）</param>
        /// <returns>結果</returns>
        public bool IsRange(int a, int from, int to)
        {
            return (from <= a && a <= to);
        }

        /// <summary>
        ///  0プレースホルダーを作成する
        /// </summary>
        /// <param name="digit">プレースホルダーの桁数</param>
        /// <returns>作成したプレースホルダー</returns>
        private string createPlaceHolder(int digit)
        {
            string placeHolder = "";
            for (int i = 0; i < digit; i++) { placeHolder += "0"; }
            return placeHolder;
        }

        /// <summary>
        /// 文字列をヌメロン値に変換する
        /// 文字列の大きさの配列に1桁ずつ格納する
        /// 文字列の内容確認はしない
        /// </summary>
        /// <param name="str">変換対象の文字列</param>
        /// <returns>変換したヌメロン値</returns>
        public int[] ToNumeloValue(String str)
        {
            int[] buf = new int[str.Length];
            //変換対象文字列の大きさだけ
            for (int i = 0; i < str.Length; i++)
            {
                if (!int.TryParse(str[i].ToString(), out buf[i])) { Console.WriteLine("error: ToNumeloValue(String str)"); }
            }
            return buf;
        }

        /// <summary>
        /// 整数値をヌメロン値に変換する
        /// 変換するのは1以上(0は使用しないため変換できない)
        /// 関数内でcreatePlaceHolder を呼び出す
        /// </summary>
        /// <param name="index">整数値(1以上)</param>
        /// <param name="digit">桁数</param>
        /// <returns>変換したヌメロン値</returns>
        public int[] ToNumeloValue(int index, int digit)
        {
            string placeHolder = createPlaceHolder(digit);
            string buf = index.ToString(placeHolder);
            return ToNumeloValue(buf);

        }

        /// <summary>
        /// 指定した桁数n に対して 10^n のbool配列を返す.
        /// 初期化も行う.
        /// indexをヌメロン値として扱う. 
        /// trueはヌメロンに適した値, falseはヌメロンに適さなかった値.
        /// [0]は必ずfalse (000はありえないから)
        /// indexをヌメロン値に変換するにはToNumelonValue(int num, int digit)を用いる.
        /// </summary>
        /// <param name="digit">指定する桁数</param>
        /// <returns>作成した配列</returns>
        public bool[] creatList(int digit)
        {
            bool[] list = new bool[(int)Math.Pow(10, digit)];
            list[0] = false;
            for (int i = 0; i < list.Length; i++)
            {
                int[] buf = ToNumeloValue(i, digit);
                if (IsNumelonValue(buf, digit)) list[i] = true;
                else list[i] = false;
            }
            return list;
        }

        /// <summary>
        /// 質問とその判定を元に, 全パターン計算し起こりえないものfalseにしてリストを返す
        /// </summary>
        /// <param name="question">質問内容</param>
        /// <param name="eatBite">質問内容の判定</param>
        /// <param name="lst">判定結果を入れるリスト</param>
        /// <param name="digit">質問内容の桁数</param>
        /// <returns>処理したリスト</returns>
        public bool[] deleteList(int[] question, int[] eatBite, bool[] list, int digit)
        {
            for (int i = 1; i < list.Length; i++)
            {
                //あり得る手の時
                if (list[i])
                {
                    int[] buf = checkEatBite(question, ToNumeloValue(i, digit));
                    //前回の判定結果より, あり得ない手だった場合
                    if (!(eatBite[0] == buf[0] & eatBite[1] == buf[1]))
                    {
                        list[i] = false;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// ゲーム処理
        /// </summary>
        /// <param name="digit"></param>
        /// <param name="prayer1"></param>
        /// <param name="prayer2"></param>
        /// <returns></returns>
        public string GameStart(int digit, IPrayer prayer1, IPrayer prayer2)
        {
            /*エラーチェック*/
            if (prayer1 == null || prayer2 == null)
                throw new ArgumentNullException();

            /*宣言*/
            int times = 0;
            int[][] eatBite = new int[2][]
            {
                new[]{0,0},
                new[]{0,0}
            };
            int[] call = new int[digit];
            string winner = "";
            string log = "";

            /*ゲーム開始前処理*/
            prayer1.Start();
            prayer2.Start();

            Console.WriteLine("Game Start!!");

            //各prayer の答えを含むため ゲーム開始前処理後に記述
            log += prayer1.getName() + ": " + ToString(prayer1.getAns()) + "  " +
                    prayer2.getName() + " : " + ToString(prayer2.getAns()) + "\n";

            /*どちらかがゲームに勝利するまでループ*/
            while (true)
            {
                times++;
                /*Player1*/
                prayer1.Call(eatBite[0]).CopyTo(call, 0);
                prayer2.Div(call).CopyTo(eatBite[0], 0);
                Console.WriteLine(prayer1.getName() + " : " + ToString(call));
                Console.WriteLine(prayer1.getName() + " : " + ToString(eatBite[0]));
                log += "(" + prayer1.getName() + ")の入力値 " + ToString(call) + "  " +
                        "判定 " + ToString(eatBite[0]) + "  ";
                /*勝利したとき*/
                if (eatBite[0][0] == digit)
                {
                    winner = prayer1.getName();
                    log += "\n";
                    break;
                }

                /*Player2*/
                prayer2.Call(eatBite[1]).CopyTo(call, 0);
                prayer1.Div(call).CopyTo(eatBite[1], 0);
                Console.WriteLine(prayer2.getName() + " : " + ToString(call));
                Console.WriteLine(prayer2.getName() + " : " + ToString(eatBite[1]));
                log += "(" + prayer2.getName() + ")の入力値 " + ToString(call) + "  " +
                        "判定 " + ToString(eatBite[1]) + "\n";
                /*勝利したとき*/
                if (eatBite[1][0] == digit)
                {
                    winner = prayer2.getName();
                    break;
                }
            }

            Console.WriteLine("*************************************");
            Console.WriteLine("winner is " + winner);
            log += "++++++++++++++++++++++++++++++++++++++++++++++\n";
            log += winner + "の勝利  ターン数" + times + "\n";

            return log;
        }

        /// <summary>
        /// 与えられたリストを文字列にして返す(表示する)
        /// </summary>
        /// <param name="list">表示するリスト</param>
        /// <param name="digit">リストの桁数</param>
        /// <param name="OF">OutputFlag(表示する場合はtrue)</param>
        /// <returns>作成した文字列</returns>
        public string print(bool[] list, int digit, bool OF)
        {
            string str = "";
            int count = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i])
                {
                    if (OF) { Console.WriteLine(ToString(ToNumeloValue(i, digit))); }
                    str += ToString(ToNumeloValue(i, digit)) + "\n";
                    count++;
                }
            }
            if (OF) { Console.WriteLine(count); }
            str += count + "\n";
            if (OF) { Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++"); }
            str += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n";

            return str;
        }

        /// <summary>
        /// txtファイルに出力する
        /// UTF-8のテキスト用
        /// </summary>
        /// <param name="str">ファイルに出力する内容</param>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="fileName">ファイル名</param>
        public void fileWrite(string str, string filePath, string fileName)
        {
            string file_path = System.IO.Path.Combine(@filePath + "\\" + fileName + ".txt");
            // ファイルへテキストデータを書き込み
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(file_path))   // UTF-8のテキスト用
                                                                                        //using (System.IO.StreamWriter sw = new System.IO.StreamWriter(file_path, Encoding.GetEncoding("shift-jis")))  // シフトJISのテキスト用
            {
                sw.Write(str); // ファイルへテキストデータを出力する
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
        public int scanNum(int min, int max)
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
        public void logWriteToFile(string log)
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
