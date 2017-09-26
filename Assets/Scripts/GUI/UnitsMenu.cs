using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsMenu : MonoBehaviour {

    public UnitButton unitButtonPrefab;
    List<UnitButton> unitButtons;
    public UnitDatabase units;
    public GameManager GameManager { get; set; }

    // Use this for initialization
    void Start () {
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Setup(List<UnitAsset> Assets)
    {
        if(unitButtons != null)
        {
            this.transform.localScale -= new Vector3(0, unitButtons.Count, 0);
            foreach (UnitButton button in unitButtons)
                {
                    Destroy(button.gameObject);
                }
        }
        
        unitButtons = new List<UnitButton>();
        for (int i = 0; i < Assets.Count; i++)
        {
            UnitButton newButton = Instantiate(this.unitButtonPrefab, this.transform);
            newButton.Unit = Assets[i];
            newButton.GameManager = this.GameManager;
            newButton.GetComponentInChildren<Text>().text = Assets[i].Name;
            this.unitButtons.Add(newButton);
        }
        this.transform.localScale += new Vector3(0, Assets.Count, 0);
    }
}
