using System;
using System.Text;
using System.Windows.Forms;
using Notepad.Classes;

namespace Notepad.Forms
{
    public partial class ReplaceDialog : Form
    {
        private readonly Notepad _main;

        public ReplaceDialog(Notepad pMain)
        {
            InitializeComponent();
            _main = pMain;
        }

        private void controlFindWhatTextBox_Enter(object sender, EventArgs e)
        {
            var senderBox = (TextBox) sender;
            senderBox.SelectAll();
        }

        private void controlReplaceWithTextBox_Enter(object sender, EventArgs e)
        {
            var senderBox = (TextBox) sender;
            senderBox.SelectAll();
        }

        private void controlFindWhatTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void ReplaceDialog_Load(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            buttonFindNext.Enabled =
                buttonReplace.Enabled =
                    buttonReplaceAll.Enabled = textBoxFind.Text.Length > 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void ReplaceDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void Triggered()
        {
            textBoxFind.Focus();
        }

        private void buttonFindNext_Click(object sender, EventArgs e)
        {
            var searchText = textBoxFind.Text;
            var matchCase = checkBoxMatchCase.Checked;
            const bool bSearchDown = true;

            if (!_main.FindAndSelect(searchText, matchCase, bSearchDown))
            {
                MessageBox.Show(this, string.Format("Cannot find {0}", searchText), @"Notepad");
            }
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            var searchText = textBoxFind.Text;
            var replaceWithText = textBoxReplace.Text;
            var matchCase = checkBoxMatchCase.Checked;

            if (_main.SelectedText.Equals(searchText))
            {
                _main.SelectedText = replaceWithText;
            }

            if (!_main.FindAndSelect(searchText, matchCase, true))
            {
                MessageBox.Show(this, string.Format("Cannot find {0}", searchText), @"Notepad");
            }
        }

        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            var content = _main.Content;
            var searchText = textBoxFind.Text;
            var matchCase = checkBoxMatchCase.Checked;

            var indexes = Helper.GetIndexes(content, searchText, matchCase);

            var builder = new StringBuilder();
            var replaceWith = textBoxReplace.Text;

            {
                var lastIndex = -1;
                foreach (var index in indexes)
                {
                    if (index != 0)
                    {
                        int takeStart;

                        if (lastIndex == -1)
                        {
                            takeStart = 0;
                        }
                        else
                        {
                            takeStart = lastIndex + searchText.Length;
                        }

                        var takeEnd = index - 1;
                        var length = (takeEnd - takeStart) + 1;

                        var inBetween = content.Substring(takeStart, length);
                        builder.Append(inBetween);
                    }

                    builder.Append(replaceWith);
                    lastIndex = index;
                }

                {
                    int takeStart;

                    if (lastIndex == -1)
                    {
                        takeStart = 0;
                    }
                    else
                    {
                        takeStart = lastIndex + searchText.Length;
                    }

                    var takeEnd = content.Length - 1;
                    var length = (takeEnd - takeStart) + 1;
                    builder.Append(content.Substring(takeStart, length));
                }
            }
            _main.Content = builder.ToString();
        }
    }
}