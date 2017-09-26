using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//<summary>
//basic metrics for map generation and future calculations
//</summary>
public static class HexMetrics
{
    public static float floorLevel = 0;
    public static float Radius = 1f;
    public static float halfRadius = Radius * 0.5f;
    public static float Height = 2 * Radius;
    public static float RowHeight = 1.5f * Radius;
    public static float HalfWidth = (float)Mathf.Sqrt((Radius * Radius) - ((Radius / 2) * (Radius / 2)));
    public static float Width = 2 * HalfWidth;
    public static float ExtraHeight = Height - RowHeight;
    public static float Edge = RowHeight - ExtraHeight;
    public static float PointOnEdgeHeight = (innerRadius * 0.5f) * Mathf.Sqrt(3);

    static readonly float WIDTH_MULTILIER = Mathf.Sqrt(3) / 2;

    public const float radius = 1f;

    public static float innerRadius = radius * WIDTH_MULTILIER;

    public static Vector2[] corners =
    {
        new Vector2(0f, radius), //0
        new Vector2(innerRadius, 0.5f * radius),//1
        new Vector2(innerRadius, -0.5f * radius),//2
        new Vector2(0f, -radius),//3
        new Vector2(-innerRadius, -0.5f * radius),//4
        new Vector2(-innerRadius, 0.5f * radius)//5
    };
    
    public static Vector2[] middleCorners =
    {
        new Vector2((corners[1].x + corners[2].x)*0.5f ,(corners[1].y + corners[2].y)*0.5f ), //RIGHT
        new Vector2((corners[4].x + corners[5].x)*0.5f ,(corners[4].y + corners[5].y)*0.5f ), //LEFT
        new Vector2((corners[2].x + corners[3].x)*0.5f ,(corners[2].y + corners[3].y)*0.5f ), //UPPER_RIGHT
        new Vector2((corners[5].x + corners[0].x)*0.5f ,(corners[5].y + corners[0].y)*0.5f ), //DOWN_LEFT
        new Vector2((corners[4].x + corners[3].x)*0.5f ,(corners[4].y + corners[3].y)*0.5f ), //UPPER_LEFT
        new Vector2((corners[0].x + corners[1].x)*0.5f ,(corners[0].y + corners[1].y)*0.5f )  //DOWN_RIGHT
        
    };
    
    public static Vector3[] vertices = 
    {
        new Vector3(0, 0, 0),
        new Vector3(0, floorLevel, -Radius),
        new Vector3(HalfWidth, floorLevel, -halfRadius),
        new Vector3(HalfWidth, floorLevel, halfRadius),
        new Vector3(0, floorLevel, Radius),
        new Vector3(-HalfWidth, floorLevel, halfRadius),
        new Vector3(-HalfWidth, floorLevel, -halfRadius)
    };

    public static Vector2[] UV =
    {
        new Vector2(0.5f, 1),
        new Vector2(1, 0.75f),
        new Vector2(1, 0.25f),
        new Vector2(0.5f, 0),
        new Vector2(0, 0.25f),
        new Vector2(0, 0.75f)
    };
    
    public static int[] triangles =
    {
        1, 0, 2,
        2, 0, 3,
        3, 0, 4,
        4, 0, 5,
        5, 0, 6,
        6, 0, 1
    };
    
    public static int[] getTriangles()
    {
        return triangles;
    }

    public static Vector2[] getUV()
    {
        return UV;
    }

    public static Vector3[] getVertices()
    {
        return vertices;
    }

    public static Vector3[] getVerticiesScaled(float scale) {
        Vector3[] vector3 = new Vector3[6];
        for (int i = 1; i < 7; i ++) {
            vector3[i - 1].x = vertices[i].x * scale;
            vector3[i - 1].y = vertices[i].y * scale;
            vector3[i - 1].z = vertices[i].z * scale;
        }
        return vector3;
    }
}