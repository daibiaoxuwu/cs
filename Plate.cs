using System;
using System.IO;

namespace cs
{
    abstract class Plate
    {
        static Piece[][] plate;
        static ConsoleColor[][] plateCol;
        public static void init(){
            plate = new Piece[15][];
            for(int i = 0; i < 15; ++ i){
                plate[i] = new Piece[15];
            }
            for(int player = 0; player < 2; ++ player){
                /*
                if (!File.Exists("1.CSV")){
                    Console.WriteLine("找不到布局文件1.CSV。请将其放在本程序同一目录下。");
                    Console.ReadKey();
                    return;
                }
                FileStream fs = new FileStream("1.CSV", FileMode.Open);
                StreamReader reader = new StreamReader(fs);
                string line = string.Empty;
                int xpos = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] lines = line.Split(",");
                    for(int i = 0; i < 15; ++ i){
                        if(lines[i].Equals("S")){ //Sword
                            Sword sword = new Sword();
                            sword.player = player;
                            plate[(player==0?xpos:15-xpos)][i] = sword;
                        }
                    }
                    xpos++;
                }*/


                plateCol = new ConsoleColor[15][];
                for(int i = 0; i < 15; ++ i){
                    plateCol[i] = new ConsoleColor[15];
                    for(int j = 0; j < 15; ++ j){
                        plateCol[i][j] = ConsoleColor.Black;
                    }
                }
                Sword sword = new Sword();
                sword.player = 0;
                plate[0][0] = sword;
                
                sword = new Sword();
                sword.player = 1;
                plate[0][2] = sword;

                colRefresh();
                calMove();
            }

            
        }
        public static void colRefresh(){
            for(int i = 0; i < 15; ++ i){
                for(int j = 0; j < 15; ++ j){
                    plateCol[i][j] = ConsoleColor.Black;
                }
            }
        }

        public static void print(){
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("玩家"+Program.player.ToString());
            Console.WriteLine("   1 2 3 4 5 6 7 8 9 0 1 2 3 4 5  ");
            for(int i = 0; i < 15; ++ i){
                if(i<5) Console.ForegroundColor = ConsoleColor.Blue;
                else if(i>9) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = ConsoleColor.White;

                if(i<9) Console.Write(" ");
                Console.Write((i+1).ToString());

                for(int j = 0; j < 15; ++ j){
                    Console.BackgroundColor = plateCol[i][j];
                    if(i==Program.curx && j==Program.cury){
                        Console.BackgroundColor = (Program.player==0 ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed);
                    } 
                           
                    if(plate[i][j]==null){
                        Console.Write("  ");
                    } else{
                        Console.ForegroundColor=(plate[i][j].player==0 ? ConsoleColor.Blue : ConsoleColor.Red);
                        Console.Write(plate[i][j].getName());
                    }
                }
                if(i<5) Console.ForegroundColor = ConsoleColor.Blue;
                else if(i>9) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = ConsoleColor.White;
                if(i<9) Console.Write(" ");
                Console.WriteLine((i+1).ToString());
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("   1 2 3 4 5 6 7 8 9 0 1 2 3 4 5  ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void calMove(){ //判断光标处棋子的移动，修改plateCol
            Piece piece = plate[Program.curx][Program.cury];
            if(piece==null || piece.player!=Program.player) return;
            if(piece.getName()==" S"){
                piece.walk(Program.curx, Program.cury);
            }
        }
        public static bool inside(int x, int y){
            return(x>=0 && x<15 && y>=0 && y<15);
        }
        public static ConsoleColor playerCol(){
            return  (Program.player==0 ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed);
        }

        public static void walk(int x, int y, int steps, int maxsteps, int origx, int origy){ //计算棋子移动范围：默认的走路
            if(!(inside(x,y)) || steps<0 ) return;
            if (steps!=maxsteps && plate[x][y]!=null && plate[x][y].player!=Program.player){
                string atkLevel = plate[origx][origy].getAtkLevel();
                if(atkLevel=="粉碎" || (atkLevel=="刺杀" && plate[x][y].getDefLevel()!="机械")){
                    plateCol[x][y]=ConsoleColor.DarkYellow;
                    return;
                }
                switch(plate[x][y].getDefLevel()){
                    case "无":
                        plateCol[x][y]=ConsoleColor.DarkYellow;
                        return;
                    case "轻甲":
                        if(Math.Abs(x-origx)!=Math.Abs(y-origy))
                            plateCol[x][y]=ConsoleColor.DarkYellow;
                        return;
                    case "重甲":
                        if(x!=origx && y!=origy)
                            plateCol[x][y]=ConsoleColor.DarkYellow;
                        return;
                    case "机械":
                        return;
                }
            } else {
                plateCol[x][y]=ConsoleColor.DarkGray;
            }
            walk(x-1,y,steps-1,maxsteps, origx, origy);
            walk(x+1,y,steps-1,maxsteps, origx, origy);
            walk(x,y-1,steps-1,maxsteps, origx, origy);
            walk(x,y+1,steps-1,maxsteps, origx, origy);
        }

        public static int selx, sely;
        public static bool selectPiece(){
            Piece piece = plate[Program.curx][Program.cury];
            if(piece!=null && piece.player==Program.player){
                selx=Program.curx;
                sely=Program.cury;
                return true;
            }
            return false;
        }

        public static bool move(){
            if(Program.curx==selx && Program.cury == sely) return false;
            if(plateCol[Program.curx][Program.cury]==ConsoleColor.DarkGray ||
                    plateCol[Program.curx][Program.cury]==ConsoleColor.DarkYellow){
                plate[Program.curx][Program.cury]=plate[selx][sely];
                plate[selx][sely]=null;
                return true;
            }
            return false;  
        }
    }
}
