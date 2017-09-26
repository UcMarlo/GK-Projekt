using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEditor;

public class CurrencyDatabase : ScriptableObject
{
    
    [SerializeField]
    private List<Currency> database;
    
    void OnEnable() {
        if( database == null )
            database = new List<Currency>();
    }

    public void Add( Currency currency ) {
        database.Add( currency );
    }

    public void Remove( Currency currency ) {
        database.Remove( currency );
    }

    public void RemoveAt( int index ) {
        database.RemoveAt( index );
    }

    public int COUNT {
        get { return database.Count; }
    }

    //.ElementAt() requires the System.Linq
    public Currency Currency( int index ) {
        return database.ElementAt( index );
    }

    public List<Currency> GetAll()
    {
        return database;
    }
    
    public void SortAlphabeticallyAtoZ() {
        database.Sort((x, y) => string.Compare(x.Name, y.Name));
    }
    
    [MenuItem("Assets/Create/Database/Currency")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<CurrencyDatabase>();
    }
}
