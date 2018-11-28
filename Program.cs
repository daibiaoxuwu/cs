﻿using System;

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


        static void turnTurn(){
            Plate.turnTurn();
            mode=0;
            player=1-player;
            Plate.colRefresh();
        }

        static void Main(string[] args)
        {
            // init();
            Plate.init();
            


            string lastSkill="";
            while(true){
                Plate.print();
                if(mode==0){                
                    Console.WriteLine("ASDF-移动 J-选择棋子");
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
                    Plate.calMove(curx, cury);
                } else if(mode==1){
                    Console.WriteLine("ASDF-移动 J-行动 K-取消选择 " + Plate.getPrompt());
                    string answer=Console.ReadKey().Key.ToString();
                    if(answer=="W" && curx>0) curx--;
                    else if(answer=="S" && curx<14) curx++;
                    else if(answer=="D" && cury<14) cury++;
                    else if(answer=="A" && cury>0) cury--;
                    else if(answer=="K"){
                        mode=0;
                        Plate.colRefresh();
                        continue;
                    } else {
                        int response = Plate.listenKey(answer);
                        if(response==0){ //移动，返回0
                            turnTurn();
                            continue;
                        } else if(response!=-1){ //不是无效按键
                            lastSkill=answer;
                            mode=response;
                            Plate.colRefresh();
                            Plate.calSkill();
                            continue;
                        }
                    }
                } else if(mode==2){ //技能
                    Console.WriteLine("ASDF-移动 J-释放"+lastSkill+"技能 K-取消释放");
                    string answer=Console.ReadKey().Key.ToString();
                    if(answer=="W" && curx>0) curx--;
                    else if(answer=="S" && curx<14) curx++;
                    else if(answer=="D" && cury<14) cury++;
                    else if(answer=="A" && cury>0) cury--;
                    else if(answer=="K"){
                        mode=1;
                        Plate.colRefresh();
                        Plate.calMove(Plate.selx, Plate.sely);
                        continue;
                    } else if(answer=="J") {
                        if(Plate.releaseSkill()){
                            turnTurn();
                            continue;
                        }
                    }
                    Plate.colRefresh();
                    Plate.calSkill();
                }
            }
            
        }
    }
}
