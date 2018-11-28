using System;

namespace cs
{
    abstract class Piece
    {
        public int player;
        public abstract string getName();
        public virtual string getDefLevel(){return "无";}
        public virtual string getAtkLevel(){return "普通";}
        public virtual bool isAgile(){return false;}//翻墙能力
        public abstract void walk(int x, int y);
    }
}
