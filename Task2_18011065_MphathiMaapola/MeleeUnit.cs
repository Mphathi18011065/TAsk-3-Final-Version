using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Task2_18011065_MphathiMaapola
{
    [Serializable]
    public class MeleeUnit : Unit
    {
        //Zombie field used for Death method
        public bool Zombie { get; set; }

        public int XPos
        {
            get { return base.xPos; }
            set { base.xPos = value; }
        }
        public int YPos
        {
            get { return base.yPos; }
            set { base.yPos = value; }
        }

        public int Health
        {
            get { return base.health; }
            set { base.health = value; }
        }

        public int MaxHealth
        {
            get { return base.maxHealth; }
        }

        public int Attack
        {
            get { return base.attack; }
            set { base.attack = value; }
        }

        public int AttackRange
        {
            get { return base.attackRange; }
            set { base.attackRange = value; }
        }
        public int Speed
        {
            get { return base.speed; }
            set { base.speed = value; }
        }

        public int Faction
        {
            get { return base.faction; }
        }

        public string Symbol
        {
            get { return base.symbol; }
            set { base.symbol = value; }
        }

        public bool IsAttacking
        {
            get { return base.isAttacking; }
            set { base.isAttacking = value; }
        }

        public string Name
        {
            get { return base.name; }
            set { base.name = value; }
        }


        public MeleeUnit(int x, int y, int h, int s, int a, int f, string sy, string n)
        {
            XPos = x;
            YPos = y;
            Health = h;
            base.maxHealth = h;
            Speed = s;
            Attack = a;
            AttackRange = 2; //Uses Taxicab distance
            base.faction = f;
            Symbol = sy;
            IsAttacking = false;
            Zombie = false;
            Name = n;
        }

        public override void Death()
        {
            symbol = "*";
            Zombie = true;
        }

        public override void Move(int dir)
        {
            switch (dir)
            {
                case 0: YPos--; break; //North
                case 1: XPos++; break; //East
                case 2: YPos++; break; //South
                case 3: XPos--; break; //West
                default: break;
            }
        }

        public override void Combat(Unit attacker)
        {
            if (attacker is MeleeUnit)
            {
                Health = Health - ((MeleeUnit)attacker).Attack;
            }
            else if (attacker is RangedUnit)
            {
                RangedUnit ru = (RangedUnit)attacker;
                Health = Health - (ru.Attack - ru.AttackRange);
            }
            
          

            if (Health <= 0)
            {
                Death();
                //R.I.P
            }
        }


        public override bool InRange(Unit other,Building ob)
        {
            int distance = 0;
            int otherX = 0;
            int otherY = 0;
            if (other is MeleeUnit)
            {
                otherX = ((MeleeUnit)other).XPos;
                otherY = ((MeleeUnit)other).YPos;
            }
            else if (other is RangedUnit)
            {
                otherX = ((RangedUnit)other).XPos;
                otherY = ((RangedUnit)other).YPos;
            }
            else if (ob is FactoryBuilding)
            {
                otherX = ((FactoryBuilding)ob).Xpos;
                otherY = ((FactoryBuilding)ob).Ypos;
            }

            distance = Math.Abs(XPos - otherX) + Math.Abs(YPos - otherY);
            if (distance <= AttackRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override (Unit, int) Closest(List<Unit> units)
        {
            int shortest = 100;
            Unit closest = this;
            //Closest Unit and Distance                    
            foreach (Unit u in units)
            {
                if (u is MeleeUnit && u != this)
                {
                    MeleeUnit otherMu = (MeleeUnit)u;
                    int distance = Math.Abs(this.XPos - otherMu.XPos)
                               + Math.Abs(this.YPos - otherMu.YPos);
                    if (distance < shortest)
                    {
                        shortest = distance;
                        closest = otherMu;
                    }
                }
                else if (u is RangedUnit && u != this)
                {
                    RangedUnit otherRu = (RangedUnit)u;
                    int distance = Math.Abs(this.XPos - otherRu.XPos)
                               + Math.Abs(this.YPos - otherRu.YPos);
                    if (distance >= shortest)
                    {
                        shortest = distance;
                        closest = otherRu;
                    }

                }

            }
            return (closest, shortest);
        }
      

        public override string ToString()
        {
            string temp = "";
            temp += "Melee:" + Name;
            temp += "{" + Symbol + "}";
            temp += "(" + XPos + "," + YPos + ")";
            temp += Health + ", " + Attack + ", " + AttackRange + ", " + Speed;
            temp += (Zombie ? " DEAD!" : " ALIVE!");
            return temp;
        }

        public override void Save()
        {
            using(StreamWriter sw = new StreamWriter("Meleeunits.txt"))
            {
              
                sw.WriteLine(SaveString());
            }
            
        }
        public string SaveString()
        {
            string temp = "";
            temp += "Melee:" + Name;
            temp += "{" + Symbol + "}";
            temp += "Co-ordinates (" + XPos + "," + YPos + ")";
            temp += Health + ", " + Attack + ", " + AttackRange + ", " + Speed;
            temp += (Zombie ? " DEAD!" : " ALIVE!");
            return temp;
        }
    }
}
