using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void OnEnable()
    {
        References.theGameController = this;
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
