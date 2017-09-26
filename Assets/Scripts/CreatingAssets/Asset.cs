using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Asset :ScriptableObject
{
    public int Id;

    public static void LoadAssets<T>(string localPath, Dictionary<int, T> dict) where T : Asset
    {
        int i = 0;
        foreach (string anotherAssetName in Directory.GetFiles((Directory.GetCurrentDirectory()) + "\\" + localPath.Replace('/', '\\')))
        {
            if (Path.GetFileName(anotherAssetName.Split('.')[anotherAssetName.Split('.').Length - 1]).Equals("asset"))
            {
                dict.Add(/*((T)Activator.CreateInstance(typeof(T))).Id*/i, (T)AssetDatabase.LoadAssetAtPath((localPath + Path.GetFileName(anotherAssetName)), typeof(T)));
                i++;
            }

        }
    }

    public static void LoadAssets<T>(string localPath, List<T> list) where T : Asset
    {
        int i = 0;
        foreach (string anotherAssetName in Directory.GetFiles((Directory.GetCurrentDirectory()) + "\\" + localPath.Replace('/', '\\')))
        {
            if (Path.GetFileName(anotherAssetName.Split('.')[anotherAssetName.Split('.').Length - 1]).Equals("asset"))
            {
                list.Add((T)AssetDatabase.LoadAssetAtPath((localPath + Path.GetFileName(anotherAssetName)), typeof(T)));
                i++;
            }

        }
    }
}
