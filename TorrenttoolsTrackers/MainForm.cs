
using System;
using System.Collections.Generic;
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

        void AddButtonClick(object sender, EventArgs e)
        {
            var newTracker = Interaction.InputBox("Enter new tracker URI", "Add");

            if (newTracker.Length > 0)
            {
                this.trackersCheckedListBox.Items.Add(newTracker);
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
            if (MessageBox.Show($"Would you like to clear {this.trackersCheckedListBox.Items.Count} tracker{(this.trackersCheckedListBox.Items.Count > 1 ? "s" : string.Empty)}?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.trackersCheckedListBox.Items.Clear();
            }
        }

        void CopyButtonClick(object sender, EventArgs e)
        {

        }

        void ExecuteButtonClick(object sender, EventArgs e)
        {

        }

        void NewToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        void OptionsToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        void WeeklyReleasesPublicDomainWeeklycomToolStripMenuItemClick(object sender, EventArgs e)
        {

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

        }
    }
}
