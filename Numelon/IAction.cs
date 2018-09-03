using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    interface IAction
    {
        /// <summary>
        /// そのクラスの行動を表すメソッド
        /// 情報はこの後取得する
        /// </summary>
        void action();
    }
}
