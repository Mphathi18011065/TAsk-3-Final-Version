using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2_18011065_MphathiMaapola
{
    class WizardUnit : Unit
    {
        //isDead field used for Death method
        public bool IsDead { get; set; }

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



        public WizardUnit(int x, int y, int h, int s, int a, int f, string sy, string n)// wizard constructor called in the map class

        {
            XPos = x;
            YPos = y;
            Health = h;
            base.maxHealth = h;
            Speed = s;
            Attack = a;
            AttackRange = 1; //Uses Taxicab distance
            base.faction = f;
            Symbol = sy;
            IsAttacking = false;
            IsDead = false;
            name = n;


        }

        public override void Death()
        {
            // Called when wizard is killed 
            symbol = "#";
            IsDead = true;
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
            else if (attacker is WizardUnit)
            {
                WizardUnit wu = (WizardUnit)attacker;
                Health = Health - (wu.Attack - wu.AttackRange); // Using the wizards attack range which is 1 block to attack other wizard units

            }

            if (Health <= 0)
            {
                Death(); //Calls the death methos when the wizard is about to die 
            }
        }



        public override (Unit, int) Closest(List<Unit> units)
        {
            int shortest = 1;
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
                    if (distance < shortest)
                    {
                        shortest = distance;
                        closest = otherRu;
                    }
                }
                else if (u is WizardUnit && u != this)
                {
                    WizardUnit otherRu = (WizardUnit)u;
                    int distance = Math.Abs(this.XPos - otherRu.XPos)
                               + Math.Abs(this.YPos - otherRu.YPos);
                    if (distance < shortest)
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
            temp += "Name : " + name; 
            temp += "Melee:";
            temp += "{" + Symbol + "}";
            temp += "(" + XPos + "," + YPos + ")";
            temp += Health + ", " + Attack + ", " + AttackRange + ", " + Speed;
            temp += (IsDead ? " DEAD!" : " ALIVE!");
            return temp;
        }

        public override bool InRange(Unit other, Building building)
        {
            int distance = 0;
            int otherX = 0;
            int otherY = 0;
            if (other is MeleeUnit) //Checking if the Melee unit is in range 
            {
                otherX = ((MeleeUnit)other).XPos;
                otherY = ((MeleeUnit)other).YPos;
            }
            else if (other is RangedUnit) //Checking if the Ranged units are in range 
            {
                otherX = ((RangedUnit)other).XPos;
                otherY = ((RangedUnit)other).YPos;
            }
            else if (other is WizardUnit) //Checking if the wizard is in range 
            {
                otherX = ((WizardUnit)other).XPos;
                otherY = ((WizardUnit)other).YPos;
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

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
