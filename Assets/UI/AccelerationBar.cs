using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationBar : MonoBehaviour
{
    public Image filledBar;

    public void ShowAccelerationFraction(float fraction)
    {
        //Scales the filled part of the Acceleration bar based on the fraction
        filledBar.rectTransform.localScale = new Vector3(1, fraction, 1);
        Debug.Log(fraction);
    }


    private void OnEnable()
    {
        References.theAccelerationBar = this;
        
    }
}
