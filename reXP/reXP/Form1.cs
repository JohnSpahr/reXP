using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace reXP
{
    public partial class reXP : Form
    {
        public reXP()
        {
            InitializeComponent();

            start();
        }


        public string currentDrive;

        private void start()
        {
            //check if version is windows xp
            int version = Environment.OSVersion.Version.Minor;
            if (version == 1)
            {
                int totalDrives = 0;

                foreach (var drive in DriveInfo.GetDrives())
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        //add each removable drive to listbox
                        driveList.Items.Add(drive.Name + " - " + drive.VolumeLabel);
                        totalDrives++;
                    }
                }

                if (totalDrives != 0)
                {
                    driveList.SelectedIndex = 0;
                }
                else
                {
                    if (MessageBox.Show("No removable drives found. Would you like to check for drives again?", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        //check for drives again
                        start();
                    }
                    else
                    {
                        //exit
                        Environment.Exit(0);
                    }
                }
            }
            else
            {
                //if OS version isn't XP
                MessageBox.Show("This program only works with Windows XP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        public static void dirCopy(string source, string target)
        {
            DirectoryInfo input = new DirectoryInfo(source);
            DirectoryInfo output = new DirectoryInfo(target);

            CopyAll(input, output);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            //copy files
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            //copy subdirectories
            foreach (DirectoryInfo sourceSub in source.GetDirectories())
            {
                DirectoryInfo nextTarget = target.CreateSubdirectory(sourceSub.Name);
                CopyAll(sourceSub, nextTarget);
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            statusLbl.Text = "Copying files... please wait."; //indicate that files are being copied
            statusLbl.Refresh();
            startBtn.Enabled = false; //disable start button
            Cursor = Cursors.WaitCursor; //show wait cursor

            if (!Directory.Exists(currentDrive + "reXP Save"))
            {
                Directory.CreateDirectory(currentDrive + "reXP Save");
            }

            if (freeCellBox.Checked == true)
            {
                if (File.Exists(@"C:\WINDOWS\system32\freecell.exe"))
                {
                    //copy FreeCell 
                    File.Copy(@"C:\WINDOWS\system32\freecell.exe", currentDrive + @"reXP Save\FreeCell.exe", true);
                }
                else
                {
                    //if process fails
                    MessageBox.Show("Unable to copy FreeCell to drive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (heartsBox.Checked == true)
            {
                if (File.Exists(@"C:\WINDOWS\system32\mshearts.exe"))
                {
                    //copy Hearts 
                    File.Copy(@"C:\WINDOWS\system32\mshearts.exe", currentDrive + @"reXP Save\Hearts.exe", true);
                }
                else
                {
                    //if process fails
                    MessageBox.Show("Unable to copy Hearts to drive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (minesweeperBox.Checked == true)
            {
                if (File.Exists(@"C:\WINDOWS\system32\winmine.exe"))
                {
                    //copy Minesweeper 
                    File.Copy(@"C:\WINDOWS\system32\winmine.exe", currentDrive + @"reXP Save\Minesweeper.exe", true);
                }
                else
                {
                    //if process fails
                    MessageBox.Show("Unable to copy Minesweeper to drive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (pinballBox.Checked == true)
            {
                if (Directory.Exists(@"C:\Program Files\Windows NT\Pinball") && !Directory.Exists(currentDrive + @"reXP Save\Pinball"))
                {
                    //copy Pinball
                    dirCopy(@"C:\Program Files\Windows NT\Pinball", currentDrive + @"reXP Save\Pinball");
                }
                else
                {
                    //if process fails
                    MessageBox.Show("Unable to copy Pinball to drive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (solitaireBox.Checked == true)
            {
                if (File.Exists(@"C:\WINDOWS\system32\sol.exe"))
                {
                    //copy Solitaire 
                    File.Copy(@"C:\WINDOWS\system32\sol.exe", currentDrive + @"reXP Save\Solitaire.exe", true);
                }
                else
                {
                    //if process fails
                    MessageBox.Show("Unable to copy Solitaire to drive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (spiderSolitaireBox.Checked == true)
            {
                if (File.Exists(@"C:\WINDOWS\system32\spider.exe"))
                {
                    //copy Spider Solitaire 
                    File.Copy(@"C:\WINDOWS\system32\spider.exe", currentDrive + @"reXP Save\Spider Solitaire.exe", true);
                }
                else
                {
                    //if process fails
                    MessageBox.Show("Unable to copy Spider Solitaire to drive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (xpToursBox.Checked == true)
            {
                if (Directory.Exists(@"C:\WINDOWS\Help\Tours"))
                {
                    //copy tours to directory
                    dirCopy(@"C:\WINDOWS\Help\Tours", currentDrive + @"reXP Save\Windows XP Tours\");

                }
                else
                {
                    //if process fails
                    MessageBox.Show("Unable to copy Windows XP Tours to drive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (movieMakerBox.Checked == true)
            {
                if (Directory.Exists(@"C:\Program Files\Movie Maker"))
                {
                    //copy movie maker to directory
                    dirCopy(@"C:\Program Files\Movie Maker", currentDrive + @"reXP Save\Windows Movie Maker\");
                }
                else
                {
                    //if process fails
                    MessageBox.Show("Unable to copy Movie Maker to drive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (freeCellBox.Checked == true || heartsBox.Checked == true || solitaireBox.Checked == true)
            {
                if (File.Exists(@"C:\WINDOWS\system32\cards.dll"))
                {
                    //copy cards.dll 
                    File.Copy(@"C:\WINDOWS\system32\cards.dll", currentDrive + @"reXP Save\cards.dll", true);
                }
                else
                {
                    //if process fails
                    MessageBox.Show("Unable to copy cards.dll to drive. This file is needed for some of the Windows XP card games.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            statusLbl.Text = "Operation complete."; //set status label to default text
            statusLbl.Refresh();
            Cursor = Cursors.Default; //show normal cursor
            startBtn.Enabled = true; //enable start button
            Process.Start(currentDrive + "reXP Save"); //open up folder
        }

        private void driveList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //when current drive is changed
            currentDrive = driveList.GetItemText(driveList.SelectedItem);
            currentDrive = currentDrive.Substring(0, currentDrive.IndexOf(" ") + 1);
            currentDrive = currentDrive.Trim();
        }

        private void selectAllBtn_Click(object sender, EventArgs e)
        {
            foreach (var control in Controls)
            {
                //select all programs
                CheckBox cb = control as CheckBox;
                if (cb != null)
                {
                    cb.Checked = true;
                }
            }
        }

        private void selectNoneBtn_Click(object sender, EventArgs e)
        {
            foreach (var control in Controls)
            {
                //deselect all programs
                CheckBox cb = control as CheckBox;
                if (cb != null)
                {
                    cb.Checked = false;
                }
            }
        }

        private void aboutBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //show about screen
            MessageBox.Show("reXP - The best way to re-eXPerience Windows XP programs!\nVersion 1.3.2\nhttps://github.com/JohnSpahr", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
