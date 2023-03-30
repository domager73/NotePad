using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBook
{
    public partial class FormMain : Form
    {
        private string savedFileName;
        private bool hasUnsavedChanges;
        private string textFind;

        public FormMain()
        {
            InitializeComponent();
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.Font = fontDialog.Font;
            }
        }

        private void переносПоСловамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox.WordWrap == false)
            {
                richTextBox.WordWrap = true;
                переносПоСловамToolStripMenuItem.Checked = true;
            }
            else
            {
                richTextBox.WordWrap = false;
                переносПоСловамToolStripMenuItem.Checked = false;
            }
        }

        private void вырезатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Undo();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Redo();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectedText = "";
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
        }

        private void времяИДатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Text += DateTime.Now;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox.Text, richTextBox.Font, Brushes.Black, 10, 10);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            savedFileName = "";
            hasUnsavedChanges = false;
            this.Icon = Icon.ExtractAssociatedIcon("icon.ico");
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                savedFileName = saveFileDialog.FileName;

                toolStripStatusLabelSaveStatusName.Text = savedFileName;
                richTextBox.SaveFile(savedFileName, RichTextBoxStreamType.UnicodePlainText);
                hasUnsavedChanges = false;
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (savedFileName == "")
            {
                сохранитьКакToolStripMenuItem_Click(null, null);
            }
            else
            {
                richTextBox.SaveFile(savedFileName, RichTextBoxStreamType.PlainText);
                hasUnsavedChanges = false;
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            hasUnsavedChanges = true;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hasUnsavedChanges == true)
            {
                DialogResult res = MessageBox.Show("У вас есть не сохраненные изменения. Сохранить", "Предупреждение",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    сохранитьToolStripMenuItem_Click(null, null);
                }
            }
            savedFileName = "";
            toolStripStatusLabelSaveStatusName.Text = "Файл не сохранен";
            richTextBox.Clear();
            hasUnsavedChanges = false;
        }

        private void открытьToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (hasUnsavedChanges == true)
            {
                DialogResult res = MessageBox.Show("У вас есть не сохраненные изменения. Сохранить", "Предупреждение",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    сохранитьToolStripMenuItem_Click(null, null);
                }
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                savedFileName = openFileDialog.FileName;
                toolStripStatusLabelSaveStatusName.Text = savedFileName;
                richTextBox.LoadFile(savedFileName, RichTextBoxStreamType.UnicodePlainText);
                hasUnsavedChanges = false;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasUnsavedChanges == true)
            {
                DialogResult res = MessageBox.Show("У вас есть не сохраненные изменения. Сохранить", "Предупреждение",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    сохранитьToolStripMenuItem_Click(null, null);
                }
                else if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFind formFind = new FormFind();

            formFind.ShowDialog();

            if (formFind.GetCheckClick())
            {
                string fintText = formFind.GetText();

                if (richTextBox.Text.Length > fintText.Length)
                {
                    int j = 0;
                    int numberFound = 0;

                    while (j < richTextBox.Text.Length - fintText.Length + 1)
                    {
                        if (richTextBox.Text.Substring(j, fintText.Length) == fintText)
                        {
                            richTextBox.Select(j, fintText.Length);
                            richTextBox.SelectionColor = Color.YellowGreen;
                            j += fintText.Length;
                            numberFound++;
                        }
                        else
                        {
                            j++;
                        }
                    }

                    if (numberFound == 0) 
                    {
                        MessageBox.Show("Не один элемент не найден");
                    }
                }
            }
        }
    }
}
