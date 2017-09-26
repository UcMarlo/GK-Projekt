using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UnitsController
{
    public Unit SelectedUnit { get; set; }
    Color normalColor;

    public UnitsController()
    {
    }

    public void SelectUnit(GameObject ourHitObject, Player player)
    {
        if (SelectedUnit != null)
        {
            UnselectSelectedUnit();
        }
        if(ourHitObject.GetComponent<UnitComponent>().UnitLogic.Player != player)
        {
            return;
        }
        SelectedUnit = ourHitObject.GetComponent<UnitComponent>().UnitLogic;
        MeshRenderer mr = ourHitObject.GetComponent<MeshRenderer>() as MeshRenderer;
        normalColor = mr.material.color;
        mr.material.color = Color.black;
        //gameManager.guiManager.guiElements.UnitActionMenu.SetActive(true);
    }

    public void UnselectSelectedUnit()
    {
        if (SelectedUnit != null)
        {
            MeshRenderer mr = SelectedUnit.UnitGameObject.GetComponent<MeshRenderer>() as MeshRenderer;
            mr.material.color = normalColor;
            SelectedUnit = null;
            //gameManager.guiManager.guiElements.UnitActionMenu.SetActive(false);
        }
    }

    public void ExecuteUnitAction(int actionNumber)
    {
        SelectedUnit.Execute(SelectedUnit.Hex, actionNumber);
        if (SelectedUnit.UnitInfo.UnitType == UnitType.Settler)
        {
            UnselectSelectedUnit();
            //SelectedUnit.UnitComponent.UnitLogic.Die();
        }
    }

    public void MoveUnit(Hex hex)
    {
        if (hex.Unit == null)
        {
            if (hex.MainCityHex)
            {
                SelectedUnit.InCity = true;
                hex.City.UnitsInCity.Add(SelectedUnit);
            }
            else
            {
                if (SelectedUnit.InCity)
                {
                    SelectedUnit.Hex.City.UnitsInCity.Remove(SelectedUnit);
                    if(SelectedUnit.Hex.City.UnitInCityNumber > 0) SelectedUnit.Hex.City.UnitInCityNumber--;
                }
                SelectedUnit.InCity = false;
            }
            SelectedUnit.Hex.Unit = null;
            SelectedUnit.Hex = hex;  
            hex.Unit = SelectedUnit;
            SelectedUnit.UnitComponent.UpdatePosition();
        }
        else
        {
            if(SelectedUnit.UnitInfo.UnitType == UnitType.Military && SelectedUnit.Player != hex.Unit.Player)
            {
                ((MilitaryUnit)SelectedUnit).Attack(hex);
            }
        }
    }

    public void TakeToNextTurn(List<Unit> units)
    {
        foreach(Unit unit in units)
        {
            unit.MovementPoints = unit.UnitInfo.MoveRange;
            unit.CombatPoints = 1;
        }
    }
}
