using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RLMapLoader
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        static extern bool CreateHardLink(
                            string lpFileName,
                            string lpExistingFileName,
                            IntPtr lpSecurityAttributes
                            );


        private Memory rlMemory;
        IntPtr trainingMapAddress;
        MapPackageManager mpm;

        public Form1()
        {
            InitializeComponent();

            // Initialize Map Package Manager
            mpm = new MapPackageManager();

            // Load Settings
            if (Properties.Settings.Default.RLFolder != "")
            {
                rlDirTextBox.Text = Properties.Settings.Default.RLFolder;
            }
            if (Properties.Settings.Default.ModFolder != "")
            {
                modsDirTextBox.Text = Properties.Settings.Default.ModFolder;
            }
            loadOnStartCheckBox.Checked = Properties.Settings.Default.loadMapOnStart;
            restoreDefaultMapCheckBox.Checked = Properties.Settings.Default.restoreDefaultMapOnClose;

            //InitializeMemoryAddresses();
            InitializeCustomMapList();

            // Add default maps to selection
            mapSelectComboBox.Items.Add("[Default] EuroStadium_P.upk");
            mapSelectComboBox.Items.Add("[Default] EuroStadium_Rainy_P.upk");

            mapSelectComboBox.Items.Add("[Default] HoopsStadium_P.upk");

            mapSelectComboBox.Items.Add("[Default] Labs_CirclePillars_P.upk");
            mapSelectComboBox.Items.Add("[Default] Labs_Cosmic_P.upk");
            mapSelectComboBox.Items.Add("[Default] Labs_DoubleGoal_P.upk");
            mapSelectComboBox.Items.Add("[Default] Labs_Underpass_P.upk");
            mapSelectComboBox.Items.Add("[Default] Labs_Underpass_v0_p.upk");
            mapSelectComboBox.Items.Add("[Default] Labs_Utopia_P.upk");

            mapSelectComboBox.Items.Add("[Default] NeoTokyo_P.upk");

            mapSelectComboBox.Items.Add("[Default] Park_P.upk");
            mapSelectComboBox.Items.Add("[Default] Park_Night_P.upk");
            mapSelectComboBox.Items.Add("[Default] Park_Rainy_P.upk");

            mapSelectComboBox.Items.Add("[Default] Stadium_P.upk");
            mapSelectComboBox.Items.Add("[Default] Stadium_Winter_P.upk"); 

            mapSelectComboBox.Items.Add("[Default] test_Volleyball.upk");

            mapSelectComboBox.Items.Add("[Default] TrainStation_P.upk");
            mapSelectComboBox.Items.Add("[Default] TrainStation_Night_P.upk");

            mapSelectComboBox.Items.Add("[Default] TutorialAdvanced.upk");
            mapSelectComboBox.Items.Add("[Default] TutorialTest.upk");

            mapSelectComboBox.Items.Add("[Default] UtopiaStadium_P.upk");
            mapSelectComboBox.Items.Add("[Default] UtopiaStadium_Dusk_P.upk");

            mapSelectComboBox.Items.Add("[Default] Wasteland_P.upk");

            if (Properties.Settings.Default.lastMap != "") {
                mapSelectComboBox.SelectedItem = Properties.Settings.Default.lastMap;
            } else
            {
                // TO DO: replace this with hash detection to determine actual map, not just default to Park_P when unknown
                mapSelectComboBox.SelectedIndex = mapSelectComboBox.Items.Count - 13;

            }

            if (loadOnStartCheckBox.Checked)
            {
                // Load map, if we had disabled restoring default park_p on last close, don't backup.
                LoadCustomMap(Properties.Settings.Default.restoreDefaultMapOnClose);
            }

        }

        private void InitializeMemoryAddresses()
        {
            Process[] processes = Process.GetProcessesByName("RocketLeague");
            if (processes.Length > 0)
            {
                rlMemory = new Memory(processes[0]);
                //trainingMapAddress = rlMemory.GetAddress("\"RocketLeague.exe\"+0164955C+464+54c+6a8+114+714"); - 1.19
                //trainingMapAddress = rlMemory.GetAddress("\"RocketLeague.exe\"+0164C44C+604+5e4+a8+714+514"); - 1.20
                trainingMapAddress = rlMemory.GetAddress("\"RocketLeague.exe\"+01634DEC+3c+6a8+7c4+54+714"); // 1.21

                byte[] buff = new byte[6];
                rlMemory.ReadMemory(trainingMapAddress, buff, 6);
                //currentFreeplayMapTextBox.Text = System.Text.Encoding.Default.GetString(buff);
            }
            else
            {
                //MessageBox.Show("Rocket League isn't running...");
            }
        }

        public void InitializeCustomMapList()
        {
            try
            {
                string[] filePaths = Directory.GetFiles(Properties.Settings.Default.ModFolder, "*.upk");
                foreach(string mapPath in filePaths)
                {
                    string mapName = Path.GetFileName(mapPath);
                    mapSelectComboBox.Items.Add(mapName);
                }
            }
            catch { }
        }

        public void ChangeFreePlayMap(String mapName)
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
            LoadCustomMap(Properties.Settings.Default.lastMap.Equals("[Default] Park_P.upk"));
            /**
            if(rlMemory != null)
            {
                ChangeFreePlayMap(currentFreeplayMapTextBox.Text);
            }
            else
            {
                InitializeMemoryAddresses();
            }
            **/
        }

        private void saveMapNameButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I don't do anything yet.");
        }


        private void reloadGameButton_Click(object sender, EventArgs e)
        {
            /**
            var processes = Process.GetProcessesByName("RocketLeague");

            if (processes.Length == 0)
            {
                Process.Start(@"C:\Path\To\Your\RocketLeague.exe");
            }

            // Kill the extras
            for (int i = 1; i < processes.Length; i++)
            {
                processes[i].Kill();
            }
            **/
        }

        private Boolean LoadCustomMap(Boolean backupDefaultMap)
        {
            Process[] processes = Process.GetProcessesByName("RocketLeague");
            if (processes.Length > 0)
            {
                statusLabel.Text = "Unable to load map, please exit Rocket League and try again.";
                return false;
            } else if(!Directory.Exists(Properties.Settings.Default.RLFolder))
            {
                statusLabel.Text = "Unable to load map, please select a valid Rocket League Folder.";
                return false;
            }
            else if (!Directory.Exists(Properties.Settings.Default.ModFolder))
            {
                statusLabel.Text = "Unable to load map, please select a valid Mods Folder.";
                return false;
            }
            else
            {
                string newMapPath = "";

                // Make sure the last map loaded wasn't Park_P otherwise we will end up deleting the UPK for no reason.
                if (mapSelectComboBox.SelectedItem.ToString().Equals("[Default] Park_P.upk") && !Properties.Settings.Default.lastMap.Equals("[Default] Park_P.upk"))
                {
                    RestoreOriginalMap();
                    return true;

                } // Park_P is already loaded
                else if (mapSelectComboBox.SelectedItem.ToString().Equals("[Default] Park_P.upk") && Properties.Settings.Default.lastMap.Equals("[Default] Park_P.upk"))
                {
                    statusLabel.Text = "Park_P is already loaded, doing nothing.";
                    return true;
                }
                string parkpPath = Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\Park_P.upk";
                // If default rl map, load from maps folder
                if (mapSelectComboBox.SelectedItem.ToString().Contains("[Default] "))
                {
                    newMapPath = Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\" + mapSelectComboBox.SelectedItem.ToString().Replace("[Default] ", ""); ;
                }
                else
                {
                    newMapPath = Properties.Settings.Default.ModFolder + "\\" + mapSelectComboBox.SelectedItem;

                }
                // Backup original Park_P map if current Park_P map is original
                if (backupDefaultMap)
                {
                    try
                    {
                        File.Copy(parkpPath, Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\Park_P.upk.bak");
                    }
                    catch (IOException ex)
                    {
                        statusLabel.Text = "Backup already exists.";
                    }
                }

                // Create link to selected map
                try
                {
                    File.Delete(parkpPath);
                }
                catch
                {

                }

                statusLabel.Text = mapSelectComboBox.SelectedItem.ToString().Replace("[Default] ", "") + " loaded successfully";

                // Only update last loaded map on load button click...duh
                Properties.Settings.Default.lastMap = mapSelectComboBox.SelectedItem.ToString();
                Properties.Settings.Default.Save();

                return CreateHardLink(parkpPath, newMapPath, IntPtr.Zero);
            

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Revert Park_P back to default map if enabled
            if(Properties.Settings.Default.restoreDefaultMapOnClose)
                RestoreOriginalMap();
        }

        private void RestoreOriginalMap()
        {
            string parkpPath = Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\Park_P.upk";
            string backupParkp = Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\Park_P.upk.bak";
            if (!File.Exists(backupParkp) )
            {
                if (!Properties.Settings.Default.lastMap.Equals("[Default] Park_P.upk"))
                {
                    MessageBox.Show("No backup exists, unable to restore backup.");
                    return;
                } else
                {

                }
            }
            try {
                File.Delete(parkpPath);
            }
            catch
            {
                statusLabel.Text = "Unable to delete current Park_P file.  File was not restored.";
                return;
            }
            try
            {
                File.Copy(Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\Park_P.upk.bak", parkpPath);
                statusLabel.Text = "Original Park_P restored.";
 
            }
            catch (IOException ex)
            {
                statusLabel.Text = "Park_P already exists.";

            }
            mapSelectComboBox.SelectedItem = "[Default] Park_P.upk";
            Properties.Settings.Default.lastMap = "[Default] Park_P.upk";
            Properties.Settings.Default.Save();
        }

        private void selectModsButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                modsDirTextBox.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.ModFolder = modsDirTextBox.Text;

                Properties.Settings.Default.Save();
                statusLabel.Text = "New mod path saved.";
            }
        }

        private void selectRLButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                rlDirTextBox.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.RLFolder = rlDirTextBox.Text;
                Properties.Settings.Default.Save();
                statusLabel.Text = "New Rocket League path saved.";
            }
        }

        private void deleteParkPBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string parkpPath = Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\Park_P.upk";
            string backupParkp = Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\Park_P.upk.bak";

            if (!File.Exists(parkpPath))
            {
                DialogResult result1 = MessageBox.Show("Are you sure you want to delete the backup?  Park_P.upk is missing.",
                                "Delete Backup?",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result1 == DialogResult.Yes)
                {
                    File.Delete(backupParkp);
                    statusLabel.Text = "Park_P backup deleted.";
                }
            } else
            {
                File.Delete(backupParkp);
                statusLabel.Text = "Park_P backup deleted.";
            }
                
        }

        private void loadOnStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            Properties.Settings.Default.loadMapOnStart = loadOnStartCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void mapSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mapPackageManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mpm.Show();
        }

        private void restoreOriginalParkPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreOriginalMap();
        }

        private void restoreDefaultMapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.restoreDefaultMapOnClose = restoreDefaultMapCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void restoreDefaultSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Need to save the last loaded map or things would get wonky.  Maybe we should restore default Park_P on settings reset?
            String currentlyLoadedMap = Properties.Settings.Default.lastMap;

            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();

            // Load Settings
            if (Properties.Settings.Default.RLFolder != "")
            {
                rlDirTextBox.Text = Properties.Settings.Default.RLFolder;
            }
            if (Properties.Settings.Default.ModFolder != "")
            {
                modsDirTextBox.Text = Properties.Settings.Default.ModFolder;
            }
            loadOnStartCheckBox.Checked = Properties.Settings.Default.loadMapOnStart;
            restoreDefaultMapCheckBox.Checked = Properties.Settings.Default.restoreDefaultMapOnClose;

            InitializeCustomMapList();
            mapSelectComboBox.SelectedItem = currentlyLoadedMap;
            Properties.Settings.Default.lastMap = currentlyLoadedMap;
        }
    }
}
