using System;
using System.IO;
using System.Collections.Generic;

public class Config
{
	public Config()
	{
	}

    static char BINARY_SHIFT = (char)30;

    static public string DecodeShift(string x)
    {
        string nowy = "";

        for(int i = 0; i < x.Length; i++)
        {
            char t = x[i];

            t += (t - BINARY_SHIFT == (char)26 ? (char)0 : BINARY_SHIFT);

            nowy += t.ToString();
        }

        return nowy;
    }
    ///--------------------------------------------------------------------------------------------------
    static public string CodeShift(string x)
    {
        string nowy = "";

        for (int i = 0; i < x.Length; i++)
        {
            char t = x[i];

            t -= (t - BINARY_SHIFT == (char)26 ? (char)0 : BINARY_SHIFT);

            nowy += t.ToString();
        }

        return nowy;
    }
    ///--------------------------------------------------------------------------------------------------
    static public void SaveToConfigIni(ref string classesDirectory, ref string targetDirectory, ref string password,
        System.Windows.Forms.ComboBox comboBox1, ref List<string> comboBoxOptions, ref string networkPassword, ref string networkLogin, ref bool networkCheck)
    {
        FileStream file = new FileStream("config.ini",
            FileMode.Create, FileAccess.Write);

        BinaryWriter bytes = new BinaryWriter(file);

        bytes.Write(CodeShift(classesDirectory));
        bytes.Write(CodeShift(targetDirectory));
        bytes.Write(CodeShift(password));

        bytes.Write(CodeShift(networkPassword));
        bytes.Write(CodeShift(networkLogin));

        bytes.Write(networkCheck);

        for (int i=0; i < comboBoxOptions.Count; i++)
            bytes.Write(CodeShift(comboBoxOptions[i]));

        bytes.Close();

        comboBox1.Items.Clear();
        for (int i = 0; i < comboBoxOptions.Count; i++)
            comboBox1.Items.Add(comboBoxOptions[i]);

    }
    ///--------------------------------------------------------------------------------------------------
    static public void ReadFromConfigIni(ref string classesDirectory,ref string targetDirectory, ref string password,
        System.Windows.Forms.ComboBox comboBox1, ref List<string> comboBoxOptions, ref string networkPassword, ref string networkLogin, ref bool networkCheck)
    {
        //classesDirectory = "wolololo.txt";
        //targetDirectory = "NO_TAK.txt";
        //password = "qwerty";

        //FileStream file = new FileStream("config.ini",
        //    FileMode.OpenOrCreate, FileAccess.Write);

        //BinaryWriter bytes = new BinaryWriter(file);

        //bytes.Write(CodeShift(classesDirectory));
        //bytes.Write(CodeShift(targetDirectory));
        //bytes.Write(CodeShift(password));

        //string temp = CodeShift("1D (Liceum)");
        //bytes.Write(temp);
        //temp = CodeShift("1D (Technikum)");
        //bytes.Write(temp);
        //temp = CodeShift("2A");
        //bytes.Write(temp);

        //bytes.Close();

        FileStream file = new FileStream("config.ini",
                    FileMode.OpenOrCreate, FileAccess.Read);

        BinaryReader bytes = new BinaryReader(file);
        try
        {
            classesDirectory = DecodeShift(bytes.ReadString());
            targetDirectory = DecodeShift(bytes.ReadString());
            password = DecodeShift(bytes.ReadString());

            networkLogin = DecodeShift(bytes.ReadString());
            networkPassword = DecodeShift(bytes.ReadString());

            networkCheck = bytes.ReadBoolean();

            while (true)
            {
                string temp = DecodeShift(bytes.ReadString());
                comboBoxOptions.Add(temp);
            }

        }
        catch (Exception)
        {

            for (int i = 0; i < comboBoxOptions.Count; i++)
                comboBox1.Items.Add(comboBoxOptions[i]);
        }

        bytes.Close();
    }
    ///--------------------------------------------------------------------------------------------------
    static public void WorkingConfig()
    {
        FileStream file = new FileStream("config.ini",
            FileMode.Create, FileAccess.Write);

        BinaryWriter bytes = new BinaryWriter(file);

        bytes.Write(CodeShift(@"\\10.0.0.200\Wymiennik(DANE)\PONI\Klasy\klasy.txt"));
        bytes.Write(CodeShift(@"\\10.0.0.200\Wymiennik(DANE)\PONI\Temp\"));
        bytes.Write(CodeShift("123"));

        bytes.Write(CodeShift("Daniel"));
        bytes.Write(CodeShift("Daniel"));

        bytes.Write(true);

        bytes.Write(CodeShift("1D"));
        bytes.Write(CodeShift("1E"));
        bytes.Write(CodeShift("3F"));

        bytes.Close();
    }
}
