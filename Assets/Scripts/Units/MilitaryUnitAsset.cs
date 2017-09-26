using UnityEditor;
using UnityEngine;

public class MilitaryUnitAsset : UnitAsset
{
    public int Damage;
    public int TerrainDamageMod;
    public int TerrainMoveMod;

    [MenuItem("Assets/Create/MilitaryUnit")]
    public static new void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<UnitAsset>();
    }
}
