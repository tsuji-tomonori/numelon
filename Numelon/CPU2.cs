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
        private bool[] list;
        NumelonFunction nf = new NumelonFunction();

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="digit">桁数</param>
        /// <param name="name">ユーザ名</param>
        public CPU2(int digit, string name)
        {
            this.digit = digit;
            this.name = name;
            list = nf.creatList(digit);
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
            if (firstFlag)
            {
                pre = nf.CreateNum(3);
                firstFlag = false;
            }
            else
            {
                list = nf.deleteList(pre, eatBite, list, digit);
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i])
                    {
                        pre = nf.ToNumeloValue(i, digit);
                        break;
                    }
                }
            }
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
        /// ユーザ名の取得
        /// </summary>
        /// <returns>ユーザ名</returns>
        public string getName() { return name; }

        /// <summary>
        /// 桁数の取得
        /// </summary>
        /// <returns>桁数</returns>
        public int getDigit() { return digit; }
    }
}

