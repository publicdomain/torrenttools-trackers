using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace TorrenttoolsTrackers
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            // The InitializeComponent() call is required for Windows Forms designer support.
            InitializeComponent();
        }

        void TargetPathBrowseButtonClick(object sender, EventArgs e)
        {
            var targetPath = string.Empty;

            if (this.fileRadioButton.Checked)
            {
                this.openFileDialog.Title = "Open target file";

                this.openFileDialog.FileName = string.Empty;

                if (this.openFileDialog.ShowDialog() == DialogResult.OK && this.openFileDialog.FileNames.Length > 0)
                {
                    targetPath = this.openFileDialog.FileName;
                }
            }
            else
            {
                this.folderBrowserDialog.SelectedPath = string.Empty;

                if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK && this.folderBrowserDialog.SelectedPath.Length > 0)
                {
                    targetPath = this.folderBrowserDialog.SelectedPath;
                }
            }

            if (targetPath.Length > 0)
            {
                this.targetPathTextBox.Text = targetPath;
            }
        }

        void TorrenttoolsBrowseButtonClick(object sender, EventArgs e)
        {
            this.openFileDialog.Title = "Open torrenttools executable";

            this.openFileDialog.FileName = string.Empty;

            if (this.openFileDialog.ShowDialog() == DialogResult.OK && this.openFileDialog.FileNames.Length > 0)
            {
                this.torrenttoolsPathTextBox.Text = this.openFileDialog.FileName;
            }
        }

        void OutputTorrentBrowseButtonClick(object sender, EventArgs e)
        {
            this.outputTorrentSaveFileDialog.FileName = string.Empty;

            if (this.outputTorrentSaveFileDialog.ShowDialog() == DialogResult.OK && this.outputTorrentSaveFileDialog.FileName.Length > 0)
            {
                this.outputTorrentTextBox.Text = this.outputTorrentSaveFileDialog.FileName;
            }
        }

        void AddButtonClick(object sender, EventArgs e)
        {
            var newTracker = Interaction.InputBox("Enter new tracker URI", "Add");

            if (newTracker.Length > 0)
            {
                var retInt = this.trackersCheckedListBox.Items.Add(newTracker);

                this.trackersCheckedListBox.SetItemChecked(retInt, true);
            }
        }

        void EditButtonClick(object sender, EventArgs e)
        {
            if (this.trackersCheckedListBox.SelectedIndex > -1)
            {
                var editUri = Interaction.InputBox("Edit tracker URI", "Edit", this.trackersCheckedListBox.SelectedItem.ToString());

                this.trackersCheckedListBox.Items[this.trackersCheckedListBox.SelectedIndex] = editUri;
            }
        }

        void DeleteButtonClick(object sender, EventArgs e)
        {
            if (this.trackersCheckedListBox.SelectedIndex > -1)
            {
                this.trackersCheckedListBox.Items.RemoveAt(this.trackersCheckedListBox.SelectedIndex);
            }
        }

        void ClearButtonClick(object sender, EventArgs e)
        {
            //#if (MessageBox.Show($"Would you like to clear {this.trackersCheckedListBox.Items.Count} tracker{(this.trackersCheckedListBox.Items.Count > 1 ? "s" : string.Empty)}?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.trackersCheckedListBox.Items.Clear();
            }
        }

        void CopyButtonClick(object sender, EventArgs e)
        {
            string command = this.GetTorrenttoolsCommand();

            if (command.Length > 0)
            {
                Clipboard.SetText(command);

                MessageBox.Show("Command copied to clipboard.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void ExecuteButtonClick(object sender, EventArgs e)
        {

        }

        string GetTorrenttoolsCommand()
        {
            string command = string.Empty;

            if (this.targetPathTextBox.Text.Length == 0)
            {
                MessageBox.Show("Target path missing.", "No target", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                goto exitLabel;
            }

            if (this.torrenttoolsPathTextBox.Text.Length == 0)
            {
                MessageBox.Show("torrenttools path missing", "No executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                goto exitLabel;
            }

            var trackers = string.Empty;

            if (this.trackersCheckedListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please check trackers to process.", "No trackers", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                goto exitLabel;
            }
            else
            {
                foreach (string tracker in this.trackersCheckedListBox.CheckedItems)
                {
                    trackers += !singleTierToolStripMenuItem.Checked ? $" \"{tracker}\"" : $" {tracker}";
                }
            }

            trackers = trackers.Substring(1);

            if (singleTierToolStripMenuItem.Checked)
            {
                trackers = $"\"[{trackers}]\"";
            }

            string output = this.outputTorrentTextBox.Text.Length > 0 ? $" --output \"{this.outputTorrentTextBox.Text}\"" : string.Empty;

            command = $"{this.torrenttoolsPathTextBox.Text} create {this.targetPathTextBox.Text} --announce {trackers}{output}";

        exitLabel:

            return command;
        }

        void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.targetPathTextBox.Clear();
            this.torrenttoolsPathTextBox.Clear();
            this.outputTorrentTextBox.Clear();
            this.trackersCheckedListBox.Items.Clear();
        }

        void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        void OptionsToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set tool strip menu item
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)e.ClickedItem;

            // Toggle checked
            toolStripMenuItem.Checked = !toolStripMenuItem.Checked;

            // Set topmost by check box
            this.TopMost = this.alwaysOnTopToolStripMenuItem.Checked;
        }

        void WeeklyReleasesPublicDomainWeeklycomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open our public domain website
            Process.Start("https://publicdomainweekly.com");
        }

        void OriginalThreadDonationCodercomToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        void SourceCodeGithubcomToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Close program        
            this.Close();
        }
    }
}
