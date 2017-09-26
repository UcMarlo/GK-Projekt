using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanelOption : MonoBehaviour {

    public GameObject OptionPanel;

    public void OpenPanel()
    {
        OptionPanel.SetActive(true);
    }
}
