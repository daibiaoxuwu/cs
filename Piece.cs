using System;

namespace cs
{
    abstract class Piece
    {
        public int player;
        public abstract string getName();
        public abstract string getDefLevel();
        public abstract string getAtkLevel();
        public abstract void walk(int x, int y);
    }
}
