using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class City
{
    public Player Player { get; set;}
    public Resources ResourceBalance { get; set; }
    public List<Hex> Hexes { get; set; }
    public int Population { get; set; }
    public string Name { get; set; }
    public GameObject CityGameObject { get; set; }
    CityComponent cityComponent;
    public List<Unit> UnitsInCity { get; set; }
    UnitDatabase unitsAssets;
    Hex mainHex;
    public int UnitInCityNumber { get; set; }

    public City(Hex hex, Player player)
    {
        this.Player = player;
        //ResourceBalance = new Resources(Player.ResourceBalance);
        mainHex = hex;
        hex.MainCityHex = true;
        Hexes = new List<Hex>();
        AddHex(hex);
        foreach(Hex h in hex.GetNeightbours())
        {
            AddHex(h);
        }
        Population = 1;
        Name = "TestCity";
        CityGameObject = GameObject.Instantiate(Player.cityPrefab);
        CityGameObject.transform.position += hex.Position();
        cityComponent = CityGameObject.AddComponent<CityComponent>();
        cityComponent.CityGameObject = CityGameObject;
        cityComponent.CityLogic = this;
        UnitsInCity = new List<Unit>();
        this.unitsAssets = this.Player.gameManager.UnitDatabase;
        UnitInCityNumber = 0;
    }

    // add hex and its neighbours to list
    public void AddHex(Hex hex)
    {
        hex.City = this;
        //ResourceBalance = ResourceBalance + tile.Resources;
        //foreach (NaturalGood resource in hexes.Resources)
        //{
        //    //ResourceBalance.NormalResourcesQuantity[resource.Info.Name] += resource.ResourceQuantity;
        //}
        Hexes.Add(hex);
    }

    //TODO: Highlight hexes
    public void SelectCity(int playerNumber)
    {
        ShowInfo();
        if(UnitsInCity.Count > 0)
        {
            if (UnitInCityNumber >= UnitsInCity.Count)
            {
                UnitInCityNumber = 0;
            }
            Player.gameManager.UnitsController.SelectUnit(UnitsInCity[UnitInCityNumber].UnitGameObject,Player);
            Debug.Log("Selected:" + UnitsInCity[UnitInCityNumber].PrivateName);
            UnitInCityNumber++;
        }
    }

    public void ShowInfo()
    {
        string info = this.Name;
        info += "\nArea: " + Hexes.Count.ToString() + " tile(s)";
        info += "\nPopulation level: " + Population.ToString();
        //info += "\n" + ResourceBalance.DisplayQuantity();
        Debug.Log(info);
    }

    public void HighlightHexes()
    {
        foreach (Hex h in Hexes)
        {
            // highlight hex
        }
    }

    public void ExpandBorders()
    {

    }

    public void CreateUnit(int number)
    {
        Unit newUnit = Unit.UnitFactory(123, unitsAssets.Unit(number), mainHex, Player);
        newUnit.InCity = true;
        Player.units.Add(newUnit);
        UnitsInCity.Add(newUnit);
    }

    public void CreateUnit(UnitAsset unit)
    {
        Unit newUnit = Unit.UnitFactory(123, unit, mainHex, Player);
        newUnit.InCity = true;
        Player.units.Add(newUnit);
        UnitsInCity.Add(newUnit);
    }
}
