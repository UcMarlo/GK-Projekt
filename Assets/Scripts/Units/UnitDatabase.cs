using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEditor;

public class UnitDatabase : ScriptableObject
{
    [SerializeField]
    private List<UnitAsset> database;

    void OnEnable()
    {
        if (database == null)
            database = new List<UnitAsset>();
    }

    public void Add(UnitAsset unitAsset)
    {
        database.Add(unitAsset);
    }

    public void Remove(UnitAsset unitAsset)
    {
        database.Remove(unitAsset);
    }

    public void RemoveAt(int index)
    {
        database.RemoveAt(index);
    }

    public int COUNT
    {
        get { return database.Count; }
    }

    //.ElementAt() requires the System.Linq
    public UnitAsset Unit(int index)
    {
        return database.ElementAt(index);
    }

    public void SortAlphabeticallyAtoZ()
    {
        database.Sort((x, y) => string.Compare(x.Name, y.Name));
    }

    [MenuItem("Assets/Create/UnitDatabase")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<UnitDatabase>();
    }
}