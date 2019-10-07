using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task2_18011065_MphathiMaapola
{
    [Serializable]
    public abstract class Building
    {
        protected int xPos;
        protected int yPos;
        protected int health;
        protected int maxHealth;
        protected int faction;
        protected string symbol;
        protected string name;

        public Building()
        { }

        public Building(int x, int y, int h, int f, string s)
        {
            xPos = x;
            yPos = y;
            health = h;
            faction = f;
            symbol = s;
        }

        public abstract void Destrcution();
        public abstract void Combat(Unit attacker);
       
       
        public abstract override string ToString();

        public abstract void Save(Building b);


    }
}
