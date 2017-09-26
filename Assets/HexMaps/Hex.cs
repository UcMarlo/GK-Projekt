using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hex : PlaceableOnMap{

    public readonly HexMap HexMap;

    Hex[] neighbours;

    //public readonly int x;
    //public readonly int y;
    //public readonly int S;
    //public readonly int R; //row
    //public readonly int Q; //column

    float radius = HexMetrics.radius;
    public float Elevation;
    bool allowWrapEastWest = true;
    bool allowWrapNorthSouth = false;

    public Unit unit;

    NaturalUpgrade naturalUpgrade;
    Resource resource;
    public HexTerrain hexTerrain;
    Upgrade upgrade;

    public City City { get; set; }
    public Unit Unit { get; set; }
    public bool MainCityHex { get; set; }

    public Mesh localMesh;

    public Mesh ColiderMesh;

    #region CURRENCY

    public Dictionary<Currency, int> currencyProfitPerTurn;    

    #endregion
    
    

    public Hex(HexMap hexMap, int q, int r) {
        this.HexMap = hexMap;
        Q = q;
        R = r;
        S = -(q + r);
    }

    public Hex(HexMap hexMap, HexTerrain hexTerrain, int q, int r)
    {
        this.hexTerrain = hexTerrain;
        this.HexMap = hexMap;
        Q = q;
        R = r;
        S = -(q + r);
        initCurrencies();
        recalculateCurrencyPerTurn();
    }


    private void initCurrencies()
    {
        currencyProfitPerTurn = new Dictionary<Currency, int>();
        foreach (var currency in HexMap.CurrencyDatabase.GetAll())
        {
            currencyProfitPerTurn.Add(currency,0);
        }
    }
    
    public Dictionary<Currency, int> recalculateCurrencyPerTurn()
    {
        foreach (Currency iCurrency in HexMap.CurrencyDatabase.GetAll())
        {
            currencyProfitPerTurn[iCurrency] = 0;
        }
        for (int i = 0; i < hexTerrain.KeyCurrencies.Count; i++)
        {
            currencyProfitPerTurn[hexTerrain.KeyCurrencies[i]] += hexTerrain.ValueCurrencies[i]; //VALUE OF TERRAIN
        }
        
        //TODO: Insert there value of upgrade
        return currencyProfitPerTurn;
    }
    
    #region GEOMETRIC_STUFF
    
    public Hex[] GetNeightbours()
    {
        if (this.neighbours != null)
        {
            return this.neighbours;
        }

        
        Hex[] found = new Hex[6];
        
        found[0] = HexMap.GetHexAt( Q + 1, R + 0 ); //RIGHT
        found[1] = HexMap.GetHexAt( Q - 1, R + 0 ); //LEFT
        found[2] = HexMap.GetHexAt( Q + 0, R + 1 ); //UP RIGHT
        found[3] = HexMap.GetHexAt( Q + 0, R - 1 ); //DOWN LEFT
        found[4] = HexMap.GetHexAt( Q - 1, R + 1 ); //UP LEFT
        found[5] = HexMap.GetHexAt( Q + 1, R - 1 ); //DOWN RIGHT

        neighbours = found;
        return neighbours;
    }

    #endregion
    
    public static float Distance(Hex a, Hex b)
    {
        int dQ = Mathf.Abs(a.Q - b.Q);
        if(a.HexMap.AllowHorizontalWrap)
        {
            if(dQ > a.HexMap.MapColumns / 2)
                dQ = a.HexMap.MapColumns - dQ;
        }

        int dR = Mathf.Abs(a.R - b.R);
        if(a.HexMap.AllowVerticalWrap)
        {
            if(dR > a.HexMap.MapRows / 2)
                dR = a.HexMap.MapRows - dR;
        }

        return 
            Mathf.Max( 
                dQ,
                dR,
                Mathf.Abs(a.S - b.S)
            );
    }
}
