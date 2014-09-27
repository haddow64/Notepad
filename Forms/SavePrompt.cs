using System;
using System.Windows.Forms;
using Notepad.Classes;

namespace Notepad.Forms
{
    public partial class SavePrompt : Form
    {
        public SavePrompt(string filename)
        {
            InitializeComponent();

            labelSave.Text = labelSave.Text.FormatUsingObject(new {Filename = filename ?? "Untitled"});
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void buttonDontSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}