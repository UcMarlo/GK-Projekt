using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : Button {

    private string text;
    private UnitAsset unitAsset;
    public GameManager GameManager { get; set; }


    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(CreateUnit);
    }

    public string Text
    {
        get
        {
            return text;
        }

        set
        {
            text = value;
        }
    }

    public UnitAsset Unit
    {
        get
        {
            return unitAsset;
        }

        set
        {
            unitAsset = value;
        }
    }

    private void CreateUnit()
    {
        GameManager.SelectedCity.CreateUnit(unitAsset);
    }
}
