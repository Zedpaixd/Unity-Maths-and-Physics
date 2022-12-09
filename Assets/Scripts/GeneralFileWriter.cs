using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GeneralFileWriter : MonoBehaviour
{
    static StreamWriter Swriter;

    public static void writeLine(string line, string _fileName = "CollisionStats.txt")
    {
        using (Swriter = new StreamWriter("C:\\Users\\Armand\\Desktop\\"+_fileName, true))
        {
            Swriter.Write(line + "\n");
            Swriter.Flush();
            Swriter.Close();
        }
            
    }

    public static void reset(string _fileName = "CollisionStats.txt")
    {
        using (Swriter = new StreamWriter("C:\\Users\\Armand\\Desktop\\" + _fileName, false))
        {
            Swriter.Write("");
        }
    }
}
