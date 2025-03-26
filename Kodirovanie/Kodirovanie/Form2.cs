using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kodirovanie
{
    public partial class Form2 : Form
    {
        public Form2(double[] colorChannel)
        {
            InitializeComponent();
            
            for (int i = 0; i < colorChannel.Length; i++)
            {
                label1.Text += $"{i}: {colorChannel[i]:f4}  ";
                if (i % 8 == 0 && i != 0) label1.Text += "\n";
            }
        }
    }
}
