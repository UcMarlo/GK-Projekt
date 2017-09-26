using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ?? upgrading conditions ??
public class Worker : Unit
{
    public Worker(int id, UnitAsset unitAsset, Hex hex, Player player)
    {
        this.Id = id;
        this.UnitInfo = unitAsset;
        this.PrivateName = unitAsset.Name;
        this.Player = player;
        this.Hex = hex;
        this.CurrentHealth = unitAsset.MaxHealth;
        hex.Unit = this;
        this.PrivateName = "Worker";
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
        Debug.Log("Worker is working");
        //tile.Upgrade(actionNumber);
        //switch (actionNumber)
        //{
        //case 0:
        //    Debug.Log("Upgrade first resource");
        //    tile.Upgrade(tile, actionNumber);
        //    break;
        //case 1:
        //    Debug.Log("Upgrade second resource");
        //    tile.Upgrade(tile, actionNumber);
        //    break;
        //}
    }
}
