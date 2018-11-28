using System;

namespace cs
{
    class Shield : Piece
    {
        
        public override string getName(){return "盾";}
        public override string getDefLevel(){return "盾牌";}
        public override string getPrompt(){return "U-防御";}

        public override void calSkill(int selx, int sely){
            int x = selx, y = sely;
            for(int i = 1; i <= 15; ++ i){
                x+=(Program.curx-selx);
                y+=(Program.cury-sely);
                if(!Plate.inside(x,y)) return;
                Plate.plateCol[x][y]=ConsoleColor.DarkGray;
            }
        }
        public override bool releaseSkill(int srcx, int srcy, int dstx, int dsty){
            waitx=dstx-srcx;
            waity=dsty-srcy;
            return true;
        }
    }
}
