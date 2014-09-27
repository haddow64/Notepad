using System;
using System.Windows.Forms;

namespace Notepad
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var notepad = new Forms.Notepad();

            var commandLine = Environment.CommandLine.Trim();

            var filename = (string) null;

            if (commandLine.StartsWith("\""))
            {
                var closingQuoteIndex = commandLine.IndexOf('\"', 1);

                if (closingQuoteIndex < (commandLine.Length - 1))
                {
                    filename = commandLine.Substring(closingQuoteIndex + 1).Trim();
                }
            }
            else
            {
                var firstSpaceIndex = commandLine.IndexOf(' ', 1);

                if (firstSpaceIndex != -1)
                {
                    filename = commandLine.Substring(firstSpaceIndex + 1).Trim();
                }
            }

            if (filename != null)
            {
                notepad.Open(filename);
            }

            Application.Run(notepad);
        }
    }
}