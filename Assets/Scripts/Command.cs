using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class Command
{
    public Command() { }
    public abstract void execute(GameManager gameManager);
}

public class MouseLeftButtonDownCommand : Command
{
    public MouseLeftButtonDownCommand() : base()
    {
    }
    public override void execute(GameManager gameManager)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject ourHitObject = hitInfo.collider.transform.gameObject;
            gameManager.Select(ourHitObject);
        }
        else
        {
            Debug.Log("Raycast hit nothing");
        }
    }
}

public class MouseRightButtonDownCommand : Command
{
    public MouseRightButtonDownCommand() : base()
    {
    }
    public override void execute(GameManager gameManager)
    {
        if(gameManager.UnitsController.SelectedUnit != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject ourHitObject = hitInfo.collider.transform.gameObject;
                if (ourHitObject.name.StartsWith("hex"))
                {
                    Hex hex = ourHitObject.GetComponent<HexComponent>().Hex;
                    if (hex.hexTerrain.Type != TerrainType.MOUNTAIN && hex.hexTerrain.Type != TerrainType.OCEAN)
                    {
                        gameManager.UnitsController.MoveUnit(ourHitObject.GetComponent<HexComponent>().Hex);
                    }
                }
            }
            else
            {
                Debug.Log("Raycast hit nothing");
            }
        }
    }
}

public class ButtonOCommand : Command
{
    public ButtonOCommand() : base() { }
    public override void execute(GameManager gameManager)
    {
    }
}


public class Button1Command : Command
{
    public Button1Command() : base() { }
    public override void execute(GameManager gameManager)
    {
        //TODO: do it in action button event
        if(gameManager.UnitsController.SelectedUnit != null)
        {
            gameManager.UnitsController.ExecuteUnitAction(0);
        }
        if(gameManager.SelectedCity != null)
        {
            gameManager.SelectedCity.CreateUnit(0);
        }
    }
}


public class Button2Command : Command
{
    public Button2Command() : base() { }
    public override void execute(GameManager gameManager)
    {
        //TODO: do it in action button event
        if (gameManager.UnitsController.SelectedUnit != null)
        {
            gameManager.UnitsController.ExecuteUnitAction(1);
        }
        if (gameManager.SelectedCity != null)
        {
            gameManager.SelectedCity.CreateUnit(1);
        }
    }
}

public class Button3Command : Command
{
    public Button3Command() : base() { }
    public override void execute(GameManager gameManager)
    {
        if (gameManager.UnitsController.SelectedUnit != null)
        {
            gameManager.UnitsController.ExecuteUnitAction(2);
        }
        if (gameManager.SelectedCity != null)
        {
            gameManager.SelectedCity.CreateUnit(2);
        }
    }
}

public class Button4Command : Command
{
    public Button4Command() : base() { }
    public override void execute(GameManager gameManager)
    {
        if (gameManager.UnitsController.SelectedUnit != null)
        {
            gameManager.UnitsController.ExecuteUnitAction(3);
        }
        if (gameManager.SelectedCity != null)
        {
            gameManager.SelectedCity.CreateUnit(3);
        }
    }
}

public class ButtonEditorModeSwitch : Command
{
    public ButtonEditorModeSwitch() : base() { }
    public override void execute(GameManager gameManager)
    {
    }
}

public class NextTurnCommand : Command
{
    public NextTurnCommand() : base() { }
    public override void execute(GameManager gameManager)
    {
        gameManager.NextTurn();
    }
}

