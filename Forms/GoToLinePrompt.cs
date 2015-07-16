using System;
using System.Globalization;
using System.Windows.Forms;
using Notepad.Classes;

namespace Notepad.Forms
{
    public partial class GoToLineDialog : Form
    {
        public int LineNumber;

        public GoToLineDialog(int pLineNumber)
        {
            InitializeComponent();
            LineNumber = pLineNumber;
            textBoxLineNumber.Text = LineNumber.ToString(CultureInfo.InvariantCulture);
        }

        private void GoToLinePrompt_Load(object sender, EventArgs e)
        {
            textBoxLineNumber.SelectAll();
        }

        private void buttonGoTo_Click(object sender, EventArgs e)
        {
            if (textBoxLineNumber.Text.IsEmpty())
            {
                MessageBox.Show(this, @"You must enter a value.", @"Notepad - Goto Line");
                return;
            }
           
            var potentialLineNumber = Int32.Parse(textBoxLineNumber.Text);

            if (potentialLineNumber == 0)
            {
                MessageBox.Show(this, @"Zero (0) is not a valid line number, line numbers start at 1.",
                    @"Notepad - Goto Line");
                return;
            }

            LineNumber = potentialLineNumber;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void controlLineNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)) return;

            var senderBox = (TextBox) sender;
            controlToolTip.Show("You can only type a number here.", senderBox);
            e.Handled = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}