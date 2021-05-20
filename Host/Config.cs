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
    static public void SaveToConfigIni(ref string archiwumPath, ref string dataPath, ref List<Tuple<string, string>> MACs)
    { 
        FileStream file = new FileStream("config.ini",
            FileMode.Create, FileAccess.Write);

        BinaryWriter bytes = new BinaryWriter(file); 

        bytes.Write(CodeShift(archiwumPath));
        bytes.Write(CodeShift(dataPath));

        foreach(var i in MACs)
        {
            if(i.Item1 != "" && i.Item2 != "")
            {
                bytes.Write(CodeShift(i.Item1));
                bytes.Write(CodeShift(i.Item2));
            }
        }

        bytes.Close();
    }
    ///--------------------------------------------------------------------------------------------------
    static public void ReadFromConfigIni(ref string archiwumPath, ref string dataPath, ref List<Tuple<string, string>> MACs)
    {
        //archiwumPath = "wolololo.txt";
        //dataPath = "NO_TAK.txt";

        //FileStream file = new FileStream("config.ini",
        //    FileMode.OpenOrCreate, FileAccess.Write);

        //BinaryWriter bytes = new BinaryWriter(file);

        //bytes.Write(CodeShift(archiwumPath));
        //bytes.Write(CodeShift(dataPath));

        //bytes.Close();

        FileStream file = new FileStream("config.ini",
                    FileMode.OpenOrCreate, FileAccess.Read);

        BinaryReader bytes = new BinaryReader(file);

        try
        {
            archiwumPath = DecodeShift(bytes.ReadString());
            dataPath = DecodeShift(bytes.ReadString());

            while (true)
            {
                string temp1 = DecodeShift(bytes.ReadString());
                string temp2 = DecodeShift(bytes.ReadString());
                if(temp1 != "" && temp2 != "")
                    MACs.Add(new Tuple<string, string>(temp1,temp2));
            }

        }
        catch (Exception)
        {
            ;
        }

        bytes.Close();
    }
}
