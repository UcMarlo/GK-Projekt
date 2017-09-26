using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanelOption : MonoBehaviour {

    public GameObject OptionPanel;

    public void ClosePanel()
    {
        OptionPanel.SetActive(false);
    }
}
