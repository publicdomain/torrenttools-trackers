
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

        }

        void TorrenttoolsBrowseButtonClick(object sender, EventArgs e)
        {

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

        void ExitToolStripMenuItemClick(object sender, EventArgs e)
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
    }
}
