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
    public partial class Form3 : Form
    {
        public List<Tuple<string, string>> MACs;

        public Form3(ref List<Tuple<string, string>> m)
        {
            InitializeComponent();
            MACs = m;


            foreach (var i in MACs)
            {
                listaa.AppendText(i.Item2 + ":\t\t" + i.Item1);
                listaa.AppendText(Environment.NewLine);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MACs.Clear();
            int dlugosc = listaa.Lines.Length;

            for (int k = 0; k < dlugosc - 1; k++)
            {
                string i = listaa.Lines[k];

                string item1 = "", item2 = "";

                for (int j = 0; j < i.Length; j++)
                {
                    if (i.Substring(j, 3) == ":\t\t")
                    {
                        item1 = i.Substring(j + 3, i.Length - j - 3);
                        item2 = i.Substring(0, j);
                        break;
                    }
                }

                MACs.Add(new Tuple<string, string>(item1, item2));
            }

            this.Close();
        }

        private void Listaa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
