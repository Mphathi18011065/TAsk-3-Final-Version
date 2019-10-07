using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Task2_18011065_MphathiMaapola

{
    [Serializable]
    public partial class Form1 : Form
    {
        GameEngine engine;
        Map map;
        ResourceBuilding resources;
        FactoryBuilding factb;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            engine = new GameEngine(20, txtInfo, grpMap);
            factb = new FactoryBuilding(grpMap);

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblRound.Text = "Round  : " + engine.Round.ToString();
            // UPdates the resources per round
           // resources = new ResourceBuilding();
           // resources.GenerateResources(engine.Round, lblResource, lblAr);

            engine.Update();
        }


        private void BtnSave_Click_1(object sender, EventArgs e)
        {
            map = new Map(20, txtInfo);
                map.Save();
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            grpMap.Size = new Size(trackBar1.Value, trackBar1.Value);
            mapSize.Text = trackBar1.Value.ToString();

        }
    }
}
