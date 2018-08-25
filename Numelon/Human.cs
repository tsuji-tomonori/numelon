using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class Human : IPrayer
    {
        /*宣言*/
        private int[] answer;
        private int digit;
        private string name;
        NumelonFunction nf = new NumelonFunction();

        /// <summary>
        /// デフォルトコンストラクター
        /// ユーザ名・桁数はコンソール上にて入力してもらう
        /// ScanNum, ScanDigitを使用
        /// </summary>
        public Human(int digit)
        {
            name = ScanName();
            this.digit = digit;
        }

        /// <summary>
        /// ゲーム開始
        /// 自分の数をコンソール上にて入力してもらう
        /// ScanNum関数を使用
        /// </summary>
        public void Start()
        {
            answer = ScanNum(name + "さんの数を入力してください");
            
        }

        /// <summary>
        /// 手の決定
        /// ScanNumを使用
        /// </summary>
        /// <param name="eatBite">前回の判定結果(使わない)</param>
        /// <returns>手(コンソール上にて入力されたもの)</returns>
        public int[] Call(int[] eatBite)
        {
            return ScanNum(name + "さん 予想する数を入力してください");
        }

        /// <summary>
        /// 相手の手の判定
        /// </summary>
        /// <param name="question">相手の手</param>
        /// <returns>判定結果</returns>
        public int[] Div(int[] question)
        {
            return nf.checkEatBite(question, answer);
        }

        /// <summary>
        /// コンソール上にて桁数を入力してもらう
        /// </summary>
        /// <returns>入力された桁数</returns>
        private int ScanDigit()
        {
            /*宣言*/
            bool finFlag = false;
            int buf = 0;

            /*適切に入力されるまでループ*/
            while (!finFlag)
            {
                Console.Write("桁数を入力してください(1～9) :");
                string str = Console.ReadLine();
                /*入力内容が整数値のとき*/
                if(int.TryParse(str, out buf))
                {
                    /*入力値が1～9であるとき*/
                    if (nf.IsRange(buf, 1, 9)) { finFlag = true; }
                }
                /*入力内容に不備があるとき*/
                if (!finFlag) { Console.WriteLine("入力内容に誤りがあります"); }
            }
            return buf;
        }

        /// <summary>
        /// コンソール上にて値(numelonに適した)をを入力してもらう
        /// </summary>
        /// <param name="message">入力を促すメッセージ内容</param>
        /// <returns>入力された値</returns>
        private int[] ScanNum(string message)
        {
            /*宣言*/
            bool finFlag = false;
            int[] ans = new int[digit];

            /*適切に入力されるまでループ*/
            while (!finFlag)
            {
                bool errorFlag = false;
                Console.Write(message + " : ");
                string str = Console.ReadLine();
                /*桁数が入力内容の大きさと一致するとき*/
                if(str.Length == digit)
                {
                    /*入力内容の大きさだけループ*/
                    for (int i = 0; i < str.Length; i++)
                    {
                        /*入力された値が整数値のとき*/
                        if (!int.TryParse(str[i].ToString(), out ans[i]))
                        {
                            errorFlag = true;
                            break;
                        }
                    }
                }
                /*入力内容に不備がないとき*/ //重複・桁数の確認
                if (!errorFlag) { errorFlag = !nf.IsNumelonValue(ans, digit); }
                /*入力内容に不備があるとき*/
                if (errorFlag) { Console.WriteLine("入力内容に誤りがあります"); }
                /*入力内容に不備がないとき*/
                else { finFlag = true; }

            }
            return ans;
        }

        /// <summary>
        /// コンソール上にてユーザ名を入力してもらう
        /// </summary>
        /// <returns>入力されたユーザ名</returns>
        private string ScanName()
        {
            Console.Write("名前を入力してください : ");
            string str = Console.ReadLine();
            return str;
        }

        /// <summary>
        /// ユーザ名の取得
        /// </summary>
        /// <returns>ユーザ名</returns>
        public string getName() { return name; }

        /// <summary>
        /// 桁数の取得
        /// </summary>
        /// <returns>桁数</returns>
        public int getDigit() { return digit; }

        /// <summary>
        /// 答えの取得
        /// </summary>
        /// <returns>答え</returns>
        public int[] getAns() { return answer; }
    }
}
