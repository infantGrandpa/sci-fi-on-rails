using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EnemyRailsBehaviour : MonoBehaviour
{
    private CinemachineDollyCart myDolly;

    private void OnEnable()
    {
        References.theEnemyDolly = this;
    }

    private void Start()
    {
        myDolly = GetComponent<CinemachineDollyCart>();
    }

    public void setSpeed(float targetSpeed)
    {
        myDolly.m_Speed = targetSpeed;
    }
}
