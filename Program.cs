using System;

namespace cs
{
    class Program
    {
        static void init(){
            Console.WriteLine("这是cyvasse的命令行版本。为了更好的体验，请将此窗口全屏。");
            Console.WriteLine("下面将要测量此窗口的宽度。");
            Console.WriteLine("按A 确定 S 返回");
            string answer=Console.ReadKey().Key.ToString();
            if(answer=="S"){
                Console.WriteLine("程序结束。");
                return;
            } 
            for(int i = 0; i < 50; ++i){
                Console.WriteLine((52-i));
            }
            Console.WriteLine("2 请输入你在屏幕顶端看到的数字：");
            answer = Console.ReadLine();
            LINENUM = int.Parse(answer);
            Console.WriteLine("您的窗口共有" + LINENUM + "行。即将进入游戏。");
            Console.WriteLine("按A 确定 S 返回");
            answer=Console.ReadKey().Key.ToString();
            if(answer=="S"){
                Console.WriteLine("程序结束。");
                return;
            }
            LINENUM = 20;
        }

        static int LINENUM = 0;
        static void Main(string[] args)
        {
            // init();
            Plate.init();
            Plate.print();
        }
    }
}
