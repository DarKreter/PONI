using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form2 : Form
    {
        public Form1 f;
        private short ile = 0;
        public Form2(Form1 ff)
        {
            f = ff;
            InitializeComponent();
        }     

        private void Form2_Load(object sender, EventArgs e)
        {

        }


        private void Button1_Click_1(object sender, EventArgs e)
        {
            f.Enabled = true;
            this.Close();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            f.Enabled = true;
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if(kod.Text != f.password)
            {
                if(++ile == 3)
                {
                    f.Enabled = true;
                    this.Close();
                }
                kod.Text = "ERROR";
                kod.PasswordChar = '\0';
            }
            else
            {
                Form3 thirdForm = new Form3(f);
                thirdForm.Show();
                this.Close();
            }
        }

        private void Kod_TextChanged(object sender, EventArgs e)
        {
            kod.PasswordChar = '*';
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void Kod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (kod.Text != f.password)
                {
                    if (++ile == 3)
                    {
                        f.Enabled = true;
                        this.Close();
                    }
                    kod.Text = "ERROR";
                    kod.PasswordChar = '\0';
                }
                else
                {
                    Form3 thirdForm = new Form3(f);
                    thirdForm.Show();
                    this.Close();
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                f.Enabled = true;
                this.Close();
            }
        }
    }
}
