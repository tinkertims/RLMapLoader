using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RLMapLoader
{
    public partial class MapPackageManager : Form
    {
        private BindingList<MapPackage> mapPackages = new BindingList<MapPackage>();
        private WebClient webClient;
        Form1 mainForm;

        public MapPackageManager(Form1 formRef)
        {
            InitializeComponent();
            mainForm = formRef;

            // Get list of maps from server
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://hack.fyi/rlmaps/maps.json");

                List<MapPackageJson> maps  = JsonConvert.DeserializeObject<List<MapPackageJson>>(json);

                foreach(MapPackageJson mpj in maps)
                {
                    mapPackages.Add(mpj.mapPackage);
                }
            }

            dataGridView1.DataSource = mapPackages;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Columns["FileUrl"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            packageStatusLabel.Text = "Downloaded initial map list.";
        }

        private void MapPackageManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            packageStatusLabel.Text = "Downloaded initial map list.";
            this.Hide();
            e.Cancel = true;

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (System.Uri.IsWellFormedUriString(r.Cells["FileUrl"].Value.ToString(), UriKind.Absolute))
                {
                    r.Cells["FileUrl"] = new DataGridViewButtonCell();
                    // Note that if I want a different link colour for example it must go here
                    DataGridViewButtonCell c = r.Cells["FileUrl"] as DataGridViewButtonCell;
                    int pad = (r.Height - 25) / 2;
                    c.Style.Padding = new Padding(10, pad, 10, pad);
                }
                if (System.Uri.IsWellFormedUriString(r.Cells["PreviewUrl"].Value.ToString(), UriKind.Absolute))
                {
                    string imageUrl = r.Cells["PreviewUrl"].Value.ToString();
                    r.Cells["PreviewUrl"] = new DataGridViewImageCell();
                    DataGridViewImageCell c = r.Cells["PreviewUrl"] as DataGridViewImageCell;
                    c.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }

            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "PreviewUrl")
            {
                if (System.Uri.IsWellFormedUriString(dataGridView1.Rows[e.RowIndex].Cells["PreviewUrl"].Value.ToString(), UriKind.Absolute))
                {

                    // Note that if I want a different link colour for example it must go here
                    var request = WebRequest.Create(dataGridView1.Rows[e.RowIndex].Cells["PreviewUrl"].Value.ToString());
                    try
                    {
                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            Image tempPreview = (Image)Bitmap.FromStream(stream);
                            e.Value = tempPreview;
                        }
                    }
                    catch
                    {

                    }
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "FileUrl")
            {
                Uri downloadUrl = new Uri(mapPackages[e.RowIndex].FileUrl);
                string filename = System.IO.Path.GetFileName(downloadUrl.LocalPath);

                if (File.Exists(Properties.Settings.Default.ModFolder + "\\" + filename))
                {
                    e.Value = "Update";
                } else
                {
                    e.Value = "Download";
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex].Name == "FileUrl" &&
                e.RowIndex >= 0)
            {
                Uri downloadUrl = new Uri(mapPackages[e.RowIndex].FileUrl);
                string filename = System.IO.Path.GetFileName(downloadUrl.LocalPath);

                if (File.Exists(Properties.Settings.Default.ModFolder+ "\\" + filename))
                {
                    // Confirm overwrite
                    DialogResult dialogResult = MessageBox.Show(filename + " already exists, overwrite?", "Dependency Exists", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        packageStatusLabel.Text = "Downloading " + mapPackages[e.RowIndex].Name + " map...";
                        webClient = new WebClient();
                        webClient.Proxy = null;
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                        webClient.DownloadFileAsync(downloadUrl, Properties.Settings.Default.ModFolder + "\\" + filename);
                    }
                } else
                {
                    packageStatusLabel.Text = "Downloading " + mapPackages[e.RowIndex].Name + " map...";
                    webClient = new WebClient();
                    webClient.Proxy = null;
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                    webClient.DownloadFileAsync(downloadUrl, Properties.Settings.Default.ModFolder + "\\" + filename);
                }
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadProgressBar.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            packageStatusLabel.Text = "Download completed!";
            mainForm.RescanModsFolder();
        }

        private void statusStrip1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if engine files already exist, if so ignore.
            String[] mapDependencies = { "EditorLandscapeResources.upk", "EditorMaterials.upk", "EditorMeshes.upk", "EditorResources.upk", "Engine_MI_Shaders.upk", "EngineBuildings.upk", "EngineDebugMaterials.upk", "EngineFonts.upk", "EngineFonts_RUS.upk", "EngineMaterials.upk", "EngineMeshes.upk", "EngineResources.upk", "EngineSounds.upk", "EngineVolumetrics.upk", "MapTemplateIndex.upk", "MapTemplates.upk", "MaterialTemplates.upk", "MobileEngineMaterials.upk", "MobileResources.upk", "NodeBuddies.upk" };
            Boolean[] downloadDep;
            Boolean yesToAll = false;


            foreach (string depName in mapDependencies)
            {
                if (File.Exists(Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\" + depName))
                {
                    // Confirm overwrite
                    DialogResult dialogResult = MessageBox.Show(depName + " already exists, overwrite?", "Dependency Exists", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // Download dependency
                        packageStatusLabel.Text = "Downloading " + depName + " dependency...";
                        webClient = new WebClient();
                        webClient.Proxy = null;
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                        Uri downloadUrl = new Uri("http://hack.fyi/rlmaps/dep/" + depName);
                        string filename = System.IO.Path.GetFileName(downloadUrl.LocalPath);

                        webClient.DownloadFileAsync(downloadUrl, Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\" + filename);
                    }

                }
                else
                {
                    // Download dependency
                    packageStatusLabel.Text = "Downloading " + depName + " dependency...";
                    webClient = new WebClient();
                    webClient.Proxy = null;
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                    Uri downloadUrl = new Uri("http://hack.fyi/rlmaps/dep/" + depName);
                    string filename = System.IO.Path.GetFileName(downloadUrl.LocalPath);

                    webClient.DownloadFileAsync(downloadUrl, Properties.Settings.Default.RLFolder + "\\TAGame\\CookedPCConsole\\" + filename);
                }
            }
            


        }
    }
}
