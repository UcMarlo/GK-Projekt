using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class MilitaryUnit : Unit
{
    public int TerrainDamageMod { get; set; }
    public int TerrainMoveMod { get; set; }
    public int Expirience { get; set; }
    private const int expValue = 20;
    private int Damage;
    //public new MilitaryUnitAsset UnitInfo { get; set; }

    public MilitaryUnit(int id, MilitaryUnitAsset unitAsset, Hex hex, Player player)
    {
        this.Id = id;
        this.UnitInfo = unitAsset;
        this.PrivateName = unitAsset.Name;
        this.Player = player;
        this.Damage = unitAsset.Damage;
        this.Hex = hex;
        this.CurrentHealth = unitAsset.MaxHealth;
        hex.Unit = this;
        this.PrivateName = "Soldier";
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

    // attack enemy
    // usage - GameManager - selectedUnit.Attack(UnitOnHex)
    // ?? or replace it with universal move method ??
    public void Attack(Hex hex)
    {
        Debug.Log("Attack");
        Unit opponent = hex.Unit;
        if (opponent.UnitInfo.UnitType == UnitType.Military)
        {
            opponent.SubtractHealth(Damage);
            GainExpirience();
        }
        //if (tile.Unit == null)
        //{
        //    move unit
        //}
    }

    public override void Execute(Hex hex, int actionNumber)
    {
        switch (actionNumber)
        {
            case 0:
                if (CurrentHealth + 10 <= UnitInfo.MaxHealth)
                {
                    CurrentHealth += 10;
                }
                else if (CurrentHealth < UnitInfo.MaxHealth)
                {
                    CurrentHealth = UnitInfo.MaxHealth;
                }
                break;
        }
            
    }

    public void GainExpirience()
    {
        Expirience += expValue;
        Debug.Log("Current expirience: " + Expirience);
        if (Expirience > 100)
        {
            LevelUp(0);
            Expirience -= 100;
        }
    }

    // upgrade unit (obsolete -> brand new)
    public void Upgrade()
    {

    }

    public void LevelUp(int option)
    {
        switch (option)
        {
            case 0:
                UnitInfo.MaxHealth += 50;
                break;
            case 1:
                Damage += 20;
                break;
        }
        Debug.Log("New level gained.\nCurrent stats:\nMax HP: " + UnitInfo.MaxHealth + "\nDamage: " + Damage + "\netc.");
        // increase damage, health etc.
    }

    // overtake enemy utility (or even military) unit
    public void Overtake(Unit enemyUnit)
    {
        enemyUnit.Player = this.Player;
    }
}
