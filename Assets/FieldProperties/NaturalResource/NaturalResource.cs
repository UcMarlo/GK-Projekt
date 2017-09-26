using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class NaturalResource : ScriptableObject
{
    [SerializeField] public int Id;
    [SerializeField] public String Name;
    [SerializeField] public Mesh Mesh;
    [SerializeField] public Material Material;
    [SerializeField] public int MeshChunksOnHex;
    [SerializeField] public List<Currency> KeyCurrencies;
    [SerializeField] public List<int> ValueCurrencies;
    [SerializeField] public List<HexTerrain> SpawnLocations;

    public NaturalResource(int id, string name, Mesh mesh, Material material, int meshChunksOnHex,
        List<Currency> keyCurrencies, List<int> valueCurrencies)
    {
        Id = id;
        Name = name;
        Mesh = mesh;
        Material = material;
        MeshChunksOnHex = meshChunksOnHex;
        KeyCurrencies = keyCurrencies;
        ValueCurrencies = valueCurrencies;
    }

    [MenuItem("Assets/Create/NaturalResource")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<NaturalResource>();
    }
}