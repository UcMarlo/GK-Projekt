using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseProgresTreePanel : MonoBehaviour {

    public GameObject OptionPanel;

    public void ClosePanel()
    {
        OptionPanel.SetActive(false);
    }
}
