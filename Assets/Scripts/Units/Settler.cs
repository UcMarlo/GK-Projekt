using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//TODO: add city building condition (there is no different city in some area etc)
[CreateAssetMenu()]
public class Settler : Unit
{
    public Settler(int id, UnitAsset unitAsset, Hex hex, Player player)
    {
        this.Id = id;
        this.UnitInfo = unitAsset;
        this.PrivateName = unitAsset.Name;
        this.Player = player;
        this.Hex = hex;
        this.CurrentHealth = unitAsset.MaxHealth;
        hex.Unit = this;
        this.PrivateName = "Settler";
        UnitGameObject = GameObject.Instantiate(unitAsset.UnitPrefab);
        UnitGameObject.transform.position += hex.Position();
        UnitComponent = UnitGameObject.AddComponent<UnitComponent>();
        UnitComponent.UnitLogic = this;
        foreach (Collider c in UnitGameObject.GetComponents<Collider>())
        {
            c.enabled = false;
        }
        foreach (Collider c in UnitGameObject.GetComponents<Collider>())
        {
            c.enabled = false;
        }
    }

    public override void Execute(Hex hex, int actionNumber)
    {
        Debug.Log("Build new city");
        //tile.City = new City(tile, this.Player);
        new City(hex, this.Player);
        Die();
    }
}
