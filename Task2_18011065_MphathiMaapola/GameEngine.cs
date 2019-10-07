using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;


namespace Task2_18011065_MphathiMaapola
{
    [Serializable]
    public class GameEngine
    {
        Map map;
        private int round;
        Random r = new Random();
        GroupBox grpMap;
        ResourceBuilding resourceBuilding;
        FactoryBuilding factb;
        Form1 Form1 = new Form1();

        public int Round
        {
            get { return round; }
        }

        public GameEngine(int numUnits, TextBox txtInfo, GroupBox gMap)
        {
            grpMap = gMap;
            map = new Map(numUnits, txtInfo);
            map.Generate();
            map.Display(grpMap);
            factb = new FactoryBuilding(grpMap);
          


            round = 1;
        }

        public void Update() // Display updates
        {
            for (int i = 0; i < map.Units.Count; i++)
            {
                if (map.Units[i] is MeleeUnit)
                {
                    MeleeUnit mu = (MeleeUnit)map.Units[i];

                    if (mu.Health <= mu.MaxHealth * 0.25) // Running Away
                    {
                        mu.Move(r.Next(0, 4));
                    }
                    else
                    {
                        (Unit closest, int distanceTo) = mu.Closest(map.Units);


                        // allows units to attack
                        if (distanceTo <= mu.AttackRange)
                        {
                            mu.IsAttacking = true;
                            mu.Combat(closest);
                        }
                        else //Move around 
                        {
                            if (closest is MeleeUnit)
                            {
                                MeleeUnit closestMu = (MeleeUnit)closest;
                                if (mu.XPos > closestMu.XPos) //North
                                {
                                    mu.Move(0);
                                }
                                else if (mu.XPos < closestMu.XPos) //South
                                {
                                    mu.Move(2);
                                }
                                else if (mu.YPos > closestMu.YPos) //West
                                {
                                    mu.Move(3);
                                }
                                else if (mu.YPos < closestMu.YPos) //East
                                {
                                    mu.Move(1);
                                }
                            }
                            else if (closest is RangedUnit)
                            {
                                RangedUnit closestRu = (RangedUnit)closest;
                                if (mu.XPos > closestRu.XPos) //North
                                {
                                    mu.Move(0);
                                }
                                else if (mu.XPos < closestRu.XPos) //South
                                {
                                    mu.Move(2);
                                }
                                else if (mu.YPos > closestRu.YPos) //West
                                {
                                    mu.Move(3);
                                }
                                else if (mu.YPos < closestRu.YPos) //East
                                {
                                    mu.Move(1);
                                }
                            }
                            else if (closest is WizardUnit)
                            {
                                WizardUnit closestWu = (WizardUnit)closest;

                                if (mu.XPos > closestWu.XPos) //North
                                {
                                    mu.Move(0);
                                }
                                else if (mu.XPos < closestWu.XPos) //South
                                {
                                    mu.Move(2);
                                }
                                else if (mu.YPos > closestWu.YPos) //West
                                {
                                    mu.Move(3);
                                }
                                else if (mu.YPos < closestWu.YPos) //East
                                {
                                    mu.Move(1);
                                }
                            }
                        }

                    }
                }
                else if (map.Units[i] is RangedUnit)
                {
                    RangedUnit ru = (RangedUnit)map.Units[i];

                    (Unit closest, int distanceTo) = ru.Closest(map.Units);

                    //Check In Range
                    if (distanceTo <= ru.AttackRange)
                    {
                        ru.IsAttacking = true;
                        ru.Combat(closest);
                    }
                    else //Move Towards
                    {
                        if (closest is MeleeUnit)
                        {
                            MeleeUnit closestMu = (MeleeUnit)closest;
                            if (ru.XPos > closestMu.XPos) //North
                            {
                                ru.Move(0);
                            }
                            else if (ru.XPos < closestMu.XPos) //South
                            {
                                ru.Move(2);
                            }
                            else if (ru.YPos > closestMu.YPos) //West
                            {
                                ru.Move(3);
                            }
                            else if (ru.YPos < closestMu.YPos) //East
                            {
                                ru.Move(1);
                            }
                        }
                        else if (closest is RangedUnit)
                        {
                            RangedUnit closestRu = (RangedUnit)closest;
                            if (ru.XPos > closestRu.XPos) //North
                            {
                                ru.Move(0);
                            }
                            else if (ru.XPos < closestRu.XPos) //South
                            {
                                ru.Move(2);
                            }
                            else if (ru.YPos > closestRu.YPos) //West
                            {
                                ru.Move(3);
                            }
                            else if (ru.YPos < closestRu.YPos) //East
                            {
                                ru.Move(1);
                            }
                        }
                        else if (closest is WizardUnit)
                        {
                            WizardUnit closestWu = (WizardUnit)closest;

                            if (ru.XPos > closestWu.XPos) //North
                            {
                                ru.Move(0);
                            }
                            else if (ru.XPos < closestWu.XPos) //South
                            {
                                ru.Move(2);
                            }
                            else if (ru.YPos > closestWu.YPos) //West
                            {
                                ru.Move(3);
                            }
                            else if (ru.YPos < closestWu.YPos) //East
                            {
                                ru.Move(1);
                            }
                        }
                    }

                    //  }

                }
                else if (map.Units[i] is WizardUnit)
                {
                    WizardUnit wu = (WizardUnit)map.Units[i];

                    if (wu.Health <= wu.Health * 0.50)

                    {
                        wu.Move(r.Next(0, 4));
                    }
                    else
                    {
                        (Unit closest, int distanceTo) = wu.Closest(map.Units);

                        // allows units to attack
                        if (distanceTo <= wu.AttackRange)
                        {
                            wu.IsAttacking = true;
                            wu.Combat(closest);
                        }
                        else //Move around 
                        {
                            if (closest is MeleeUnit)
                            {
                                MeleeUnit closestMu = (MeleeUnit)closest;
                                if (wu.XPos > closestMu.XPos) //North
                                {
                                    wu.Move(0);
                                }
                                else if (wu.XPos < closestMu.XPos) //South
                                {
                                    wu.Move(2);
                                }
                                else if (wu.YPos > closestMu.YPos) //West
                                {
                                    wu.Move(3);
                                }
                                else if (wu.YPos < closestMu.YPos) //East
                                {
                                    wu.Move(1);
                                }
                            }
                            else if (closest is RangedUnit)
                            {
                                RangedUnit closestRu = (RangedUnit)closest;
                                if (wu.XPos > closestRu.XPos) //North
                                {
                                    wu.Move(0);
                                }
                                else if (wu.XPos < closestRu.XPos) //South
                                {
                                    wu.Move(2);
                                }
                                else if (wu.YPos > closestRu.YPos) //West
                                {
                                    wu.Move(3);
                                }
                                else if (wu.YPos < closestRu.YPos) //East
                                {
                                    wu.Move(1);
                                }
                            }
                            else if (closest is WizardUnit)
                            {
                                WizardUnit closestWu = (WizardUnit)closest;

                                if (wu.XPos > closestWu.XPos) //North
                                {
                                    wu.Move(0);
                                }
                                else if (wu.XPos < closestWu.XPos) //South
                                {
                                    wu.Move(2);
                                }
                                else if (wu.YPos > closestWu.YPos) //West
                                {
                                    wu.Move(3);
                                }
                                else if (wu.YPos < closestWu.YPos) //East
                                {
                                    wu.Move(1);
                                }
                            }
                        }

                    }


                }
                
               // generate resources// resourceBuilding.GenerateResources(Round, formlblResource, lblAr);
                map.Display(grpMap);
                round++;
            }


        }

        public int DistanceTo(Unit a, Unit b)
        {
            int unitDIstance = 0;
            // buttons know how far they are from each other 

            return Distance(a, b, ref unitDIstance);
        }

        //Distance method for Distance To
        private static int Distance(Unit a, Unit b, ref int unitDIstance)
        {
            if (a is MeleeUnit && b is MeleeUnit) // Melee units know how far they are from each other 
            {
                MeleeUnit start = (MeleeUnit)a;
                MeleeUnit end = (MeleeUnit)b;
                unitDIstance = Math.Abs(start.XPos - end.XPos) + Math.Abs(start.YPos - end.YPos);
            }
            else if (a is RangedUnit && b is MeleeUnit) // Melee units and ranged units know how far they are from each other 
            {
                RangedUnit start = (RangedUnit)a;
                MeleeUnit end = (MeleeUnit)b;
                unitDIstance = Math.Abs(start.XPos - end.XPos) + Math.Abs(start.YPos - end.YPos);
            }
            else if (a is RangedUnit && b is RangedUnit) //RAnged iunits know how far they are from eacho ther and enemy units 
            {
                RangedUnit start = (RangedUnit)a;
                RangedUnit end = (RangedUnit)b;
                unitDIstance = Math.Abs(start.XPos - end.XPos) + Math.Abs(start.YPos - end.YPos);
            }
            else if (a is MeleeUnit && b is RangedUnit) // Melee units and Ranged units know how far they are from each otehr 
            {
                MeleeUnit start = (MeleeUnit)a;
                RangedUnit end = (RangedUnit)b;
                unitDIstance = Math.Abs(start.XPos - end.XPos) + Math.Abs(start.YPos - end.YPos);
            }
            else if (a is WizardUnit && b is WizardUnit)
            {
                WizardUnit start = (WizardUnit)a;
                WizardUnit end = (WizardUnit)b;
                unitDIstance = Math.Abs(start.XPos - end.XPos) + Math.Abs(start.YPos - end.YPos);
            }
            else if (a is WizardUnit && b is RangedUnit)
            {
                WizardUnit start = (WizardUnit)a;
                RangedUnit end = (RangedUnit)b;
                unitDIstance = Math.Abs(start.XPos - end.XPos) + Math.Abs(start.YPos - end.YPos);
            }
            else if (a is WizardUnit && b is MeleeUnit)
            {
                WizardUnit start = (WizardUnit)a;
                MeleeUnit end = (MeleeUnit)b;
                unitDIstance = Math.Abs(start.XPos - end.XPos) + Math.Abs(start.YPos - end.YPos);
            }
            return unitDIstance;
        }
    }
}
