using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progres : MonoBehaviour {

    [SerializeField]
    private float FillAmount;
    [SerializeField]
    private Image content;

    
    private void HandleBar()
    {
        
        content.fillAmount = Map(8,0,10,0,1);
    }
    // value - ile zostalo tur inMax ile potrzeba tur
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    } // (8-0)*(1-0)/(10-0)+0 = 8/10
}
