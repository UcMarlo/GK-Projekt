using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButton : MonoBehaviour {

    public GameObject OptionPanel;
    public void Clicked()
    {
        Application.Quit();
    }
}
