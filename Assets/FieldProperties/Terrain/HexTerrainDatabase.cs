using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HexTerrainDatabase : ScriptableObject {
    [SerializeField]
    private List<HexTerrain> database;
	
    void OnEnable() {
        if( database == null )
            database = new List<HexTerrain>();
    }
	
    public void Add( HexTerrain hexTerrain ) {
        database.Add( hexTerrain );
    }
	
    public void Remove( HexTerrain hexTerrain ) {
        database.Remove( hexTerrain );
    }
	
    public void RemoveAt( int index ) {
        database.RemoveAt( index );
    }
	
    public int COUNT {
        get { return database.Count; }
    }

    public List<HexTerrain> getAllRecords()
    {
        return database;
    }
	
    //.ElementAt() requires the System.Linq
    public HexTerrain Terrain( int index ) {
         return database.ElementAt( index );
     }
 	
     public void SortAlphabeticallyAtoZ() {
         database.Sort((x, y) => string.Compare(x.Name, y.Name));
     }
 }