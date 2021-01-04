using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace CD_Reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        List<string> outputList = new List<string>();
        public IEnumerable<string> getDiscDirectoryInfo(string path)
        {
            List<string> filePaths = new List<string>();

            try
            {
                string[] cdInfo = Directory.GetFiles(path);
                int counter = 1;

                foreach (var item in cdInfo)
                {

                    outputList.Add(counter + ". " + item.Substring(3) + "\n");
                    richTextBox1.Text += (counter + ". " + item.Substring(3) + "\n");
                    counter++;
                }
                if (cdInfo.Length == 0)
                {
                     MessageBox.Show("Error! No Disc Detected or No Content within Disc.");
                }
            }
            catch
            {
                MessageBox.Show("Error! No Disc Detected or No Content within Disc.");
            }


            return filePaths;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get all of the ready CD drives
            foreach (var cdDrive in DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.CDRom && d.IsReady))
            {
                // Start at the drive and get all of the files recursively
                IEnumerable<string> driveFiles = getDiscDirectoryInfo(cdDrive.Name);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text File|*.txt";
            saveFileDialog1.Title = "Save the Text File";
            //saveFileDialog1.ShowDialog();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName))
                    foreach (var item in outputList)
                        sw.WriteLine(item);
            }
        }
    }
}
