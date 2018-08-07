using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class ArtificialIncompetence : IPrayer
    {
        /*宣言*/
        private int[] answer;
        private int digit;
        private string name;
        NumelonFunction nf = new NumelonFunction();

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="digit">桁数</param>
        /// <param name="name">ユーザ名</param>
        public ArtificialIncompetence(int digit, string name)
        {
            this.digit = digit;
            this.name = name;
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
            return nf.CreateNum(digit);
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
        /// 答えの取得
        /// </summary>
        /// <returns>答え</returns>
        public int[] getAns() { return answer; }
    }
}
