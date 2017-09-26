using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.ComponentModel;

public class HexTerrainEditor : EditorWindow {
	private enum State
	{
		BLANK,
		EDIT,
		ADD
	}
	
	#region OBJECT INFO
	private State state;
	private int selectedItem;
	private int itemID;
	private TerrainType type;
	private float moveInCostModifier;
	private float elevation;
	private Material _material;
	private string newItemName;
	private int biomeId; 
	#endregion

	private const string ASSET_PATH = @"Assets/Database/HexTerrain/";
	private const string DATABASE_PATH = @"Assets/Database/terrainDB.asset";
	
	private HexTerrainDatabase terrainDatabase;
	private Vector2 _scrollPos;
	
	[MenuItem("Baza/EDYTOR/HexTerrain Database  %#w")]
	public static void Init() {
		HexTerrainEditor window = EditorWindow.GetWindow<HexTerrainEditor>();
		window.minSize = new Vector2(800, 400);
		window.Show();
	}
	
	void OnEnable() {
		if (terrainDatabase == null)
			LoadDatabase();
		
		state = State.BLANK;
	}
	
	void OnGUI() {
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		DisplayListArea();
		DisplayMainArea();
		EditorGUILayout.EndHorizontal();
	}
	
	void LoadDatabase() {
		terrainDatabase = (HexTerrainDatabase)AssetDatabase.LoadAssetAtPath(DATABASE_PATH, typeof(HexTerrainDatabase));
		
		if (terrainDatabase == null)
			CreateDatabase();
	}
	
	void CreateDatabase() {
		terrainDatabase = ScriptableObject.CreateInstance<HexTerrainDatabase>();
		AssetDatabase.CreateAsset(terrainDatabase, DATABASE_PATH);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	
	void DisplayListArea() {
		EditorGUILayout.BeginVertical(GUILayout.Width(250));
		EditorGUILayout.Space();
		
		_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, "box", GUILayout.ExpandHeight(true));
		
		for (int cnt = 0; cnt < terrainDatabase.COUNT; cnt++)
		{
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("-", GUILayout.Width(25)))
			{
				terrainDatabase.RemoveAt(cnt);
				terrainDatabase.SortAlphabeticallyAtoZ();
				EditorUtility.SetDirty(terrainDatabase);
				state = State.BLANK;
				return;
			}
			
			if (GUILayout.Button(terrainDatabase.Terrain(cnt).Name, "box", GUILayout.ExpandWidth(true)))
			{
				selectedItem = cnt;
				state = State.EDIT;
			}
			
			EditorGUILayout.EndHorizontal();
		}
		
		EditorGUILayout.EndScrollView();
		
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		EditorGUILayout.LabelField("HexTerrain: " + terrainDatabase.COUNT, GUILayout.Width(100));
		
		if (GUILayout.Button("New Terrain"))
			state = State.ADD;
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();
	}
	
	void DisplayMainArea()
	{
		EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
		EditorGUILayout.Space();
		
		switch (state)
		{
		case State.ADD:
			DisplayAddMainArea();
			break;
		case State.EDIT:
			DisplayEditMainArea();
			break;
		default:
			DisplayBlankMainArea();
			break;
		}
		
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();
	}
	
	void DisplayBlankMainArea()
	{
		EditorGUILayout.LabelField(
			"THIS IS A TERRAIN EDITOR \n" +
			"PLASE BE GENTLE\n" +
			"Elevation should be arround -2 to +2 \n" +
			"minus values are for water oriented terrains.",
			GUILayout.ExpandHeight(true));
	}
	
	void DisplayEditMainArea()
	{
		terrainDatabase.Terrain(selectedItem).Name = EditorGUILayout.TextField(new GUIContent("Name: "), terrainDatabase.Terrain(selectedItem).Name);
		terrainDatabase.Terrain(selectedItem).BiomeId = EditorGUILayout.IntField("Biome ID", terrainDatabase.Terrain(selectedItem).BiomeId);
		terrainDatabase.Terrain(selectedItem).Type = (TerrainType)EditorGUILayout.EnumPopup("Type of terrain:", terrainDatabase.Terrain(selectedItem).Type);
		terrainDatabase.Terrain(selectedItem).MoveInCostModifier = EditorGUILayout.FloatField("Move in cost modifier", terrainDatabase.Terrain(selectedItem).MoveInCostModifier);
		terrainDatabase.Terrain(selectedItem).Elevation = EditorGUILayout.FloatField("Elevation", terrainDatabase.Terrain(selectedItem).Elevation);
		terrainDatabase.Terrain(selectedItem).Material = (Material) EditorGUILayout.ObjectField(terrainDatabase.Terrain(selectedItem).Material, typeof(Material), true);
		
		EditorGUILayout.Space();
		
		if (GUILayout.Button("Done", GUILayout.Width(100)))
		{
			terrainDatabase.SortAlphabeticallyAtoZ();
			EditorUtility.SetDirty(terrainDatabase);
			state = State.BLANK;
		}
	}
	
	void DisplayAddMainArea()
	{
		newItemName = EditorGUILayout.TextField(new GUIContent("Name: "), newItemName);
		biomeId = EditorGUILayout.IntField("Biome ID", biomeId);
		type = (TerrainType)EditorGUILayout.EnumPopup("Type of terrain:", type);
		moveInCostModifier = EditorGUILayout.FloatField("Move in cost modifier", moveInCostModifier);
		elevation = EditorGUILayout.FloatField("Elevation", elevation);
		_material = (Material) EditorGUILayout.ObjectField(_material, typeof(Material), true);
		
		EditorGUILayout.Space();
		
		if (GUILayout.Button("Done", GUILayout.Width(100)))
		{
			HexTerrain newHexTerrain = new HexTerrain(itemID ,newItemName, type, moveInCostModifier,elevation,_material,biomeId);
			//string dataPath = ASSET_PATH + newHexTerrain.Name + ".asset";
			itemID = terrainDatabase.COUNT;
			
			terrainDatabase.Add(newHexTerrain);
			
			
			newItemName = string.Empty;
			EditorUtility.SetDirty(terrainDatabase);
			state = State.BLANK;
		}
	}
}