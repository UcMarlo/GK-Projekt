using UnityEngine;

public class PlaceableOnMap
{
    
    public int x;
    public int y;
    public int S;
    public int R; //row
    public int Q; //column
    bool allowWrapEastWest = true;
    bool allowWrapNorthSouth = false;

    public Vector3 Position()
    {
        return new Vector3(
            HexHorizontalSpacing() * (this.Q + this.R / 2f),
            0,
            HexVerticalSpacing() * this.R
        );
    }

    public float HexHeight()
    {
        return HexMetrics.Height;
    }

    public float HexWidth()
    {
        return HexMetrics.Width;
    }

    public float HexVerticalSpacing()
    {
        return HexHeight() * 0.75f;
    }

    public float HexHorizontalSpacing()
    {
        return HexWidth();
    }

    public Vector3 PositionFromCamera(Vector3 cameraPosition, float numRows, float numColumns)
    {
        float mapHeight = numRows * HexVerticalSpacing();
        float mapWidth = numColumns * HexHorizontalSpacing();

        Vector3 position = Position();

        if (allowWrapEastWest)
        {
            float howManyWidthsFromCamera = (position.x - cameraPosition.x) / mapWidth;

            // We want howManyWidthsFromCamera to be between -0.5 to 0.5
            if (howManyWidthsFromCamera > 0)
                howManyWidthsFromCamera += 0.5f;
            else
                howManyWidthsFromCamera -= 0.5f;

            int howManyWidthToFix = (int) howManyWidthsFromCamera;

            position.x -= howManyWidthToFix * mapWidth;
        }

        if (allowWrapNorthSouth)
        {
            float howManyHeightsFromCamera = (position.z - cameraPosition.z) / mapHeight;

            // We want howManyWidthsFromCamera to be between -0.5 to 0.5
            if (howManyHeightsFromCamera > 0)
                howManyHeightsFromCamera += 0.5f;
            else
                howManyHeightsFromCamera -= 0.5f;

            int howManyHeightsToFix = (int) howManyHeightsFromCamera;

            position.z -= howManyHeightsToFix * mapHeight;
        }


        return position;
    }
}