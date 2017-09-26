using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class Currency : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField] public int Id;
    [SerializeField] public Texture2D CurrencyIcon;
    
    [MenuItem("Assets/Create/Currency")]
    public static void CreateAsset()
     {
         ScriptableObjectUtility.CreateAsset<Currency>();
     }
 }
