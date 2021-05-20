using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Host
{
    public partial class Form2 : Form
    {
        string MAC;
        public List<Tuple<string, string>> MACs;
        public Form2(string mac, ref List<Tuple<string, string>> m)
        {
            InitializeComponent();
            MAC = macText.Text = mac;
            macText.Left = (((this.Size.Width - 15) - macText.Size.Width) / 2);
            MACs = m;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MACs.Add(new Tuple<string, string>(MAC, textBox1.Text));
            this.Close();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MACs.Add(new Tuple<string, string>(MAC, textBox1.Text));
                this.Close();
            }
        }
    }
}
