using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DateControlLibrary;

namespace DateControlTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.maskedTextBox1.Mask = "";
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {

        }
    }
}
