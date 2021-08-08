using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyController : MonoBehaviour
{
    [SerializeField]
    private GameObject essentialsObject;

    // Start is called before the first frame update
    void Start()
    {
        //Create Essentials if necessary
        if (References.thePlayer == null)
        {
            CreateEssentialsObject();
        }
        //Set the player's path to this
        CinemachineDollyCart playerDolly = References.thePlayer.transform.parent.GetComponent<CinemachineDollyCart>();
        if (playerDolly == null)
        {   
            playerDolly = References.thePlayer.transform.parent.GetComponent<CinemachineDollyCart>();
        }
        playerDolly.m_Path = GetComponent<CinemachineSmoothPath>();
    }


    public void CreateEssentialsObject()
    {
        Instantiate(essentialsObject);
    }
}
