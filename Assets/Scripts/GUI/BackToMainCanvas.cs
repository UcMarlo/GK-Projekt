using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMainCanvas : MonoBehaviour {

    public GameManager GameManager;
    // Use this for initialization
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OpenProgressTree);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OpenProgressTree()
    {
        GameManager.GuiElements.mainCanvas.enabled = true;
        GameManager.GuiElements.ProgressTreeCanvas.enabled = false;
    }
}
