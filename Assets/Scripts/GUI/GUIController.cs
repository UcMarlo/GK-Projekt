using Assets.Scripts.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GUIController
{
    public GUIElements guiElements { get; set; }
    GameManager gameManager;

    public GUIController(GUIElements guiElements, GameManager gameManager)
    {
        this.guiElements = guiElements;
        this.gameManager = gameManager;
        this.guiElements.UnitCreateMenuContent.GameManager = gameManager;
    }

    public void InitGUI()
    {
        //mainCanvas = GameObject.Instantiate(new Canvas());
    }

    public void Deactivate()
    {
        guiElements.UnitActionMenu.SetActive(false);
        guiElements.UnitCreateMenu.SetActive(false);
    }

    public void ShowUnitsCreateMenu(Player player)
    {
        guiElements.UnitCreateMenuContent.Setup(player.AvailableUnits);
        guiElements.UnitCreateMenu.SetActive(true);
    }

    public void ShowUnitActionMenu(Hex hex)
    {
        guiElements.UnitActionMenu.SetActive(true);
    }
}
