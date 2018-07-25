using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class CPULevel1 : IPrayer
    {
        /*宣言*/
        private int[] answer;
        private int[] preQuestion;
        private int digit;
        private int count = 0;
        private string placeHolder = "";
        private string name;
        private bool[] lst;
        NumelonFunction nf = new NumelonFunction();

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="digit">桁数</param>
        /// <param name="name">ユーザ名</param>
        public CPULevel1(int digit, string name)
        {
            this.digit = digit;
            this.name = name;
            intializeLst();
            createPlaceHolder();
        }
        
        /// <summary>
        /// ゲーム開始
        /// ランダムに自分の数を決定
        /// </summary>
        public void Start()
        {
            answer = nf.CreateNum(digit);
        }

        /// <summary>
        /// 手の決定
        /// </summary>
        /// <param name="eatBite">前回の手の判定結果(使わない)</param>
        /// <returns>手(ランダム)</returns>
        public int[] Call(int[] eatBite)
        {
            if (count == 0)
            {
                count++;
                preQuestion = nf.CreateNum(digit);
                return preQuestion;
            }
            
            deleteLst(eatBite);
            for (int i = 1; i < lst.Length; i++)
            {
                if (lst[i]) { preQuestion = ToNumeloValue(i); }
            }
                return preQuestion;
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
        /// lst の初期化
        /// 桁数が決定した後使用する
        /// 配列の大きさは 10^3
        /// lst[0]は使用しない(False)
        /// 配列のインデックスがヌメロンの値と対応(以下作成方法)
        /// 1:桁数通りになるよう文字列を作成し
        /// 2:それを配列に変換する
        /// </summary>
        private void intializeLst()
        {
            lst = new bool[(int)Math.Pow(10,digit) + 1];
            lst[0] = false;

            for (int i = 1; i < lst.Length; i++)
            {
                lst[i] = true;
            }

            for (int i = 1; i < lst.Length; i++)
            {
                if (!nf.IsNumelonValue(ToNumeloValue(i), digit)) { lst[i] = false; }
            }
        }

        /// <summary>
        /// 前回の手を参考にして起こりえない手を消す
        /// </summary>
        /// <param name="eatBite">前回の判定結果</param>
        private void deleteLst(int[] eatBite)
        {
            for (int i = 1; i < lst.Length; i++)
            {
                //あり得る手の時
                if (lst[i])
                {
                    int[] buf = nf.checkEatBite(preQuestion, ToNumeloValue(i));
                    //前回の判定結果より, あり得ない手だった場合
                    if (!(eatBite[0] == buf[0] & eatBite[1] == buf[1]))
                    {
                        lst[i] = false;
                    }
                }
            }
        }

        /// <summary>
        /// 0プレースホルダーを作成する
        /// 桁数が決定した後使用する
        /// 作成した0プレースホルダーは placeHolder に格納する
        /// </summary>
        private void createPlaceHolder()
        {
            for (int i = 0; i < digit; i++) { placeHolder += "0"; }
        }

        /// <summary>
        /// 組み合わせの数を求める (nPr)
        /// </summary>
        /// <param name="n">n</param>
        /// <param name="r">r</param>
        /// <returns>nPr</returns>
        public int combination(int n, int r)
        {
            if (n == r || r == 0)
                return 1;
            else
                return combination(n, r - 1) * (n - r + 1) / r;
        }

        /// <summary>
        /// lst配列の添え字をヌメロン値に変換する
        /// 変換するのは1以上(0は使用しないため変換できない)
        /// placeHolder を使用するためあらかじめ設定しておく
        /// </summary>
        /// <param name="index">lst配列の添え字(1以上)</param>
        /// <returns>変換したヌメロン値</returns>
        public int[] ToNumeloValue(int index)
        {
            //indexが0
            if (index == 0) Console.WriteLine("error:ToNumeronValue");
            string buf = index.ToString(placeHolder);
            return ToNumeloValue(buf);
            
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

    }
    
}
