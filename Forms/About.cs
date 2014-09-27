using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Notepad.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void linkLabelHaddow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://haddow64.com");
        }

        private void linkLabelGIT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/haddow64");
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}