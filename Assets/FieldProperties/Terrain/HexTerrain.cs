using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[System.Serializable]
public class HexTerrain : ScriptableObject
{
    [SerializeField] public int Id;
    [SerializeField] public string Name;
    [SerializeField] public TerrainType Type;
    [SerializeField] public float MoveInCostModifier; // -2.0 to +2.0
    [SerializeField] public float Elevation; //for Map generation
    [SerializeField] public Material Material;
    [SerializeField] public int BiomeId;
    [SerializeField] public List<Currency> KeyCurrencies;
    [SerializeField] public List<int> ValueCurrencies;

    public HexTerrain(int id, string name, TerrainType type, float moveInCostModifier, float elevation, Material material,
        int biomeId)
    {
        this.Name = name;
        this.Type = type;
        this.MoveInCostModifier = moveInCostModifier;
        this.Elevation = elevation;
        this.Material = material;
        this.BiomeId = biomeId;

    }
    
    [MenuItem("Assets/Create/Terrain")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<HexTerrain>();
    }
    
    
    
}
