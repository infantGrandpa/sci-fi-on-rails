using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CinemachineDollyCart playerDolly = References.thePlayer.transform.parent.GetComponent<CinemachineDollyCart>();
        Debug.Log(playerDolly.name);
        playerDolly.m_Path = GetComponent<CinemachineSmoothPath>();
    }


}
