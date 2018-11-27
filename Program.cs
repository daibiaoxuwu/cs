using System;

namespace cs
{
    class Program
    {
        static void init(){
            Console.WriteLine("这是cyvasse的命令行版本。为了更好的体验，请将此窗口全屏。");
            Console.WriteLine("下面将要测量此窗口的宽度。");
            Console.WriteLine("按J 确定 K 返回");
            string answer=Console.ReadKey().Key.ToString();
            if(answer=="k"){
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
            Console.WriteLine("按J 确定 K 返回");
            answer=Console.ReadKey().Key.ToString();
            if(answer=="k"){
                Console.WriteLine("程序结束。");
                return;
            }
            LINENUM = 20;
        }

        static int LINENUM = 0;
        public static int curx = 0, cury = 0;
        public static int player = 0, mode = 0;
        static void Main(string[] args)
        {
            // init();
            Plate.init();
            


            while(true){
                Plate.print();

                if(mode==0){                
                    Console.WriteLine("ASDF 移动 J 选择棋子");
                    string answer=Console.ReadKey().Key.ToString();
                    if(answer=="W" && curx>0) curx--;
                    else if(answer=="S" && curx<14) curx++;
                    else if(answer=="D" && cury<14) cury++;
                    else if(answer=="A" && cury>0) cury--;
                    else if(answer=="J"){
                        if(Plate.selectPiece()){
                            mode=1;
                            continue;
                        }
                    }
                    Plate.colRefresh();
                    Plate.calMove();
                } else {
                    Console.WriteLine("ASDF 移动 J 行动 K 取消选择");
                    string answer=Console.ReadKey().Key.ToString();
                    if(answer=="W" && curx>0) curx--;
                    else if(answer=="S" && curx<14) curx++;
                    else if(answer=="D" && cury<14) cury++;
                    else if(answer=="A" && cury>0) cury--;
                    else if(answer=="K"){
                        mode=0;
                        Plate.colRefresh();
                        Plate.calMove();
                    } else if(answer=="J"){
                        if(Plate.move()){
                            mode=0;
                            player=1-player;
                            Plate.colRefresh();
                            Plate.calMove();
                            continue;
                        }
                    }
                }

            }
            
        }
    }
}
