using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task2_18011065_MphathiMaapola
{

    public abstract class Unit
    {
        protected int xPos;
        protected int yPos;
        protected int health;
        protected int maxHealth;
        protected int speed;
        protected int attack;
        protected int attackRange;
        protected int faction;
        protected string symbol;
        protected bool isAttacking;
        protected string name;

        public abstract void Move(int dir);
        public abstract void Combat(Unit attacker);
        
        public abstract bool InRange(Unit other, Building building);
        public abstract (Unit, int) Closest(List<Unit> units);
        
        public abstract void Death();
        public abstract override string ToString();

        public abstract void Save();
        //public abstract void Naming ();
    }
}
