using UnityEditor;
using UnityEngine;

public class UnitAsset : Asset
{
    [SerializeField] public string Name;
    [SerializeField] public GameObject UnitPrefab;
    [SerializeField] public Mesh Mesh;
    [SerializeField] public MeshCollider MeshCollider;
    [SerializeField] public UnitType UnitType;
    [SerializeField] public int MaxHealth;
    //TODO Map<Resource,Integer>
    [SerializeField] public int Cost;
    [SerializeField] public int MaintainceCost;
    [SerializeField] public int MoveRange;
    [SerializeField] public bool Land;
    [SerializeField] public bool Water;

    public UnitAsset()
    {

    }

    [MenuItem("Assets/Create/Unit")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<UnitAsset>();
    }
}
