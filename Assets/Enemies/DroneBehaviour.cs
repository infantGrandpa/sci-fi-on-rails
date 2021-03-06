using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{

    public float weaponRange;
    
    private WeaponBehaviour myWeapon;


    // Start is called before the first frame update
    void Start()
    {
        myWeapon = GetComponent<WeaponBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        //Look at Player
        if (References.thePlayer.transform != null)
        {
            Transform playerTransform = References.thePlayer.transform;
            transform.LookAt(playerTransform);
            //Fire at player if in range
            if (Vector3.Distance(transform.position, playerTransform.position) <= weaponRange)
            {
                myWeapon.ChargeAndFire(playerTransform.position, true);
            }
        }
        


    }
}
