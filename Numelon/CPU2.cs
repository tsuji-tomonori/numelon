using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class CPU2 : IPrayer
    {
        /*宣言*/
        private int[] answer;
        private int[] pre;
        private int digit;
        private bool firstFlag = true;
        private string name;
        NumelonFunction nf = new NumelonFunction();
        //関数の外で定義すること
        Random rnd = new System.Random((int)DateTime.Now.Ticks);

        private bool[] firstNum = new bool[10];
        private bool[] secondNum = new bool[10];
        private bool[] thirdNum = new bool[10];

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="digit">桁数</param>
        /// <param name="name">ユーザ名</param>
        public CPU2(int digit, string name)
        {
            this.digit = digit;
            this.name = name;
            pre = new int[digit];

            for(int i = 0; i < 10; i++)
            {
                firstNum[i] = true;
                secondNum[i] = true;
                thirdNum[i] = true;
            }
            
        }


        /// <summary>
        /// ゲーム開始
        /// ランダムに自分の数を決定
        /// </summary>
        public void Start()
        {
            answer = CreateNum(digit);
        }

        /// <summary>
        /// 手の決定
        /// </summary>
        /// <param name="eatBite">前回の手の判定結果(使わない)</param>
        /// <returns>手(ランダム)</returns>
        public int[] Call(int[] eatBite)
        {
            
           
            return pre;
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
        /// 指定された桁数のヌメロンに適した数の作成
        /// </summary>
        /// <param name="digit">桁数 (正の整数値(1～9)/チェック未処理)</param>
        /// <returns>作成した数</returns>
        private int[] CreateNum(int digit)
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
