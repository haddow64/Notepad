using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Notepad.Classes;
using Notepad.Properties;
using Win32Types;

namespace Notepad.Forms
{
    /// <summary>
    ///     C# implementation of Notepad based on the Windows Notepad
    ///     Based upon examples:
    ///     http://www.codeproject.com/Articles/7313/Notepad-Application-in-C
    ///     http://www.sourcecodester.com/tutorials/c/6562/simple-notepad-application-using-c-part-2.html
    ///     Author:     Graeme Haddow
    ///     https://github.com/haddow64
    ///     Using FileDlgExtenders - gustavo_franco@hotmail.com
    ///     Available from: https://github.com/NoxHarmonium/enform/tree/master/CustomOpenFile/FileDlgExtenders
    /// </summary>
    public partial class Notepad : Form
    {
        private Encoding _encoding = Encoding.ASCII;
        private string _filename;
        private FindDialog _findDialog;
        private bool _isDirty;
        private bool _lastMatchCase;
        private bool _lastSearchDown;
        private string _lastSearchText;
        private PageSettings _pageSettings;
        private ReplaceDialog _replaceDialog;

        public Notepad()
        {
            InitializeComponent();

            if (Settings.WindowPosition.IsEmpty) return;

            Bounds = Settings.WindowPosition;
            StartPosition = FormStartPosition.Manual;
        }

        private string Filename
        {
            get { return _filename; }
            set
            {
                _filename = value;
                OnFilenameChanged();
            }
        }

        private string DocName => Filename == null ? "Untitled" : Path.GetFileName(Filename);

        public string Content
        {
            get { return controlContentTextBox.Text; }
            set { controlContentTextBox.Text = value; }
        }

        private bool StatusBarVisible
        {
            get { return Settings.IsStatusBarVisible; }
            set
            {
                menuitemViewStatusBar.Checked = controlStatusBar.Visible = Settings.IsStatusBarVisible = value;
                Settings.Save();
            }
        }

        private bool WordWrap
        {
            get { return controlContentTextBox.WordWrap; }
            set { menuitemFormatWordWrap.Checked = controlContentTextBox.WordWrap = value; }
        }

        private static Settings Settings => Settings.Default;

        private Font CurrentFont
        {
            get { return Settings.CurrentFont; }
            set
            {
                controlContentTextBox.Font = Settings.CurrentFont = value;
                Settings.Save();
            }
        }

        private bool IsDirty
        {
            get
            {
                if (Filename == null && Content.IsEmpty()) return false;
                return _isDirty;
            }
            set { _isDirty = value; }
        }

        private PageSettings PageSettings
        {
            get
            {
                if (_pageSettings == null)
                {
                    if (Settings.MoreSettings.PageSettings != null)
                    {
                        _pageSettings = Settings.MoreSettings.PageSettings;
                    }
                    else
                    {
                        var pageSettings = new PageSettings
                        {
                            Margins = new Margins(75, 75, 100, 100)
                        };

                        _pageSettings = pageSettings;
                    }
                }

                return _pageSettings;
            }
            set
            {
                Settings.MoreSettings.PageSettings = value;
                Settings.Save();
            }
        }

        public string SelectedText
        {
            get { return controlContentTextBox.SelectedText; }
            set
            {
                controlContentTextBox.SelectedText = value;
                IsDirty = true;
            }
        }

        private ContentPosition CaretPosition => CharIndexToPosition(SelectionStart);

        private int LineIndex
        {
            get { return CaretPosition.LineIndex; }
            set
            {
                var targetLineIndex = value;

                if (targetLineIndex < 0)
                {
                    targetLineIndex = 0;
                }

                if (targetLineIndex >= controlContentTextBox.Lines.Length)
                {
                    targetLineIndex = controlContentTextBox.Lines.Length - 1;
                }

                var charIndex = 0;

                for (var currentLineIndex = 0; currentLineIndex < targetLineIndex; currentLineIndex++)
                {
                    charIndex += controlContentTextBox.Lines[currentLineIndex].Length + Environment.NewLine.Length;
                }

                SelectionStart = charIndex;
                controlContentTextBox.ScrollToCaret();
            }
        }

        private int SelectionEnd => SelectionStart + SelectionLength;

        private int SelectionStart
        {
            get { return controlContentTextBox.SelectionStart; }
            set
            {
                controlContentTextBox.SelectionStart = value;
                controlContentTextBox.ScrollToCaret();
            }
        }

        private int SelectionLength
        {
            get { return controlContentTextBox.SelectionLength; }
            set { controlContentTextBox.SelectionLength = value; }
        }

        private void OnFilenameChanged()
        {
            OnDocNameChanged();
        }

        private void OnDocNameChanged()
        {
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            if (Tag == null)
            {
                Tag = Text;
            }

            Text = ((string) Tag).FormatUsingObject(new {docName = DocName});
        }

        private void controlContentTextBox_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            UpdateTitle();
            menuitemFormatWordWrap.Checked = controlContentTextBox.WordWrap;
            CurrentFont = Settings.CurrentFont;
            UpdateStatusBar();
            menuitemViewStatusBar.Checked = controlStatusBar.Visible = Settings.IsStatusBarVisible;
            controlContentTextBox.BringToFront();
        }

        private void menuitemFormatWordWrap_Click(object sender, EventArgs e)
        {
            WordWrap = !WordWrap;
        }

        private void menuitemFormatWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            // ReSharper disable once InconsistentNaming
            var Sender = (ToolStripMenuItem) sender;
            WordWrap = Sender.Checked;
        }

        private void menuitemFormatFont_Click(object sender, EventArgs e)
        {
            var fontDialog = new FontDialog {Font = CurrentFont};
            if (fontDialog.ShowDialog(this) != DialogResult.OK) return;
            CurrentFont = fontDialog.Font;
        }

        private bool Save()
        {
            if (!IsDirty) return true;

            if ((Filename == null) || new FileInfo(Filename).IsReadOnly)
            {
                return SaveAs();
            }

            File.WriteAllText(Filename, Content);
            IsDirty = false;
            return true;
        }

        private bool SaveAs()
        {
            var saveDialog = new SaveOpenDialog
            {
                FileDlgFileName = Filename,
                FileDlgDefaultExt = ".txt",
                FileDlgFilter = "Text docs (*.txt)|*.txt|All Files (*.*)|*.*",
                Encoding = _encoding,
                FileDlgCaption = "Save",
                FileDlgOkCaption = "Save"
            };

            if (saveDialog.ShowDialog(this) != DialogResult.OK) return false;

            var potentialFilename = saveDialog.MSDialog.FileName;

            _encoding = saveDialog.Encoding;
            File.WriteAllText(potentialFilename, Content, _encoding);

            Filename = potentialFilename;
            IsDirty = false;

            return true;
        }

        private void menuitemFileOpen_Click(object sender, EventArgs e)
        {
            if (!EnsureWorkNotLost()) return;

            var openDialog = new SaveOpenDialog
            {
                FileDlgDefaultExt = ".txt",
                FileDlgFileName = Filename,
                FileDlgFilter = "Text docs (*.txt)|*.txt|All Files (*.*)|*.*",
                FileDlgType = FileDialogType.OpenFileDlg,
                FileDlgCaption = "Open",
                FileDlgOkCaption = "Open"
            };

            if (openDialog.ShowDialog(this) != DialogResult.OK) return;

            Open(openDialog.MSDialog.FileName, openDialog.Encoding);
        }

        public void Open(string pFilename, Encoding encoding = null)
        {
            var filename = pFilename;

            if (!File.Exists(filename))
            {
                var fileExists = false;

                var extension = Path.GetExtension(filename);
                if (extension == "")
                {
                    filename = filename + ".txt";
                    fileExists = File.Exists(filename);
                }

                if (!fileExists)
                {
                    // ReSharper disable LocalizableElement
                    var result = MessageBox.Show("Cannot find the {Filename} file. Do you want to create a new file?",
                        "Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    // ReSharper restore LocalizableElement

                    switch (result)
                    {
                        case DialogResult.Yes:
                            File.WriteAllText(filename, "");
                            break;
                        case DialogResult.No:
                        case DialogResult.Cancel:
                            return;
                        default:
                            throw new Exception();
                    }
                }
            }

            if (encoding == null)
            {
                // Likely not opened using the file dialog
                using (var streamReader = new StreamReader(filename, true))
                {
                    streamReader.ReadToEnd();
                    _encoding = streamReader.CurrentEncoding;
                }
            }

            Content = ReadAllText(filename, encoding);
            SelectionStart = 0;
            Filename = filename;
            IsDirty = false;
        }

        private static string ReadAllText(string pFilename, Encoding encoding)
        {
            using (var fileStream = new FileStream(pFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (encoding == null)
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        var text = streamReader.ReadToEnd();
                        return text;
                    }
                }
                using (var streamReader = new StreamReader(fileStream, encoding, false))
                {
                    var text = streamReader.ReadToEnd();
                    return text;
                }
            }
        }

        private void menuitemFileSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void menuitemFileSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void menuitemFileNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private bool EnsureWorkNotLost()
        {
            if (!IsDirty) return true;

            var dialogResult = new SavePrompt(Filename).ShowDialog(this);

            switch (dialogResult)
            {
                case DialogResult.Yes:
                    return Save();
                case DialogResult.No:
                    return true;
                case DialogResult.Cancel:
                    return false;
                default:
                    throw new Exception();
            }
        }

        private void New()
        {
            if (!EnsureWorkNotLost()) return;

            Filename = null;
            Content = "";
            IsDirty = false;
            _encoding = Encoding.ASCII;
        }

        private void menuitemFilePageSetup_Click(object sender, EventArgs e)
        {
            var pageSetupDialog = new PageSetupDialog {PageSettings = PageSettings};
            if (pageSetupDialog.ShowDialog(this) != DialogResult.OK) return;
            PageSettings = pageSetupDialog.PageSettings;
        }

        private void menuitemFilePrint_Click(object ignoreSender, EventArgs ignoreE)
        {
            var printDialog = new PrintDialog();

            if (Settings.MoreSettings.PrinterSettings != null)
            {
                printDialog.PrinterSettings = Settings.MoreSettings.PrinterSettings;
            }

            if (printDialog.ShowDialog(this) != DialogResult.OK) return;
            Settings.MoreSettings.PrinterSettings = printDialog.PrinterSettings;
            Settings.Save();

            var printdoc = new PrintDocument
            {
                DefaultPageSettings = PageSettings,
                PrinterSettings = Settings.MoreSettings.PrinterSettings,
                DocumentName = Name = DocName + " - Notepad"
            };

            var remainingContentToPrint = Content;

#pragma warning disable 219
            var pageIndex = 0;
#pragma warning restore 219

            printdoc.PrintPage += (sender, e) =>
            {
                {
                    // Print body
                    int charactersFitted;
                    int linesFilled;

                    var marginBounds = new RectangleF(e.MarginBounds.X,
                        e.MarginBounds.Y + CurrentFont.Height, e.MarginBounds.Width,
                        e.MarginBounds.Height - (CurrentFont.Height*2));

                    e.Graphics.MeasureString(remainingContentToPrint, CurrentFont, marginBounds.Size,
                        StringFormat.GenericTypographic, out charactersFitted, out linesFilled);
                    e.Graphics.DrawString(remainingContentToPrint, CurrentFont, Brushes.Black, marginBounds,
                        StringFormat.GenericTypographic);

                    remainingContentToPrint = remainingContentToPrint.Substring(charactersFitted);

                    e.HasMorePages = (remainingContentToPrint.Length > 0);
                }
                pageIndex++;
            };

            printdoc.Print();
        }

        private void menuitemFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuitemEditUndo_Click(object sender, EventArgs e)
        {
            controlContentTextBox.Undo();
        }

        private void menuitemEditCut_Click(object sender, EventArgs e)
        {
            controlContentTextBox.Cut();
        }

        private void menuitemEditCopy_Click(object sender, EventArgs e)
        {
            controlContentTextBox.Copy();
        }

        private void menuitemEditPaste_Click(object sender, EventArgs e)
        {
            controlContentTextBox.Paste();
        }

        private void menuitemEditDelete_Click(object sender, EventArgs e)
        {
            if (SelectionLength == 0)
            {
                SelectionLength = 1;
            }

            SelectedText = "";
        }

        private void menuitemEditSelectAll_Click(object sender, EventArgs e)
        {
            controlContentTextBox.SelectAll();
        }

        private void menuitemEditTimeDate_Click(object sender, EventArgs e)
        {
            SelectedText = DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString();
        }

        private void menuitemEditGoTo_Click(object sender, EventArgs e)
        {
            var goToLinePrompt = new GoToLineDialog(LineIndex + 1) {Left = Left + 5, Top = Top + 44};

            if (goToLinePrompt.ShowDialog(this) != DialogResult.OK) return;

            var targetLineIndex = goToLinePrompt.LineNumber - 1;

            if (targetLineIndex > controlContentTextBox.Lines.Length)
            {
                // ReSharper disable LocalizableElement
                MessageBox.Show(this, "The line number is beyond the total number of lines",
                    "Notepad - Go to Line");
                // ReSharper restore LocalizableElement
                return;
            }

            LineIndex = targetLineIndex;
        }

        private ContentPosition CharIndexToPosition(int pCharIndex)
        {
            var currentCharIndex = 0;

            if (controlContentTextBox.Lines.Length == 0 && currentCharIndex == 0)
                return new ContentPosition {LineIndex = 0, ColumnIndex = 0};

            for (var currentLineIndex = 0; currentLineIndex < controlContentTextBox.Lines.Length; currentLineIndex++)
            {
                var lineStartCharIndex = currentCharIndex;
                var line = controlContentTextBox.Lines[currentLineIndex];
                var lineEndCharIndex = lineStartCharIndex + line.Length + 1;

                if (pCharIndex >= lineStartCharIndex && pCharIndex <= lineEndCharIndex)
                {
                    var columnIndex = pCharIndex - lineStartCharIndex;
                    return new ContentPosition {LineIndex = currentLineIndex, ColumnIndex = columnIndex};
                }

                currentCharIndex += controlContentTextBox.Lines[currentLineIndex].Length + Environment.NewLine.Length;
            }

            return null;
        }

        private void UpdateStatusBar()
        {
            if (controlCaretPositionLabel.Tag == null)
            {
                controlCaretPositionLabel.Tag = controlCaretPositionLabel.Text;
            }

            controlCaretPositionLabel.Text = ((string) controlCaretPositionLabel.Tag).FormatUsingObject(new
            {
                LineNumber = CaretPosition.LineIndex + 1,
                ColumnNumber = CaretPosition.ColumnIndex + 1
            });
        }

        private void menuitemAbout_Click(object sender, EventArgs e)
        {
            new About().ShowDialog(this);
        }

        private void controlContentTextBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
        }

        private void controlContentTextBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var docPath = (string[]) e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(docPath[0]))
                {
                    try
                    {
                        var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                        if (files != null && files.Length != 0)
                        {
                            controlContentTextBox.Text = files[0];
                        }
                    }
                    catch (Exception)
                    {
                        // ReSharper disable once LocalizableElement
                        MessageBox.Show("File could not be opened. Make sure the file is a text file.");
                    }
                }
            }
        }

        private void controlContentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateStatusBar();

            // Shortcuts
            if (e.Control && e.KeyCode == Keys.F)
            {
                Find();
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                Save();
            }
            if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                SaveAs();
            }
            if (e.Shift && e.KeyCode == Keys.P)
            {
                menuitemFilePrint_Click(sender, e);
            }
        }

        private void controlContentTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateStatusBar();
        }

        private void menuitemViewStatusBar_Click(object sender, EventArgs e)
        {
            StatusBarVisible = !StatusBarVisible;
        }

        private void menuitemEdit_DropDownOpening(object sender, EventArgs e)
        {
            menuitemEditCut.Enabled =
                menuitemEditCopy.Enabled =
                    menuitemEditDelete.Enabled = (SelectionLength > 0);

            menuitemEditFind.Enabled =
                menuitemEditFindNext.Enabled = (Content.Length > 0);
        }

        public bool FindAndSelect(string pSearchText, bool pMatchCase, bool pSearchDown)
        {
            var eStringComparison = pMatchCase
                ? StringComparison.CurrentCulture
                : StringComparison.CurrentCultureIgnoreCase;

            var index = pSearchDown
                ? Content.IndexOf(pSearchText, SelectionEnd, eStringComparison)
                : Content.LastIndexOf(pSearchText, SelectionStart, SelectionStart, eStringComparison);

            if (index == -1) return false;

            _lastSearchText = pSearchText;
            _lastMatchCase = pMatchCase;
            _lastSearchDown = pSearchDown;

            SelectionStart = index;
            SelectionLength = pSearchText.Length;

            return true;
        }

        private void menuitemEditFind_Click(object sender, EventArgs e)
        {
            Find();
        }

        private void Find()
        {
            if (Content.Length == 0) return;

            if (_findDialog == null)
            {
                _findDialog = new FindDialog(this);
            }

            _findDialog.Left = Left + 56;
            _findDialog.Top = Top + 160;

            if (!_findDialog.Visible)
            {
                _findDialog.Show(this);
            }
            else
            {
                _findDialog.Show();
            }

            _findDialog.Triggered();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !EnsureWorkNotLost();
        }

        private void menuitemEditFindNext_Click(object sender, EventArgs e)
        {
            if (_lastSearchText == null)
            {
                Find();
                return;
            }

            if (!FindAndSelect(_lastSearchText, _lastMatchCase, _lastSearchDown))
            {
                // ReSharper disable once LocalizableElement
                MessageBox.Show(this, string.Format("Cannot find {0}", _lastSearchText), "Notepad");
            }
        }

        private void menuitemEditReplace_Click(object sender, EventArgs e)
        {
            if (Content.Length == 0) return;

            if (_replaceDialog == null)
            {
                _replaceDialog = new ReplaceDialog(this);
            }

            _replaceDialog.Left = Left + 56;
            _replaceDialog.Top = Top + 113;

            if (!_replaceDialog.Visible)
            {
                _replaceDialog.Show(this);
            }
            else
            {
                _replaceDialog.Show();
            }

            _replaceDialog.Triggered();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.WindowPosition = Bounds;
            Settings.Save();
        }

        private class ContentPosition
        {
            public int ColumnIndex;
            public int LineIndex;
        }
    }
}