using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

[Serializable]
public class ProgressNodeDetails
{
    public string Name;
    public int Id;
    public int Price;
    public int Tier;
    public int[] Previous;
    public UnitAsset[] unitAsset;
        
    //TODO - private string[] units;     // eventually replace: string name -> int id
    //TODO - private string[] buildings; // eventually replace: string name -> int id
}
