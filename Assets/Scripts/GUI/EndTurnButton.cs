using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour {

    [SerializeField]
    public GameManager GameManager;
	// Use this for initialization
	void Start () {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate { GameManager.NextTurn(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
