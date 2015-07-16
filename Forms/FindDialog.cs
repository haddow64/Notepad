using System;
using System.Windows.Forms;

namespace Notepad.Forms
{
    public partial class FindDialog : Form
    {
        private readonly Notepad _main;

        public FindDialog(Notepad pMain)
        {
            InitializeComponent();
            _main = pMain;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void FindDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void controlTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateFindNextButton();
        }

        private void UpdateFindNextButton()
        {
            buttonFindNext.Enabled = textBoxFind.Text.Length > 0;
        }

        private void FindDialog_Load(object sender, EventArgs e)
        {
            UpdateFindNextButton();
        }

        private void buttonFindNext_Click(object sender, EventArgs e)
        {
            var searchText = textBoxFind.Text;
            var matchCase = checkboxMatchCase.Checked;
            var bSearchDown = radioButtonFindDown.Checked;

            if (!_main.FindAndSelect(searchText, matchCase, bSearchDown))
            {
                MessageBox.Show(this, string.Format("Cannot find {0}", searchText), @"Notepad");
            }
        }

        public void Triggered()
        {
            textBoxFind.Focus();
        }

        private void controlTextBox_Enter(object sender, EventArgs e)
        {
            var senderBox = (TextBox) sender;
            senderBox.SelectAll();
        }

        public new void Show(IWin32Window window = null)
        {
            textBoxFind.Focus();
            textBoxFind.SelectAll();

            if (window == null)
            {
                base.Show();
            }
            else
            {
                base.Show(window);
            }
        }
    }
}