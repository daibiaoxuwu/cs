using System;

namespace cs
{
    class Sword : Piece
    {
        public override string getName(){return " S";}
        public override string getDefLevel(){return "轻甲";}
        public override bool isAgile(){return true;}

        public override void walk(int x, int y){ //计算棋子移动范围 
            Plate.walk(x,y,2,2,x,y);
        }
    }
}
