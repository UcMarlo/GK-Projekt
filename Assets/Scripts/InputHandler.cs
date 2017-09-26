using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler
{

    private MouseLeftButtonDownCommand mouseLeftButtonDown;
    private MouseRightButtonDownCommand mouseRightButtonDown;
    private Command buttonO;
    private Command button1;
    private Command button2;
    private Command button3;
    private Command button4;
    private Command buttonN;

    public InputHandler()
    {
        mouseLeftButtonDown = new MouseLeftButtonDownCommand();
        mouseRightButtonDown = new MouseRightButtonDownCommand();
        buttonO = new ButtonOCommand();
        button1 = new Button1Command();
        button2 = new Button2Command();
        button3 = new Button3Command();
        button4 = new Button4Command();
        buttonN = new NextTurnCommand();
    }

    public Command getInput()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
            if (Input.anyKey)
            {
                if (Input.GetMouseButtonDown(0)) return mouseLeftButtonDown;
                if (Input.GetMouseButtonDown(1)) return mouseRightButtonDown;
                if (Input.GetKeyDown("o")) return buttonO;
                if (Input.GetKeyDown("1")) return button1;
                if (Input.GetKeyDown("2")) return button2;
                if (Input.GetKeyDown("3")) return button3;
                if (Input.GetKeyDown("4")) return button4;
                if (Input.GetKeyDown("n")) return buttonN;
            }
        return null;
    }
}
