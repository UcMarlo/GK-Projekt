using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMapContinent :HexMap
{
    protected override void GenerateMap()
    {
        base.GenerateMap();

        int ContinentsQuantity = 3;
        int continentSpacing = MapColumns / ContinentsQuantity;

        for (int c = 0; c < ContinentsQuantity; c++)
        {
            int numSplats = Random.Range(4, 8);
            for (int i = 0; i < numSplats; i++)
            {
                int range = Random.Range(5, 8);
                int y = Random.Range(range, MapRows - range);
                int x = Random.Range(0, 10) - y / 2 + (c * continentSpacing);

                ElevateArea(x, y, range);
            }
        }

        //NOISE PARAMS
        float noiseResolution = 0.05f;
        Vector2 noiseOffset = new Vector2( Random.Range(-1f,1f), Random.Range(-1f, 1f));
        float noiseScale = 2f;

        
        //CREATING NOISE MAP FOR DETERMINE WHICH TERRAIN SHOULD BE ASSIGNED TO HEX
        for (int column = 0; column < MapColumns; column++)
        {
            for (int row = 0; row < MapRows; row++)
            {
                Hex h = GetHexAt(column, row);
                float n = 
                    Mathf.PerlinNoise( ((float)column/Mathf.Max(MapColumns,MapRows) / noiseResolution) + noiseOffset.x, 
                        ((float)row/Mathf.Max(MapColumns,MapRows) / noiseResolution) + noiseOffset.y )
                    - 0.5f;
                h.Elevation += n * noiseScale;
            }
        }
        UpdateHexTerrains();

    }

    private void ElevateArea(int q, int r, int range, float centerHeight = .8f)
    {
        Hex centerHex = GetHexAt(q, r);
        Hex[] areaHexs = GetHexesWithinRangeOf(centerHex, range);

        foreach (Hex h in areaHexs)
        {
            h.Elevation = centerHeight *  Mathf.Lerp( 1f, 0.25f, Mathf.Pow(Hex.Distance(centerHex, h) / range,2f) );
        }
    }
}
