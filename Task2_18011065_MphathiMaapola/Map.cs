using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Task2_18011065_MphathiMaapola
{
    [Serializable]

    public class Map
    {
        //Serialize the whole map 

        List<Unit> units;
        List<Building> buildings;
   
        Form1 f1 = new Form1();



        Random r = new Random();
        int numUnits = 0;
        TextBox txtInfo;

        public List<Unit> Units
        {
            get { return units; }
            set { units = value; }
        }

        public List<Building> Buildings
        {
            get { return buildings; }
            set { buildings = value; }

        }



 
        public void Generate()
        {
            
            for (int i = 0; i < numUnits; i++)
            {
                if (r.Next(0, 2) == 0) //Generate Melee Unit
                {
                    MeleeUnit m = new MeleeUnit(r.Next(0,20 ),
                                                r.Next(0, 20),
                                                100,
                                                1,
                                                20,
                                                (i % 2 == 0 ? 1 : 0),// determines the faction
                                                "%",
                                                "");
                    units.Add(m);
                }
                else // Generate Ranged Unit
                {
                    RangedUnit ru = new RangedUnit(r.Next(0,20),
                                                r.Next(0, 20),
                                                100,
                                                1,
                                                20,
                                                5,
                                                (i % 2 == 0 ? 1 : 0),
                                                "~",
                                                "");
                    units.Add(ru);
                }           
            }
            //Generate factory 
            // generates half of the units value
            for (int j = 0; j < numUnits/2; j++)
            {
                if (r.Next(0, 2) == 0)// generate factory 
                {
                    FactoryBuilding f = new FactoryBuilding(r.Next(0, 20), r.Next(0, 20), (j % 2 == 0 ? 1 : 0), 20, "F", "Factory Building");
                    buildings.Add(f);
                }
            }
            //Generate Resources
            // generates half of the amount of  units
            for (int x = 0; x < numUnits/2 ; x++)
            {
                if (r.Next(0, 2) == 0)
                {
                    ResourceBuilding br = new ResourceBuilding(r.Next(0, 20), r.Next(0, 20), (x % 2 == 0 ? 1 : 0), 100, "[R.]", "Resource Building");
                    buildings.Add(br);
                }
            }
            // Generate wizards 
            // Generates num of units divided by 3 
            // The neutral class is in the normal units 
            for (int y = 0; y <numUnits ; y++)
            {
                if (r.Next(0,3)==0)
                {
                    WizardUnit w = new WizardUnit(r.Next(0, 20),r.Next(0, 20), 100,1,20, (y % 3 == 0 ? 1 : 0),// determines the faction
                                                "W","Wizards");
                    units.Add(w);
                }
            }
            // Generate Neutral Team 
            
        }

        public void Display(GroupBox groupBox)
        {
            groupBox.Controls.Clear();
            foreach (Unit u in units)
            {
                Button b = new Button();
                if (u is MeleeUnit)
                {
                    MeleeUnit mu = (MeleeUnit)u;
                    b.Size = new Size(20, 20);
                    b.Location = new Point(mu.XPos * 20, mu.YPos * 20);
                    b.Text = mu.Symbol;

                    if (mu.Faction == 0)
                    {
                        b.ForeColor = Color.AliceBlue;
                    }
                    else
                    {
                        b.ForeColor = Color.Green;
                    }
                    // Naming Melle Units  created 
                    if (mu.Faction == 0)
                    {
                        mu.Name = "Swordsmen";
                    }
                    else
                    {
                        mu.Name = "Paladin";
                    }
                }
                else if (u is WizardUnit)
                {

                    if (u is WizardUnit)
                    {
                        WizardUnit wu = (WizardUnit)u;
                        b.Size = new Size(20, 20);
                        b.Location = new Point(wu.XPos * 20, wu.YPos * 20);
                        b.Text = "W";

                        if (wu.Faction == 0)
                        {
                            b.ForeColor = Color.AliceBlue;
                            // naming wizards 
                            wu.Name = "Grand Viziour";
                        }
                        else if (wu.Faction == 2)
                        {
                            b.ForeColor = Color.Green;
                            // naming wizards 
                            wu.Name = "Grand Walkman";
                        }
                        else if (wu.Faction == 3)
                        {
                            b.ForeColor = Color.DarkKhaki;
                            b.Text = "N";
                            wu.Name = "Peace Shitters";

                        }


                        }
               }
                else if (u is RangedUnit)
                {
                    RangedUnit ru = (RangedUnit)u;
                    b.Size = new Size(20, 20);
                    b.Location = new Point(ru.XPos * 20, ru.YPos * 20);
                    b.Text = ru.Symbol;
                    if (ru.Faction == 0)
                    {
                        b.ForeColor = Color.AliceBlue;
                    }
                    else
                    {
                        b.ForeColor = Color.Green;
                    }
                    //NAming Ranged Units 
                    if (ru.Faction == 0)
                    {
                        ru.Name = "Bowsmen";
                    }
                    else
                    {
                        ru.Name = "Sorcerer";
                    }
                }

                b.Click += Unit_Click;                
                groupBox.Controls.Add(b);
            }

            

            foreach (Building b in buildings)
            {
                Button c = new Button();
                if (b is FactoryBuilding)
                {
                   
                    FactoryBuilding factory = (FactoryBuilding)b;
                    c.Size = new Size(20, 20);
                    c.Location = new Point(factory.Xpos * 20, factory.Ypos * 20); //BUilding dont move 
                    c.Text = factory.Symbol;

                    if (factory.Faction == 0)
                    {
                        c.ForeColor = Color.AliceBlue;
                        c.BackColor = Color.Azure;
                        
                    }
                    else
                    {
                        c.ForeColor = Color.Green;
                        c.BackColor = Color.Honeydew;
                      
                    }
                    
                }
                else 
                {
                 
                    ResourceBuilding resource = (ResourceBuilding)b;
                    c.Size = new Size(20, 20);
                    c.Location = new Point(resource.Xpos * 20, resource.Ypos * 20); /// creates custion postions for the values 
                    c.Text = resource.Symbol;

                    if (resource.Faction == 0)
                    {
                        c.ForeColor = Color.AliceBlue; // Differentates between enemy factories
                        c.BackColor = Color.Azure;

                    }
                    else
                    {
                        c.ForeColor = Color.Green;
                        c.BackColor = Color.Honeydew;

                    }
                    
                }
                c.Click += Factory_CLick; 
                groupBox.Controls.Add(c);
            }
          
       

        }

        public Map(int n, TextBox txt)
        {
            units = new List<Unit>();
            buildings = new List<Building>();
            numUnits = n;
            txtInfo = txt;
            

        }

        public void Unit_Click(object sender, EventArgs e)
        {
            int x, y;
            Button b = (Button)sender;
            x = b.Location.X / 20;
            y = b.Location.Y / 20;
            foreach (Unit u in units)
            {
                if (u is RangedUnit)
                {
                    RangedUnit ru = (RangedUnit)u;
                    if (ru.XPos == x && ru.YPos == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = ru.ToString();
                    }
                }
                else if (u is MeleeUnit)
                {
                    MeleeUnit mu = (MeleeUnit)u;
                    if (mu.XPos == x && mu.YPos == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = mu.ToString();
                    }
                }
            }
        }
        public void Factory_CLick(object sender, EventArgs e)
        {
            int x, y;
            Button b = (Button)sender;
            x = b.Location.X / 20;
            y = b.Location.Y / 20;
            foreach (Building building in buildings)
            {
                if (building is FactoryBuilding)
                {
                    FactoryBuilding f = (FactoryBuilding)building;
                    if (f.Xpos == x && f.Ypos == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = f.ToString();
                    }
                }
                else if (building is ResourceBuilding)
                {
                    ResourceBuilding r = (ResourceBuilding)building;
                    if (r.Xpos == x && r.Ypos == y)
                    {
                        txtInfo.Text = "";
                        txtInfo.Text = r.ToString();
                    }
                }
            }

        }

        public void Save()
        {
            //Saves 
            for (int i = 0; i < units.Capacity; i++)
            {
                if (units[i] is MeleeUnit)
                {
                    MeleeUnit mu = (MeleeUnit)units[i];
                    mu.Save();
                }
            }

        }

    }
}
