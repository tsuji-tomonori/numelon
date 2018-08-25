using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class NumelonValueList
    {
        /*宣言*/
        private bool[] basic;
        private bool[] list;
        private int[] convert;
        private int digit;
        NumelonFunction nf = new NumelonFunction();

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="digit">作成するリストの桁数</param>
        public NumelonValueList(int digit)
        {
            this.digit = digit;
            basic = nf.creatList(this.digit);
            preList();
            preConvert();
            
        }

        /// <summary>
        /// リストの作成・初期化
        /// </summary>
        private void preList()
        {
            list = new bool[nPr(10, this.digit)];
            for(int i = 0; i < list.Length; i++)
            {
                list[i] = true;
            }
        }

        /// <summary>
        /// 変換用配列の作成・初期化
        /// 先にbasic配列を作成する必要有
        /// </summary>
        private void preConvert()
        {
            convert = new int[nPr(10, digit)];
            int count = 0;
            for(int i = 0; i < basic.Length; i++)
            {
                if (basic[i])
                {
                    convert[count] = i;
                    count++;
                }
            }
        }

        /// <summary>
        /// リストの取得
        /// このリストは指定した桁数から最適の大きさで作成される
        /// そのためリストのindexはあてにならない
        /// </summary>
        /// <returns>リスト</returns>
        public bool[] getList()
        {
            return list;
        }

        /// <summary>
        /// NumelonValueList list のindexからnumelon値を取得する関数
        /// index は 0 以上 10Pdigit 以下になるようにする
        /// </summary>
        /// <param name="index">listのindex</param>
        /// <returns>ヌメロン値(int)</returns>
        public int indexToValue(int index)
        {
            return convert[index];
        }

        /// <summary>
        /// ヌメロン値(int) から NumelonValueList listのindexを取得する関数
        /// もしない場合0を返す
        /// </summary>
        /// <param name="value">ヌメロン値(int)</param>
        /// <returns>listのindex</returns>
        public int valueToIndex(int value)
        {
            return Array.IndexOf(convert, value);
        }

        /// <summary>
        /// nf.creatList(digit)にて作成したリストを返す
        /// </summary>
        /// <returns>作成したリスト</returns>
        public bool[] getBasic()
        {
            return basic;
        }

        /// <summary>
        /// n!
        /// </summary>
        /// <param name="n">n</param>
        /// <returns>n!</returns>
        public int Factorial(int n)
        {
            if (n == 0)
                return 1;
            return n * Factorial(n - 1);
        }

        /// <summary>
        /// nPr
        /// </summary>
        /// <param name="n">n</param>
        /// <param name="r">r</param>
        /// <returns>nPr</returns>
        public int nPr(int n, int r)
        {
            return Factorial(n) / Factorial(n - r);
        }



    }
}
