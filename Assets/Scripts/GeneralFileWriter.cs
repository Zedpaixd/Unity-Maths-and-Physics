using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GeneralFileWriter : MonoBehaviour
{
    static StreamWriter Swriter;

    void Start()
    {
        
    }

    public static void writeLine(string line)
    {
        using (Swriter = new StreamWriter("C:\\Users\\Armand\\Desktop\\CollisionStats.txt", true))
        {
            Swriter.Write(line + "\n");
            Swriter.Flush();
            Swriter.Close();
        }
            
    }

    public static void reset()
    {
        using (Swriter = new StreamWriter("C:\\Users\\Armand\\Desktop\\CollisionStats.txt", false))
        {
            Swriter.Write("");
        }
    }
}
