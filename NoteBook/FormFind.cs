using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBook
{
    public partial class FormFind : Form
    {
        private string findText;
        private bool checkClick;

        public FormFind()
        {
            InitializeComponent();
            findText = string.Empty;
            checkClick = false;
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            findText = textBoxFind.Text;
            checkClick = true;
            this.Close();
        }

        public string GetText() => findText;
        public bool GetCheckClick() => checkClick;
    }
}
