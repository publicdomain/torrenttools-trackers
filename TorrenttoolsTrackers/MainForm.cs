// <copyright file="MainForm.cs" company="PublicDomainWeekly.com">
//     CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication
//     https://creativecommons.org/publicdomain/zero/1.0/legalcode
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using PublicDomainWeekly;

namespace TorrenttoolsTrackers
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Gets or sets the associated icon.
        /// </summary>
        /// <value>The associated icon.</value>
        private Icon associatedIcon = null;

        public MainForm()
        {
            // The InitializeComponent() call is required for Windows Forms designer support.
            InitializeComponent();

            /* Set icons */

            // Set associated icon from exe file
            this.associatedIcon = Icon.ExtractAssociatedIcon(typeof(MainForm).GetTypeInfo().Assembly.Location);

            // Set public domain weekly tool strip menu item image
            this.weeklyReleasesPublicDomainWeeklycomToolStripMenuItem.Image = this.associatedIcon.ToBitmap();
        }

        void TargetPathBrowseButtonClick(object sender, EventArgs e)
        {
            var targetPath = string.Empty;

            if (this.fileRadioButton.Checked)
            {
                this.openFileDialog.Title = "Open target file";

                this.openFileDialog.Filter = "All files (*.*)|*.*";

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

                this.UpdateStatus();
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

                this.UpdateStatus();
            }
        }

        void ClearButtonClick(object sender, EventArgs e)
        {
            //#if (MessageBox.Show($"Would you like to clear {this.trackersCheckedListBox.Items.Count} tracker{(this.trackersCheckedListBox.Items.Count > 1 ? "s" : string.Empty)}?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.trackersCheckedListBox.Items.Clear();

                this.UpdateStatus();
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
            this.trackerListOpenFileDialog.FileName = string.Empty;

            if (this.trackerListOpenFileDialog.ShowDialog() == DialogResult.OK && this.trackerListOpenFileDialog.FileNames.Length > 0)
            {
                this.trackersCheckedListBox.Items.AddRange(File.ReadAllLines(this.trackerListOpenFileDialog.FileName));

                this.UpdateStatus();
            }
        }

        void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.trackerListSaveFileDialog.FileName = string.Empty;

            if (this.trackerListSaveFileDialog.ShowDialog() == DialogResult.OK && this.trackerListSaveFileDialog.FileName.Length > 0)
            {
                File.WriteAllLines(this.trackerListSaveFileDialog.FileName, this.trackersCheckedListBox.Items.Cast<string>().ToList());
            }
        }

        void TrackersCheckedListBoxItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.selectedCountToolStripStatusLabel.Text = $"{this.trackersCheckedListBox.CheckedItems.Count + ((e.NewValue == CheckState.Checked) ? 1 : -1)}";
        }

        void UpdateStatus()
        {
            this.trackerCountToolStripStatusLabel.Text = this.trackersCheckedListBox.Items.Count.ToString();
            this.selectedCountToolStripStatusLabel.Text = this.trackersCheckedListBox.CheckedItems.Count.ToString();
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
            // Open original thread
            Process.Start("https://www.donationcoder.com/forum/index.php?topic=51709.0");
        }

        void SourceCodeGithubcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open GitHub repository
            Process.Start("https://github.com/publicdomain/torrenttools-trackers");
        }

        void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Set license text
            var licenseText = $"CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication{Environment.NewLine}" +
                $"https://creativecommons.org/publicdomain/zero/1.0/legalcode{Environment.NewLine}{Environment.NewLine}" +
                $"Libraries and icons have separate licenses.{Environment.NewLine}{Environment.NewLine}" +
                $"Vortex Icon by OpenClipart-Vectors - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/vectors/vortex-whirlpool-swirl-strudel-146129/{Environment.NewLine}{Environment.NewLine}" +
                $"Patreon icon used according to published brand guidelines{Environment.NewLine}" +
                $"https://www.patreon.com/brand{Environment.NewLine}{Environment.NewLine}" +
                $"GitHub mark icon used according to published logos and usage guidelines{Environment.NewLine}" +
                $"https://github.com/logos{Environment.NewLine}{Environment.NewLine}" +
                $"DonationCoder icon used with permission{Environment.NewLine}" +
                $"https://www.donationcoder.com/forum/index.php?topic=48718{Environment.NewLine}{Environment.NewLine}" +
                $"PublicDomain icon is based on the following source images:{Environment.NewLine}{Environment.NewLine}" +
                $"Bitcoin by GDJ - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/vectors/bitcoin-digital-currency-4130319/{Environment.NewLine}{Environment.NewLine}" +
                $"Letter P by ArtsyBee - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/illustrations/p-glamour-gold-lights-2790632/{Environment.NewLine}{Environment.NewLine}" +
                $"Letter D by ArtsyBee - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/illustrations/d-glamour-gold-lights-2790573/{Environment.NewLine}{Environment.NewLine}";

            // Prepend sponsors
            licenseText = $"RELEASE SPONSORS:{Environment.NewLine}{Environment.NewLine}* Jesse Reichler{Environment.NewLine}* Max P{Environment.NewLine}* Raster d.o.o.{Environment.NewLine}{Environment.NewLine}=========={Environment.NewLine}{Environment.NewLine}" + licenseText;

            // Set title
            string programTitle = typeof(MainForm).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;

            // Set version for generating semantic version 
            Version version = typeof(MainForm).GetTypeInfo().Assembly.GetName().Version;

            // Set about form
            var aboutForm = new AboutForm(
                $"About {programTitle}",
                $"{programTitle} {version.Major}.{version.Minor}.{version.Build}",
                $"Made for: nickodemos{Environment.NewLine}DonationCoder.com{Environment.NewLine}Day #280, Week #40 @ October 07, 2021",
                licenseText,
                this.Icon.ToBitmap())
            {
                // Set about form icon
                Icon = this.associatedIcon,

                // Set always on top
                TopMost = this.TopMost
            };

            // Show about form
            aboutForm.ShowDialog();
        }

        void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Close program        
            this.Close();
        }
    }
}
