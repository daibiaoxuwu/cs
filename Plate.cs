using System;
using System.IO;

namespace cs
{
    abstract class Plate
    {
        static Piece[][] plate;
        public static void init(){
            plate = new Piece[15][];
            for(int i = 0; i < 15; ++ i){
                plate[i] = new Piece[15];
            }
            for(int player = 0; player < 2; ++ player){
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
                }
            }
        }

        public static void print(){
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(" 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5");
            for(int i = 0; i < 15; ++ i){
                if(i<5) Console.BackgroundColor = ConsoleColor.Blue;
                else if(i>9) Console.BackgroundColor = ConsoleColor.Red;
                else Console.BackgroundColor = ConsoleColor.Black;

                if(i<9) Console.Write(" ");
                Console.Write((i+1).ToString());

                for(int j = 0; j < 15; ++ j){
                    if(plate[i][j]==null){
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("  ");
                    } else{
                        Console.BackgroundColor = (plate[i][j].player==0 ? ConsoleColor.Blue : ConsoleColor.Red);
                        Console.Write(plate[i][j].name());
                    }
                }
                if(i<5) Console.BackgroundColor = ConsoleColor.Blue;
                else if(i>9) Console.BackgroundColor = ConsoleColor.Red;
                else Console.BackgroundColor = ConsoleColor.Black;
                if(i<9) Console.Write(" ");
                Console.Write((i+1).ToString());
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5");
            Console.BackgroundColor = ConsoleColor.Black;
        }

    }
}
