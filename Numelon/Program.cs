using System;
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
            /*宣言*/
            NumelonFunction nf = new NumelonFunction();
            IPrayer a = new Human();
            IPrayer b = new ArtificialIncompetence(a.getDigit(), "com");
            int[] eatBite = new int[2];
            int[] call = new int[a.getDigit()];
            string winner = "";

            /*ゲーム開始前処理*/
            a.Start();
            b.Start();

            Console.WriteLine("Game Start!!");
            
            /*どちらかがゲームに勝利するまでループ*/
            while (true)
            {
                /*Player a*/
                call = a.Call(eatBite);
                eatBite = b.Div(call);
                Console.WriteLine(a.getName()+" : "+ nf.ToString(call));
                Console.WriteLine(a.getName() + " : " + nf.ToString(eatBite));
                /*勝利したとき*/
                if (eatBite[0] == 3)
                {
                    winner = a.getName();
                    break;
                }

                /*Player b*/
                call = b.Call(eatBite);
                eatBite = a.Div(call);
                Console.WriteLine(b.getName() + " : " + nf.ToString(call));
                Console.WriteLine(b.getName() + " : " + nf.ToString(eatBite));
                /*勝利したとき*/
                if (eatBite[0] == 3)
                {
                    winner = b.getName();
                    break;
                }
            }

            Console.WriteLine("*************************************");
            Console.WriteLine("winner is " + winner);
        }
    }
}
