using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RLMapLoader
{
    public partial class Form1 : Form
    {
        private Memory rlMemory;
        IntPtr trainingMapAddress;

        public Form1()
        {
            InitializeComponent();
            InitializeMemoryAddresses();
            
        }

        private void InitializeMemoryAddresses()
        {
            Process[] processes = Process.GetProcessesByName("RocketLeague");
            if (processes.Length > 0)
            {
                rlMemory = new Memory(processes[0]);
                trainingMapAddress = rlMemory.GetAddress("\"RocketLeague.exe\"+0164955C+464+54c+6a8+114+714");
                byte[] buff = new byte[6];
                rlMemory.ReadMemory(trainingMapAddress, buff, 6);
                currentFreeplayMapTextBox.Text = System.Text.Encoding.Default.GetString(buff);
            }
            else
            {
                MessageBox.Show("Rocket League isn't running...");
            }
        }

        public void changeFreePlayMap(String mapName)
        {
            if(mapName.Length != 6)
            {
                MessageBox.Show("Map Name must be 6 characters!");
                return;
            }
            byte[] mapNameBytes = System.Text.Encoding.UTF8.GetBytes(mapName);
            rlMemory.WriteMemory(trainingMapAddress, mapNameBytes, 6);
        }

        private void loadMapButton_Click(object sender, EventArgs e)
        {
            if(rlMemory != null)
            {
                changeFreePlayMap(currentFreeplayMapTextBox.Text);
            }
            else
            {
                InitializeMemoryAddresses();
            }
        }

        private void saveMapNameButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I don't do anything yet.");
        }
    }
}
