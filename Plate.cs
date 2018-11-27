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
                    if(plate[i][j]==null){
                        plateCol[i][j] = ConsoleColor.Black;
                    } else if(plate[i][j].player==0){
                        plateCol[i][j] = ConsoleColor.DarkBlue;
                    } else {
                        plateCol[i][j] = ConsoleColor.DarkRed;
                    }
                }
            }
        }

        public static void print(){
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("   1 2 3 4 5 6 7 8 9 0 1 2 3 4 5  ");
            for(int i = 0; i < 15; ++ i){
                if(i<5) Console.BackgroundColor = ConsoleColor.DarkBlue;
                else if(i>9) Console.BackgroundColor = ConsoleColor.DarkRed;
                else Console.BackgroundColor = ConsoleColor.Black;

                if(i<9) Console.Write(" ");
                Console.Write((i+1).ToString());

                for(int j = 0; j < 15; ++ j){
                    Console.BackgroundColor = plateCol[i][j];
                    if(i==Program.curx && j==Program.cury){
                        if(plate[i][j]!=null && Program.player != plate[i][j].player){
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                        } else {
                            Console.BackgroundColor = (Program.player==0 ? ConsoleColor.Blue : ConsoleColor.Red);
                        }
                    } 
                           
                    if(plate[i][j]==null){
                        Console.Write("  ");
                    } else{
                        Console.Write(plate[i][j].getName());
                    }
                }
                if(i<5) Console.BackgroundColor = ConsoleColor.DarkBlue;
                else if(i>9) Console.BackgroundColor = ConsoleColor.DarkRed;
                else Console.BackgroundColor = ConsoleColor.Black;
                if(i<9) Console.Write(" ");
                Console.WriteLine((i+1).ToString());
            }
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("   1 2 3 4 5 6 7 8 9 0 1 2 3 4 5  ");
            Console.BackgroundColor = ConsoleColor.Black;
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

        public static void walk(int x, int y, int steps, int maxsteps, string atkLevel){ //计算棋子移动范围：默认的走路
            if(!(inside(x,y)) || steps<0 || (steps!=maxsteps && plate[x][y]!=null)) return;
            plateCol[x][y]=ConsoleColor.DarkGray;
            walk(x-1,y,steps-1,maxsteps,atkLevel);
            walk(x+1,y,steps-1,maxsteps,atkLevel);
            walk(x,y-1,steps-1,maxsteps,atkLevel);
            walk(x,y+1,steps-1,maxsteps,atkLevel);
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
            if(plateCol[Program.curx][Program.cury]==ConsoleColor.DarkGray){
                plate[Program.curx][Program.cury]=plate[selx][sely];
                plate[selx][sely]=null;
                return true;
            }
            return false;  
        }
    }
}
