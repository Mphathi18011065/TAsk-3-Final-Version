using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace Task2_18011065_MphathiMaapola
{
    [Serializable]
    class ResourceBuilding : Building
    {
        public string Name
        {
            get { return base.name; }
            set { base.name = value; }
        }

        public int Xpos
        {
            get { return base.xPos; }
            set { base.xPos = value; }
        }
        public int Ypos
        {
            get { return base.yPos; }
            set { base.yPos = value; }
        }

        public int Health
        {
            get { return base.health; }
            set { base.health = value; }
        }
        public int Faction
        {
            get { return base.faction; }
            set { base.faction = value; }
        }

        public string Symbol
        {
            get { return base.symbol; }
            set { base.symbol = value; }

        }


        
        string resourceType;
        int resourcesGenerated = 0;
      
        int resourcesPerRound; //Generated Per Round
        int resourcePool = 60;
        public bool isDestroyed { get; set; }
        List<int> Resources = new List<int>(60);
        Random r = new Random();


        public ResourceBuilding(int v1, int v2, int v3, int v6, string v4, string v5) // Factory constructor 
        {
            Xpos = v1;
            Ypos = v2;
            Faction = v3;
            Symbol = v4;
            Name = v5;
            Health = v6; // Factory health 

            //  GenerateUnits(20);
            //  Display();
        }



        public override void Destrcution()
        {
            Symbol = "!";
            isDestroyed = true;
        }

        public override string ToString()
        {
            string temp = "";
            temp += "{" + Symbol + "}";
            temp += "{" + resourceType + "}";
            temp += "{ Amount of resources generated :" + resourcePool.ToString();
            temp += "{ Resources per Round " + resourcesPerRound;
            temp += "(" + Xpos + "," + Ypos + ")";
            temp += " (" + Health + ")";
            temp += (isDestroyed ? " FUNCTIONAL !" : " UNFUNCTIONAL!");

            return temp;

        }

        public void GenerateResources(int rounds, Label lbl ,Label lbl2)
        {
            // Once resources are depleted building is destroyed 

            do
            {
                for (int i = 0; i < resourcePool; i++)
                {
                    Resources.Remove(i);
                      //Buidling destroys itself after it runs out of rsources 
                    if (i>resourcePool)
                    {
                        isDestroyed = true;
                    }

                      // LAbels to show Resources 
                    lbl.Text = "Resources :" + Resources.Count.ToString();
                    lbl.Text = "Resources Aquired : " + i.ToString();
                }
            } while (rounds < resourcePool);

        }

        public override void Save(Building b)
        {
            
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
                Destrcution();
            }
        }
    }
}
