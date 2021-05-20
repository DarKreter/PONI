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
using System.Threading;
using System.Net.NetworkInformation;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace Client
{
    public partial class Form1 : Form
    {
        private bool textBox = false;
        private bool checkBox = false;
        public string classesDirectory = "";
        public string targetDirectory = "";
        public string password = "";

        public string networkLogin = "", networkPassword = "";
        public bool networkActual = false;

        public List<string> comboBoxOptions;

        public Form1()
        {
            comboBoxOptions = new List<string>();
            InitializeComponent();
            comboBox1.Items.Clear();

            //Config.WorkingConfig();

            Config.ReadFromConfigIni(ref classesDirectory, ref targetDirectory,ref password, comboBox1, 
                ref comboBoxOptions, ref networkPassword, ref networkLogin, ref networkActual);
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
                checkBox = true;
            else
                checkBox = false;

            if (checkBox && textBox)
                button1.Visible = true;
            else
                button1.Visible = false;
        }

        public void Save()
        {
            Config.SaveToConfigIni(ref classesDirectory, ref targetDirectory, ref password, 
                comboBox1, ref comboBoxOptions, ref networkPassword, ref networkLogin, ref networkActual);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                textBox = true;
            else
                textBox = false;

            if (checkBox && textBox)
                button1.Visible = true;
            else
                button1.Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            Form2 secondForm = new Form2(this);
            secondForm.Show();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                System.Diagnostics.Process.Start("errorr.exe");
                return;
            }

            string date = DateTime.Now.ToString();
            string trueDate = "";

            var macAddr =
             (
                 from nic in NetworkInterface.GetAllNetworkInterfaces()
                 where nic.OperationalStatus == OperationalStatus.Up
                 select nic.GetPhysicalAddress().ToString()
             ).FirstOrDefault();

            for (int i = 0; i < date.Length; i++)
                if (date[i] == ':')
                    trueDate += "-";
                else
                    trueDate += date.Substring(i,1);
            try
            {
                FileStream file = new FileStream("temporary.txt",
                FileMode.Create, FileAccess.Write);

                StreamWriter bytes = new StreamWriter(file);

                bytes.WriteLine(date);
                bytes.WriteLine(macAddr);
                bytes.WriteLine(comboBox1.SelectedItem);
                bytes.WriteLine(textBox1.Text);

                bytes.Close();

                if(networkActual)
                {
                    ImpersonationHelper.Impersonate(targetDirectory, networkLogin, networkPassword, delegate
                    {
                        if (!File.Exists(targetDirectory + trueDate + "_" + macAddr + ".txt"))
                        {
                            File.Copy("temporary.txt", targetDirectory + trueDate + "_" + macAddr + ".txt");
                        }
                    });
                }
                else
                {
                    if (!File.Exists(targetDirectory + trueDate + "_" + macAddr + ".txt"))
                    {
                        File.Copy("temporary.txt", targetDirectory + trueDate + "_" + macAddr + ".txt");
                    }
                }









                //var credentials = new NetworkCredential("rejestracja", "rej_1234");

                //using (new NetworkConnection(@"\\K3901\write", credentials))
                //{
                //    System.Diagnostics.Process.Start("SuccesSs.exe");
                //    //File.Copy(@"lol.txt", targetDirectory + "write\\" + "lol.txt");
                //}

                System.Diagnostics.Process.Start("SuccesSs.exe");
            }
            catch (Exception)
            {
                System.Diagnostics.Process.Start("Error.exe");
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
