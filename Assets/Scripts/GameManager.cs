using Assets.Scripts.GUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    InputHandler inputHandler;
    Command command;
    public List<Player> Players;
    public GameObject cityPrefab;
    public int ActuallPlayerNumber { get; set; }
    public MapManager MapManager { get; set; }
    public GUIController GUIController;
    public GUIElements GuiElements;
    public HexMap HexMap { get; set; }
    public UnitDatabase UnitDatabase;
    public UnitsController UnitsController;
    public City SelectedCity { get; set; }
    public ProgressTree progressTree;

    // Use this for initialization
    void Start () {
        GUIController = new GUIController(GuiElements,this);
        inputHandler = new InputHandler();
        HexMap = FindObjectOfType<HexMap>();
        InitPlayers(2);
        UnitsController = new UnitsController();

        progressTree = Instantiate(progressTree);
        progressTree.ProgressTreeCanvas = GuiElements.ProgressTreeCanvas;
        progressTree.Setup();
        progressTree.LoadTree(false);
        progressTree.GameManager = this;
        GuiElements.ProgressTreeCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        command = inputHandler.getInput();
        if (command != null)
        {
            command.execute(this);
        }
    }

    void InitPlayers(int playersCount)
    {
        Players = new List<Player>();
        for(int i = 0; i < playersCount; i++)
        {
            Players.Add(new Player(i, this));
            Players[i].Init(5 + (i*5), 2);
        }
        ActuallPlayerNumber = 0;
    }

    public void Select(GameObject ourHitObject)
    {
        GUIController.Deactivate();
        switch (ourHitObject.transform.name.Split('_')[0])
        {
            case "unit":
                //UnitsController.SelectUnit(ourHitObject, Players[ActuallPlayerNumber]);
                break;
            case "hex":
                Hex selectedHex = ourHitObject.GetComponent<HexComponent>().Hex;
                if (selectedHex.Unit != null)
                {
                    UnitsController.SelectUnit(selectedHex.Unit.UnitGameObject, Players[ActuallPlayerNumber]);
                    GuiElements.UnitActionMenu.SetActive(true);
                    Text[] text = GuiElements.UnitActionMenu.GetComponentsInChildren<Text>();
                    text[0].text = selectedHex.Unit.PrivateName;
                    text[1].text = selectedHex.Unit.CurrentHealth.ToString();
                }
                else
                {
                    UnitsController.UnselectSelectedUnit();
                }
                //TODO: Select Hex
                break;
            case "city":
                UnitsController.UnselectSelectedUnit();
                SelectedCity = ourHitObject.GetComponent<CityComponent>().CityLogic;
                ourHitObject.GetComponent<CityComponent>().CityLogic.SelectCity(ActuallPlayerNumber);
                GUIController.ShowUnitsCreateMenu(Players[ActuallPlayerNumber]);
                break;
            default:
                break;
        }
    }

    public void NextTurn()
    {
        ActuallPlayerNumber++;
        if (ActuallPlayerNumber >= Players.Count) 
        {
            ActuallPlayerNumber = 0;
        }
        Debug.Log("Player " + ActuallPlayerNumber.ToString() + " turn.");
        UnitsController.TakeToNextTurn(Players[ActuallPlayerNumber].units);
        progressTree.NextTurn();
    }
}
