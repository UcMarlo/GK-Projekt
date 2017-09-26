using System;
using Assets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

//TODO: change it to WorldManager?
public class HexMap : MonoBehaviour
{
    public int MapColumns;
    public int MapRows;

    [SerializeField] private GameObject _hexPrefab;

    Hex[,] hexes;
    GameObject[,] hexesGameObjects;

    public HexTerrainDatabase HexTerrainDatabase;
    public CurrencyDatabase CurrencyDatabase;

    public Material[] HexMaterials;
    public Mesh LocalMesh;
    [SerializeField] public GameObject WaterPrefabGameObject;
    [System.NonSerialized] public GameObject WaterInstance;

    public float ScaleX = 1f;
    public float ScaleY = 1f;
    public float ScaleXInverted = -1f;
    public float ScaleYInverted = -1f;

    //For procedural map generation
    [SerializeField] public float WaterYPos = -0.2f;

    [SerializeField] public float HeightMountain = 1f;
    [SerializeField] public float HeightHill = 0.7f;
    [SerializeField] public float HeightFlat = 0.3f;

   
    void Awake()
    {
        //SetupPrefab();
        GenerateMap();
        CreateGraph();
        ApplyHeights();
        ApplyPerlinNoise();
        InstantiateWater();
    }

    private Dictionary<Hex, GameObject> _hexToGameObjectMap;
    private Dictionary<GameObject, Hex> _gameObjectToHexMap;


    public bool AllowHorizontalWrap = true;
    public bool AllowVerticalWrap = false;

    void MeshSetup()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        LocalMesh = new Mesh();

        LocalMesh.vertices = HexMetrics.getVertices();
        LocalMesh.triangles = HexMetrics.getTriangles();
        LocalMesh.uv = HexMetrics.getUV();

        LocalMesh.RecalculateNormals();

        meshFilter.mesh = LocalMesh;
    }

    private void SetupPrefab()
    {
        _hexPrefab = new GameObject("flat hexagon prefab");
        LocalMesh.RecalculateNormals();
        MeshFilter meshFilter = _hexPrefab.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = LocalMesh;
        MeshRenderer meshRenderer = _hexPrefab.AddComponent<MeshRenderer>();
    }

    private void InstantiateWater()
    {
        Vector3 MapPosistion = this.transform.position;
        MapPosistion.y = -0.2f;
        WaterInstance =
            (GameObject) Instantiate(WaterPrefabGameObject, MapPosistion, Quaternion.identity, this.transform);
        WaterInstance.name = "WaterMapInstance";
        //WaterInstance.transform.localScale = new Vector3(100,100,100); //TODO: crate scale based on map size
    }

    #region MAPGENERATION

    protected virtual void GenerateMap()
    {
        _hexToGameObjectMap = new Dictionary<Hex, GameObject>();
        _gameObjectToHexMap = new Dictionary<GameObject, Hex>();


        hexes = new Hex[MapColumns, MapRows];
        hexesGameObjects = new GameObject[MapColumns, MapRows];
        for (int column = 0; column < MapColumns; column++)
        {
            for (int row = 0; row < MapRows; row++)
            {
                HexTerrain chosenTerrain =
                    HexTerrainDatabase.Terrain(UnityEngine.Random.Range(0, HexTerrainDatabase.COUNT));
                Hex h = new Hex(this, chosenTerrain, column, row);
                hexes[column, row] = h;
                GameObject hexInstance = (GameObject) Instantiate(
                    _hexPrefab,
                    h.Position(),
                    Quaternion.AngleAxis(-90, new Vector3(1, 0, 0)),
                    this.transform
                );
                hexInstance.AddComponent<MeshCollider>();

                hexInstance.name = "hex" + "_" + column + "_" + row;

                hexInstance.GetComponent<HexComponent>().Hex = h;
                hexInstance.GetComponent<HexComponent>().HexMap = this;
                hexInstance.AddComponent<MeshCollider>();
                //TODO: comment this after debugging
                hexInstance.GetComponentInChildren<TextMesh>().text = column + "-" + row;

                MeshRenderer mr = hexInstance.GetComponent<MeshRenderer>();
                mr.material = chosenTerrain.Material;
                hexesGameObjects[column, row] = hexInstance;
                _hexToGameObjectMap[h] = hexInstance;
                _gameObjectToHexMap[hexInstance] = h;
            }
        }
    }

    public void UpdateWaterPosition()
    {
    }

    private void ApplyHeights()
    {
        foreach (KeyValuePair<Hex, GameObject> entry in _hexToGameObjectMap)
        {
            Vector3[] vertices = entry.Value.GetComponent<MeshFilter>().mesh.vertices;
            Hex[] neightbours = entry.Key.GetNeightbours();

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].z = (float) ExpFunction(vertices[i].x, vertices[i].y, entry.Key.hexTerrain.Elevation, 0, 0,
                    HexMetrics.innerRadius * 0.35f); //CENTER

                vertices[i].z = CalculateHeightsForNeightbour(vertices[i], entry.Key, //RIGHT
                    neightbours[(int) NeightbourDirections.RIGHT],
                    HexMetrics.middleCorners[(int) NeightbourDirections.RIGHT]);

                vertices[i].z = CalculateHeightsForNeightbour(vertices[i], entry.Key, //LEFT
                    neightbours[(int) NeightbourDirections.LEFT],
                    HexMetrics.middleCorners[(int) NeightbourDirections.LEFT]);

                vertices[i].z = CalculateHeightsForNeightbour(vertices[i], entry.Key, //UPPER RIGHT
                    neightbours[(int) NeightbourDirections.UPPER_RIGHT],
                    HexMetrics.middleCorners[(int) NeightbourDirections.UPPER_RIGHT]);

                vertices[i].z = CalculateHeightsForNeightbour(vertices[i], entry.Key, //UPPER LEFT
                    neightbours[(int) NeightbourDirections.UPPER_LEFT],
                    HexMetrics.middleCorners[(int) NeightbourDirections.UPPER_LEFT]);

                vertices[i].z = CalculateHeightsForNeightbour(vertices[i], entry.Key, //DOWN LEFT
                    neightbours[(int) NeightbourDirections.DOWN_LEFT],
                    HexMetrics.middleCorners[(int) NeightbourDirections.DOWN_LEFT]);

                vertices[i].z = CalculateHeightsForNeightbour(vertices[i], entry.Key, //DOWN RIGHT
                    neightbours[(int) NeightbourDirections.DOWN_RIGHT],
                    HexMetrics.middleCorners[(int) NeightbourDirections.DOWN_RIGHT]);

                vertices[i].z = CalculateHeightsForTwoNeightbours(vertices[i], entry.Key,
                    neightbours[(int) NeightbourDirections.UPPER_LEFT],
                    neightbours[(int) NeightbourDirections.UPPER_RIGHT],
                    HexMetrics.corners[3]); //TOP

                vertices[i].z = CalculateHeightsForTwoNeightbours(vertices[i], entry.Key,
                    neightbours[(int) NeightbourDirections.UPPER_RIGHT], neightbours[(int) NeightbourDirections.RIGHT],
                    HexMetrics.corners[4]); //TOP RIGHT

                vertices[i].z = CalculateHeightsForTwoNeightbours(vertices[i], entry.Key,
                    neightbours[(int) NeightbourDirections.UPPER_LEFT], neightbours[(int) NeightbourDirections.LEFT],
                    HexMetrics.corners[2]); //TOP LEFT

                vertices[i].z = CalculateHeightsForTwoNeightbours(vertices[i], entry.Key,
                    neightbours[(int) NeightbourDirections.DOWN_LEFT],
                    neightbours[(int) NeightbourDirections.DOWN_RIGHT],
                    HexMetrics.corners[0]); //DOWN

                vertices[i].z = CalculateHeightsForTwoNeightbours(vertices[i], entry.Key,
                    neightbours[(int) NeightbourDirections.DOWN_LEFT], neightbours[(int) NeightbourDirections.LEFT],
                    HexMetrics.corners[1]); //DOWN LEFT

                vertices[i].z = CalculateHeightsForTwoNeightbours(vertices[i], entry.Key,
                    neightbours[(int) NeightbourDirections.DOWN_RIGHT], neightbours[(int) NeightbourDirections.RIGHT],
                    HexMetrics.corners[5]); //DOWN RIGHT
            }

            entry.Value.GetComponent<MeshFilter>().mesh.vertices = vertices;
            entry.Value.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
            entry.Value.GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
        }
    }

    private void ApplyHeightEdgeFix()
    {
    }

    private float CalculateHeightsForNeightbour(Vector3 originalHexVector, Hex currentHex,
        Hex neigthbourHex, Vector2 shiftVector)
    {
        float height = (currentHex.hexTerrain.Elevation + neigthbourHex.hexTerrain.Elevation) * 0.2f;
        return originalHexVector.z + (float) ExpFunction(originalHexVector.x, originalHexVector.y, height,
                   shiftVector.x, shiftVector.y, HexMetrics.innerRadius * 0.3f);
    }

    private float CalculateHeightsForTwoNeightbours(Vector3 originalHexVector, Hex currentHex,
        Hex neigthbourHex, Hex neightbourHex2, Vector2 shiftVector)
    {
        if ((currentHex.hexTerrain.Elevation == neigthbourHex.hexTerrain.Elevation) &&
            (currentHex.hexTerrain.Elevation == neightbourHex2.hexTerrain.Elevation))
        {
            float height = (currentHex.hexTerrain.Elevation + neigthbourHex.hexTerrain.Elevation +
                            neightbourHex2.Elevation) * 0.2f;
            return originalHexVector.z + (float) ExpFunction(originalHexVector.x, originalHexVector.y, height,
                       -shiftVector.x, shiftVector.y, HexMetrics.innerRadius * 0.3f);
        }
        else
        {
            return originalHexVector.z;
        }
    }

    private void ApplyPerlinNoise()
    {
        foreach (KeyValuePair<Hex, GameObject> entry in _hexToGameObjectMap)
        {
            Vector3[] vertices = entry.Value.GetComponent<MeshFilter>().mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 worldPnt = GetVertexWorldPosition(vertices[i], entry.Value.transform);
                vertices[i].z = vertices[i].z + 0.2f * Mathf.PerlinNoise((worldPnt.x * 4.2f), (worldPnt.z * 4.2f));
            }
            entry.Value.GetComponent<MeshFilter>().mesh.vertices = vertices;
            entry.Value.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
            entry.Value.GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
        }
    }

    private void CreateGraph()
    {
        foreach (Hex hex in hexes)
        {
            hex.GetNeightbours();
        }
    }

    private double ExpFunction(float x, float y, float HeightScale, float xShift, float yShift, float radius)
    {
        return HeightScale *
               Math.Exp(-((Math.Pow((x - xShift), 2) + Math.Pow((y - yShift), 2))) / (2 * Math.Pow(radius, 2)));
    }

    //exp(-((X-0.5).^2+(Y-0.5).^2)/(2*r^2));
    private Vector3 GetRelativePositiveVec3(Vector3 vector)
    {
        return new Vector3(vector.x + HexMetrics.Radius, vector.y + HexMetrics.Radius, vector.z);
    }

    private Vector3 GetVertexWorldPosition(Vector3 vertex, Transform owner)
    {
        return owner.localToWorldMatrix.MultiplyPoint3x4(vertex);
    }


    public void UpdateHexTerrains()
    {
        for (int column = 0; column < MapColumns; column++)
        {
            for (int row = 0; row < MapRows; row++)
            {
                Hex h = hexes[column, row];

                GameObject hexGO = _hexToGameObjectMap[h];

                HexComponent hexComponent = hexGO.GetComponentInChildren<HexComponent>();
                MeshRenderer meshRenderer = hexGO.GetComponentInChildren<MeshRenderer>();

                int biomeId = 0; // In the future biome id will be determinated procedurally

                if (h.Elevation >= HeightMountain)
                {
                    h.hexTerrain = getTypeRandomFromDatabaseBy(TerrainType.MOUNTAIN, biomeId);
                }
                else if (h.Elevation >= HeightHill)
                {
                    h.hexTerrain = getTypeRandomFromDatabaseBy(TerrainType.HILL, biomeId);
                }
                else if (h.Elevation >= HeightFlat)
                {
                    h.hexTerrain = getTypeRandomFromDatabaseBy(TerrainType.FLAT, biomeId);
                }
                else
                {
                    h.hexTerrain = getTypeRandomFromDatabaseBy(TerrainType.OCEAN, biomeId);
                }

                meshRenderer.material = h.hexTerrain.Material; // Update material;
            }
        }
    }

    #endregion

    public Hex GetHexAt(int x, int y)
    {
        if (hexes == null)
        {
            return null;
        }


        x = x % MapColumns;
        if (x < 0)
        {
            x += MapColumns;
        }

        y = y % MapRows;
        if (y < 0)
        {
            y += MapRows;
        }


        try
        {
            return hexes[x, y];
        }
        catch
        {
            Debug.LogError("hex not found");
            return null;
        }
    }

    public GameObject GetHexGOAt(int x, int y)
    {
        if (hexesGameObjects == null)
        {
            return null;
        }

        x = x % MapColumns;
        if (x < 0)
        {
            x += MapColumns;
        } 
        y = y % MapRows;
        if (y < 0)
        {
            y += MapRows;
        }

        try
        {
            return hexesGameObjects[x, y];
        }
        catch
        {
            Debug.LogError("hex not found");
            return null;
        }
    }

    public Hex[] GetHexesWithinRangeOf(Hex centerHex, int range)
    {
        List<Hex> results = new List<Hex>();

        for (int dx = -range; dx < range - 1; dx++)
        {
            for (int dy = Mathf.Max(-range + 1, -dx - range); dy < Mathf.Min(range, -dx + range - 1); dy++)
            {
                results.Add(GetHexAt(centerHex.Q + dx, centerHex.R + dy));
            }
        }

        return results.ToArray();
    }

    public void placeUnitAt(int x, int y, Unit unit)
    {
        Hex hex = GetHexAt(x, y);
    }


    #region HexTerrainDatabase

    HexTerrain getTypeFromDatabase(TerrainType terrainType, int biomeId)
    {
        return HexTerrainDatabase.getAllRecords().First(item => item.Type == terrainType && item.BiomeId == biomeId);
    }

    
    HexTerrain getTypeRandomFromDatabaseBy(TerrainType terrainType, int biomeId)
    {
        List<HexTerrain> terrains =  HexTerrainDatabase.getAllRecords().FindAll(item => item.Type == terrainType && item.BiomeId == biomeId);
        return terrains[Random.Range(0, terrains.Count)];
    }
    #endregion
}


public enum NeightbourDirections
{
    RIGHT = 0,
    LEFT = 1,
    UPPER_RIGHT = 2,
    DOWN_LEFT = 3,
    UPPER_LEFT = 4,
    DOWN_RIGHT = 5
};