using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class NaturalGoodAsset : Asset
{
    public string Name;
    public GameObject RawForm;
    public GameObject ArmedForm;
    public Image Icon;
    public bool TechnologyRequired;
    public bool Strategic;
}
