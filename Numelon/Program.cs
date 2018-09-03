using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numelon
{
    class Program
    {

        static void Main(string[] args)
        {
            NumelonFunction nf = new NumelonFunction();
            IAction actMode = null;
            Console.WriteLine("実行モードを選んでください play : 1 , test : 2 , 終了 : 0");
            switch (nf.scanNum(0, 2))
            {
                case 0:
                    Environment.Exit(0);
                    break;

                case 1:
                    actMode = new NumelonAction();
                    break;

                case 2:
                    actMode = new Test();
                    break;

            }
            actMode.action();
        }

    }
}
