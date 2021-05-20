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

namespace Host
{
    public partial class Tytul : Form
    {

        public string archiwumPath = "";
        public string dataPath = "";
        public List<Tuple<string, string>> MACs = new List<Tuple<string, string>>();
        Form2 f;    Form3 f3;

        public Tytul()
        {
            InitializeComponent();

            Config.ReadFromConfigIni(ref archiwumPath, ref dataPath, ref MACs);

            DataText.Text = dataPath;
            archiwumText.Text = archiwumPath;

            //foreach(var i in MACs)
            //    listaa.Items.Add(i.Item1 + i.Item2);

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Scan()
        {
            try
            {
                string[] files = Directory.GetFiles(dataPath, "*.txt");

                string data, MAC, classs, names;

                for (int i = 0; i < files.Length; i++)
                {
                    FileStream file = new FileStream(files[i],
                    FileMode.Open, FileAccess.Read);

                    StreamReader bytes = new StreamReader(file);

                    data = bytes.ReadLine();
                    MAC = bytes.ReadLine();
                    classs = bytes.ReadLine();
                    names = bytes.ReadToEnd();

                    bytes.Close();

                    foreach (var k in MACs)
                        if (k.Item1 == MAC)
                        {
                            MAC = k.Item2;
                            goto haveID;
                        }

                    int x = MACs.Count;

                    f = new Form2(MAC, ref MACs);
                    f.ShowDialog();

                    MAC = MACs[MACs.Count - 1].Item2;

                    Config.SaveToConfigIni(ref archiwumPath, ref dataPath, ref MACs);

                haveID:
                    string date = DateTime.Now.ToString();
                    string trueDate = "";

                    for (int j = 0; j < date.Length; j++)
                        if (date[j] == ':')
                            trueDate += "-";
                        else
                            trueDate += date.Substring(j, 1);


                    //int kk = 0;
                    //for (int it = names.Length - 3; it >= 0; it--)
                    //    if (names[it] != '\n')
                    //    {
                    //        it--;
                    //        break;
                    //    }
                    //    else
                    //        kk++;

                    string namees = names.Substring(0, names.Length - 2);

                    File.AppendAllText((archiwumPath + trueDate.Substring(0, 10) + ".txt"), (MAC + ": " + classs + " " + namees + "(" + data + ")") + Environment.NewLine);

                    listaa.Items.Add(MAC + ": " + classs + " " + names + "(" + data + ")");
                    File.Delete(files[i]);

                }
            }
            catch (Exception)
            {
                System.Diagnostics.Process.Start("Error.exe");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Scan();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Scan();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "CHANGE")
            {
                button2.Text = "SAVE";
                DataText.ReadOnly = false;
            }
            else
            {
                button2.Text = "CHANGE";
                DataText.ReadOnly = true;

                dataPath = DataText.Text;

                Config.SaveToConfigIni(ref archiwumPath,ref dataPath, ref MACs);
            }
        }

        private void ClassText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (button2.Text == "CHANGE")
                {
                    button2.Text = "SAVE";
                    DataText.ReadOnly = false;
                }
                else
                {
                    button2.Text = "CHANGE";
                    DataText.ReadOnly = true;

                    dataPath = DataText.Text;
                    Config.SaveToConfigIni(ref archiwumPath, ref dataPath, ref MACs);
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "CHANGE")
            {
                button3.Text = "SAVE";
                archiwumText.ReadOnly = false;
            }
            else
            {
                button3.Text = "CHANGE";
                archiwumText.ReadOnly = true;

                archiwumPath = archiwumText.Text;
                Config.SaveToConfigIni(ref archiwumPath,ref dataPath, ref MACs);
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (button3.Text == "CHANGE")
                {
                    button3.Text = "SAVE";
                    archiwumText.ReadOnly = false;
                }
                else
                {
                    button3.Text = "CHANGE";
                    archiwumText.ReadOnly = true;

                    archiwumPath = archiwumText.Text;
                    Config.SaveToConfigIni(ref archiwumPath,ref dataPath, ref MACs);
                }
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            f3 = new Form3(ref MACs);
            f3.ShowDialog();

            Config.SaveToConfigIni(ref archiwumPath, ref dataPath, ref MACs);
        }

        private void Tytul_Leave(object sender, EventArgs e)
        {

        }

        private void Tytul_Load(object sender, EventArgs e)
        {
            this.FormClosing += Form1_FormClosing;
        }

        private void Tytul_Click(object sender, EventArgs e)
        {

        }
    }
}
