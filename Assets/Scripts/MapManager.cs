using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//TODO: while generate natural goods on hex, allow scenario that there is no natural good on hex
public class MapManager
{
    public List<NaturalGoodAsset> NormalResources { get; set; }
    public List<NaturalGoodAsset> StrategicResources { get; set; }
    public GameObject TilePrefab { get; set; }
    public MapManager(GameObject TilePrefab)
    {
        this.TilePrefab = TilePrefab;
        NormalResources = new List<NaturalGoodAsset>();
        Asset.LoadAssets("Assets/Resources/Normal resources/", NormalResources);
        StrategicResources = new List<NaturalGoodAsset>();
        Asset.LoadAssets("Assets/Resources/Strategic resources/", StrategicResources);
    }
}
