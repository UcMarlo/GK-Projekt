using UnityEngine;

//TODO: method to return available upgrading action icons and link upgrades building with these icons
//TODO: add resources to city and/or player resources balance
//TODO: fix bug - GameObject on tile are smaller than should be
public class NaturalGood
{
    public NaturalGoodAsset Info;
    public GameObject RawForm { get; set; }
    public GameObject ArmedForm { get; set; }
    public string ResourceName { get; set; }
    public int ResourceQuantity { get; set; }

    public NaturalGood(NaturalGoodAsset asset, Hex hex)
    {
        Info = asset;
        ResourceName = asset.Name;
        //RawForm = GameObject.Instantiate(Info.RawForm,tile.Tile.transform);
        //ArmedForm = GameObject.Instantiate(Info.ArmedForm, tile.Tile.transform);
        ArmedForm.SetActive(false);
        foreach (Collider c in RawForm.GetComponents<Collider>())
        {
            c.enabled = false;
        }
        foreach (Collider c in ArmedForm.GetComponents<Collider>())
        {
            c.enabled = false;
        }
    }

    public void BuildUpgrade(Hex hex)
    {
        RawForm.SetActive(false);
        ArmedForm.SetActive(true);
        ResourceQuantity *= 2;
        //ArmedForm.transform.position += tile.Tile.transform.position;
        //if (Info.Strategic)
        //{
        //    tile.City.ResourceBalance.StrategicResourcesQuantity[Info.Name] += ResourceQuantity;
        //}
        //else
        //{
        //    tile.City.ResourceBalance.NormalResourcesQuantity[Info.Name] += ResourceQuantity;
        //}

    }

    public void RemoveUpgrade(Hex hex)
    {
        ResourceQuantity /= 2;
        if (Info.Strategic)
        {
            //tile.City.ResourceBalance.StrategicResourcesQuantity[Info.Name] -= ResourceQuantity;
        }
        else
        {
            //tile.City.ResourceBalance.NormalResourcesQuantity[Info.Name] -= ResourceQuantity;
        }
        ArmedForm.SetActive(false);
        RawForm.SetActive(true);
    }
}
