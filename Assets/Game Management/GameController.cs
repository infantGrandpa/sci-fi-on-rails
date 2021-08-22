using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int currentScore;

    private void OnEnable()
    {
        References.theGameController = this;
    }

    private void Start()
    {
        SetWallsToTrigger();
    }

    private void Update()
    {
        
    }

    private void SetWallsToTrigger()
    {
        //Sets all walls and props to be triggers - this is so there is no actual collision
        GameObject[] gameObjectList = GameObject.FindGameObjectsWithTag(References.wallsTag); //will return an array of all GameObjects in the scene
        foreach (GameObject thisGameObject in gameObjectList) {        
            //Check if Mesh (to set as convex)
            MeshCollider thisMeshCollider = thisGameObject.GetComponent<MeshCollider>();
            if (thisMeshCollider != null)
            {
                thisMeshCollider.convex = true;
                thisMeshCollider.isTrigger = true;
            } else
            {
                //Check if it has any other collider
                Collider thisCollider = thisGameObject.GetComponent<Collider>();
                if (thisCollider != null)
                {
                    thisCollider.isTrigger = true;
                }
            }
        }
    }
    
    public void IncreaseScore(int increaseBy)
    {
        currentScore += increaseBy;
        References.theCanvas.scoreText.text = currentScore.ToString();
    }


    public void PrefsSetBool(string keyName, bool value)
    {
        PlayerPrefs.SetInt(keyName, value ? 1 : 0);
    }

    public bool PrefsGetBool(string keyName)
    {
        return PlayerPrefs.GetInt(keyName)==1;
    }

    public void PrefsSetFloat(string keyName, float value)
    {
        PlayerPrefs.SetFloat(keyName, value);
    }

    public float PrefsGetFloat(string keyName)
    {
        return PlayerPrefs.GetFloat(keyName);
    }

    public void PrefsSetInt(string keyName, int value)
    {
        PlayerPrefs.SetInt(keyName, value);
    }

    public int PrefsGetInt(string keyName)
    {
        return PlayerPrefs.GetInt(keyName);
    }

    public void PrefsSetString(string keyName, string value)
    {
        PlayerPrefs.SetString(keyName, value);
    }

    public string PrefsGetString(string keyName)
    {
        return PlayerPrefs.GetString(keyName);
    }
}
