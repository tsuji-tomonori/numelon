using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    interface IPrayer
    {
        /// <summary>
        /// ゲーム開始前の処理(自分の数の設定)
        /// </summary>
        void Start();

        /// <summary>
        /// 自分の手の決定
        /// 1回目の eatBite の値はでたらめになるため別途処理が必要
        /// </summary>
        /// <param name="eatBite">前回の判定結果</param>
        /// <returns>決定した手</returns>
        int[] Call(int[] eatBite);

        /// <summary>
        /// その手のEatBiteを判定する
        /// </summary>
        /// <param name="question">入力された手</param>
        /// <returns>判定結果</returns>
        int[] Div(int[] question);

        /// <summary>
        /// 桁数の取得
        /// </summary>
        /// <returns>桁数</returns>
        int getDigit();

        /// <summary>
        /// ユーザ名の取得
        /// </summary>
        /// <returns>名前</returns>
        string getName();

        /// <summary>
        /// 答えの取得
        /// </summary>
        /// <returns>答え</returns>
        int[] getAns();

    }
}
