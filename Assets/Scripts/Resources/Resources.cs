using System.Collections.Generic;

public class Resources
{
    public Dictionary<string, int> NormalResourcesQuantity { get; set; }
    public Dictionary<string, int> StrategicResourcesQuantity { get; set; }

    public Resources(List<NaturalGoodAsset> normal, List<NaturalGoodAsset> strategic)
    {
        NormalResourcesQuantity = new Dictionary<string, int>();
        foreach(NaturalGoodAsset asset in normal)
        {
            NormalResourcesQuantity.Add(asset.Name, 0);
        }
        StrategicResourcesQuantity = new Dictionary<string, int>();
        foreach (NaturalGoodAsset asset in strategic)
        {
            StrategicResourcesQuantity.Add(asset.Name, 0);
        }
    }

    public Resources(Resources resources)
    {
        NormalResourcesQuantity = new Dictionary<string, int>();
        foreach (string name in resources.NormalResourcesQuantity.Keys)
        {
            NormalResourcesQuantity.Add(name, 0);
        }
        StrategicResourcesQuantity = new Dictionary<string, int>();
        foreach (string name in resources.StrategicResourcesQuantity.Keys)
        {
            StrategicResourcesQuantity.Add(name, 0);
        }
    }

    public string DisplayQuantity()
    {
        string info = "Normal resources:\n";
        foreach (KeyValuePair <string,int> entry in NormalResourcesQuantity)
        {
            info += entry.Key + ": " + entry.Value.ToString() + "\n";
        }
        info += "Starategic resources:\n";
        foreach (KeyValuePair<string, int> entry in StrategicResourcesQuantity)
        {
            info += entry.Key + ": " + entry.Value.ToString() + "\n";
        }
        return info;
    }
}
