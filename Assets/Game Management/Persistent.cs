using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent : MonoBehaviour
{
    void Awake()
    {
        if (References.essentials != null)
        {
            Destroy(References.essentials.gameObject);
        }
        References.essentials = this;
        DontDestroyOnLoad(gameObject);
    }
}
