using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Military,
    Settler,
    Worker
}

public abstract class Unit : PlaceableOnMap {
    
    // ?? level ??
    //TODO: method to return unit action icon(s?)
    public int Id { get; set; }
    public string PrivateName { get; set; }
    public int CurrentHealth { get; set; }
    //public UnitType UnitType { get; set; }
    public UnitAsset UnitInfo { get; set; }
    public Player Player { get; set; }
    public Hex Hex { get; set; }
    public GameObject UnitGameObject;
    //TODO: replace GameObject to Mesh
    //public Mesh UnitMesh;
    public UnitComponent UnitComponent { get; set; }
    public bool InCity { get; set; }
    public int MovementPoints { get; set; }
    public int CombatPoints { get; set; }

    public Unit()
    {

    }

    // Setup unit
    public Unit(int id, UnitAsset unitAsset, Hex hex, Player player)
    {
        this.Id = id;
        this.UnitInfo = unitAsset;
        this.PrivateName = unitAsset.Name;
        this.Player = player;
        this.Hex = hex;
        this.CurrentHealth = unitAsset.MaxHealth;
        hex.Unit = this;
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

    // take away quantum of health damaged by enemy
    public void SubtractHealth(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Current opponent health" + CurrentHealth.ToString());
        if (CurrentHealth <= 0) Die();
    }

    // destroy unit
    public void Die()
    {
        GameObject.Destroy(UnitGameObject);
        Player.units.Remove(this);
        Hex.Unit = null;
    }

    public abstract void Execute(Hex hex, int actionNumber);

    public static Unit UnitFactory(int id, UnitAsset unitAsset, Hex hex, Player player)
    {
        switch (unitAsset.UnitType)
        {
            case UnitType.Military:
                return new MilitaryUnit(id, (MilitaryUnitAsset)unitAsset, hex, player);
            case UnitType.Settler:
                return new Settler(id, unitAsset, hex, player);
            case UnitType.Worker:
                return new Worker(id, unitAsset, hex, player);
            default: //Throw error!
                return null;
        }
    }
}
