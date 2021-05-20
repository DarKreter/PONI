using System;
using System.IO;
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
    public partial class Form3 : Form
    {
        private Form1 f;

        public Form3(Form1 ff)
        {
            f = ff;
            InitializeComponent();
            ClassText.Text = f.classesDirectory;
            directTextBox.Text = f.targetDirectory;
            passTextBox.Text = f.password;

            loginTextBox.Text = f.networkLogin;
            passwordTextBox.Text = f.networkPassword;

            NetworkCheckBox(f.networkActual); 
        }

        private void Iks_Click(object sender, EventArgs e)
        {
            f.Enabled = true;
            this.Close();
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void ClassText_TextChanged(object sender, EventArgs e)
        {

        }

        private void FolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "CHANGE")
            {
                button1.Text = "SAVE";
                ClassText.ReadOnly = false;
            }
            else
            {
                button1.Text = "CHANGE";
                ClassText.ReadOnly = true;
                f.classesDirectory = ClassText.Text;

                f.Save();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "CHANGE")
            {
                button2.Text = "SAVE";
                directTextBox.ReadOnly = false;
            }
            else
            {
                button2.Text = "CHANGE";
                directTextBox.ReadOnly = true;
                f.targetDirectory = directTextBox.Text;

                f.Save();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "CHANGE")
            {
                button3.Text = "SAVE";
                passTextBox.ReadOnly = false;
            }
            else
            {
                button3.Text = "CHANGE";
                passTextBox.ReadOnly = true;
                f.password = passTextBox.Text;

                f.Save();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            f.comboBoxOptions.Clear();

            
            try
            {

                if (f.networkActual)
                {
                    ImpersonationHelper.Impersonate(f.targetDirectory, f.networkLogin, f.networkPassword, delegate
                    {
                        FileStream file = new FileStream(f.classesDirectory,
                            FileMode.Open, FileAccess.Read);

                        StreamReader strumien = new StreamReader(file);

                        while (!strumien.EndOfStream)
                        {
                            string temp = strumien.ReadLine();
                            f.comboBoxOptions.Add(temp);

                        }

                        strumien.Close();
                    });
                }
                else
                {
                    FileStream file = new FileStream(f.classesDirectory,
                         FileMode.Open, FileAccess.Read);

                    StreamReader strumien = new StreamReader(file);

                    while (!strumien.EndOfStream)
                    {
                        string temp = strumien.ReadLine();
                        f.comboBoxOptions.Add(temp);

                    }
                    strumien.Close();
                }

                f.comboBoxOptions.Sort();

                f.Save();

                System.Diagnostics.Process.Start("Success.exe");

            }
            catch (Exception)
            {
                System.Diagnostics.Process.Start("Error.exe");
            }
        }

        private void ClassText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (button1.Text == "CHANGE")
                {
                    button1.Text = "SAVE";
                    ClassText.ReadOnly = false;
                }
                else
                {
                    button1.Text = "CHANGE";
                    ClassText.ReadOnly = true;
                    f.classesDirectory = ClassText.Text;

                    f.Save();
                }
            }
        }

        private void DirectTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (button2.Text == "CHANGE")
                {
                    button2.Text = "SAVE";
                    directTextBox.ReadOnly = false;
                }
                else
                {
                    button2.Text = "CHANGE";
                    directTextBox.ReadOnly = true;
                    f.targetDirectory = directTextBox.Text;

                    f.Save();
                }
            }
        }

        private void PassTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (button3.Text == "CHANGE")
                {
                    button3.Text = "SAVE";
                    passTextBox.ReadOnly = false;
                }
                else
                {
                    button3.Text = "CHANGE";
                    passTextBox.ReadOnly = true;
                    f.password = passTextBox.Text;

                    f.Save();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "CHANGE")
            {
                button5.Text = "SAVE";
                loginTextBox.ReadOnly = false;
            }
            else
            {
                button5.Text = "CHANGE";
                loginTextBox.ReadOnly = true;
                f.networkLogin = loginTextBox.Text;

                f.Save();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "CHANGE")
            {
                button6.Text = "SAVE";
                passwordTextBox.ReadOnly = false;
            }
            else
            {
                button6.Text = "CHANGE";
                passwordTextBox.ReadOnly = true;
                f.networkPassword = passwordTextBox.Text;

                f.Save();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            NetworkCheckBox(checkBox1.Checked);
        }

        private void NetworkCheckBox(bool x)
        {

            f.networkActual = checkBox1.Checked = button6.Enabled = button5.Enabled = label5.Enabled = label6.Enabled = passwordTextBox.Enabled = loginTextBox.Enabled = x;
            if(!x)
            {
                button6.Text = "CHANGE";
                f.networkPassword = passwordTextBox.Text;

                button5.Text = "CHANGE";
                f.networkLogin = loginTextBox.Text;

            }
            f.Save();
        }

        private void loginTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (button5.Text == "CHANGE")
                {
                    button5.Text = "SAVE";
                    loginTextBox.ReadOnly = false;
                }
                else
                {
                    button5.Text = "CHANGE";
                    loginTextBox.ReadOnly = true;
                    f.networkLogin = loginTextBox.Text;

                    f.Save();
                }
            }
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (button6.Text == "CHANGE")
                {
                    button6.Text = "SAVE";
                    passwordTextBox.ReadOnly = false;
                }
                else
                {
                    button6.Text = "CHANGE";
                    passwordTextBox.ReadOnly = true;
                    f.networkPassword = passwordTextBox.Text;

                    f.Save();
                }
            }
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
