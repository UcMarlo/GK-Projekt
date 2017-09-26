using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Resource : ScriptableObject
{
    public String name; // resource name
    public float value;
    //TODO: check if this is working as intended
    public Dictionary<TerrainType, bool> Occurrence;
    public Text displayedName;
    public Text displayedValue;
//TODO: combine mesh from this object with mesh on map goto -> unity mesh combine
    public Texture resoureImage; // GUI image
    public Mesh mesh; // mesh displayed on map 

}
