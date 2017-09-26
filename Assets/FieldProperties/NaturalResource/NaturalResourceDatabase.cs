using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class NaturalResourceDatabase : ScriptableObject
{
    [SerializeField] private List<NaturalResource> database;

    void OnEnable()
    {
        if (database == null)
            database = new List<NaturalResource>();
    }

    public void Add(NaturalResource naturalResource)
    {
        database.Add(naturalResource);
    }

    public void Remove(NaturalResource naturalResource)
    {
        database.Remove(naturalResource);
    }

    public void RemoveAt(int index)
    {
        database.RemoveAt(index);
    }

    public int COUNT
    {
        get { return database.Count; }
    }

    public List<NaturalResource> getAllRecords()
    {
        return database;
    }


    public NaturalResource get(int index)
    {
        return database.ElementAt(index);
    }

    public void SortAlphabeticallyAtoZ()
    {
        database.Sort((x, y) => string.Compare(x.Name, y.Name));
    }

    [MenuItem("Assets/Create/Database/NaturalResourceDatabase")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<NaturalResourceDatabase>();
    }
}