using System.Collections.Generic;
using UnityEngine;

//TODO: get possible natural upgrades icons and upgrade resource depend on selected icon
public class Player
{
    public int Number { get; set; }
    public List<City> cities { get; set; }
    public List<Unit> units { get; set; }
    public Resources ResourceBalance { get; set; }
    public List<UnitAsset> unitsAssets;
    public Unit testSoldier;
    public Unit testWorker;
    public Unit testSettler;
    public GameManager gameManager { get; set; }
    public UnitDatabase UnitDatabase { get; set; }
    public List<UnitAsset> AvailableUnits { get; set; }

    public GameObject cityPrefab;

    public Player(int number, GameManager gameManager)
    {
        this.Number = number;
        this.gameManager = gameManager;
        this.cityPrefab = gameManager.cityPrefab;
        cities = new List<City>();
        units = new List<Unit>();
        //unitsAssets = new List<UnitAsset>();
        this.UnitDatabase = gameManager.UnitDatabase;
        //Asset.LoadAssets("Assets/Resources/Units/", unitsAssets);
        //ResourceBalance = new Resources(gameManager.MapManager.NormalResources, gameManager.MapManager.StrategicResources);
        AvailableUnits = new List<UnitAsset>();
    }

    public void AddCity(City city)
    {
        cities.Add(city);
    }

    private Hex[] SetInitHexes(int x, int y)
    {
        
        for(int i = x; i < gameManager.HexMap.MapColumns; i++)
        {
            for (int j = y; j < gameManager.HexMap.MapRows; j++)
            {
                if(gameManager.HexMap.GetHexAt(i, j).hexTerrain.Type == TerrainType.FLAT ||
                    gameManager.HexMap.GetHexAt(i, j).hexTerrain.Type == TerrainType.HILL)
                {
                    foreach (Hex neightbour in gameManager.HexMap.GetHexAt(i, j).GetNeightbours())
                    {
                        if (neightbour.hexTerrain.Type == TerrainType.FLAT ||
                            neightbour.hexTerrain.Type == TerrainType.HILL)
                        {
                            Hex[] results = { gameManager.HexMap.GetHexAt(i, j), neightbour };
                            return results;
                        }
                    }
                }
            }
        }
        return null;

    }

    public void Init(int x, int y)
    {
        Hex[] initHexes = SetInitHexes(x, y);
        if (gameManager.UnitDatabase.COUNT > 0)
        {
            testSoldier = Unit.UnitFactory(123, gameManager.UnitDatabase.Unit(0), initHexes[0], this);
            testSoldier.UnitComponent.UnitLogic.PrivateName = "MySoldier";
            units.Add(testSoldier);
        }
            if (gameManager.UnitDatabase.COUNT > 2)
        {
            testSettler = Unit.UnitFactory(345, gameManager.UnitDatabase.Unit(2), initHexes[1], this);
            testSettler.UnitComponent.UnitLogic.PrivateName = "MySettler";
            units.Add(testSettler);
        }
    }

    public void InstantiateTestUnits()
    {
        if (gameManager.UnitDatabase.COUNT > 0)
        {
            testSoldier = Unit.UnitFactory(123, gameManager.UnitDatabase.Unit(0), gameManager.HexMap.GetHexAt(3,3), this);
            testSoldier.UnitComponent.UnitLogic.PrivateName = "MySoldier";
            units.Add(testSoldier);
            if (gameManager.UnitDatabase.COUNT > 1)
            {
                testWorker = Unit.UnitFactory(234, gameManager.UnitDatabase.Unit(1), gameManager.HexMap.GetHexAt(4, 4), this);
                testWorker.UnitComponent.UnitLogic.PrivateName = "MyWorker";
                units.Add(testWorker);
                if (gameManager.UnitDatabase.COUNT > 2)
                {
                    testSettler = Unit.UnitFactory(345, gameManager.UnitDatabase.Unit(2), gameManager.HexMap.GetHexAt(5, 5), this);
                    testSettler.UnitComponent.UnitLogic.PrivateName = "MySettler";
                    units.Add(testSettler);
                }
            }
        }
    }

    public void UnlockUnit(UnitAsset[] units)
    {
        foreach (UnitAsset unit in units)
        {
            AvailableUnits.Add(unit);
        }
    }
}
