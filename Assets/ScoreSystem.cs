using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    private int myScoreValue;
    
    public void IncreasePlayerScore()
    {
        References.theGameController.IncreaseScore(myScoreValue);
    }

}
