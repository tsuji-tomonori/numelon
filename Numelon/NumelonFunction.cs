using System;
using System.Collections.Generic;
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
            for (int i = 0; i < numFlag.Length; i++){ numFlag[i] = true; }

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
                    if(count == digit) { finFlag = true; }
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
            for(int i = 0; i < question.Length; i++)
            {
                for(int j = 0; j < answer.Length; j++)
                {
                    if(question[i] == answer[j])
                    {
                        //Eat
                        if(i == j) { eatBite[0]++; }
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
    }
}
