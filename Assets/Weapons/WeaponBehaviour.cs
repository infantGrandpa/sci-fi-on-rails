using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{

    [Header("Bullet Info")]
    public GameObject bulletPrefab;
    public GameObject myBulletSource;

    [Space]

    [Header("Weapon Info")]
    public float secondsBetweenShots;
    private float secondsSinceLastShot;
    public float accuracy;

    [Header("Charging")]
    public bool isChargable = false;
    public float secsToCharge;
    private float secsCharging;

    [Header("Homing")]
    public bool isHoming = false;
    public float homingDistanceZ;
    public float homingDistancyXY;
    public GameObject myHomingReticule;
    public float reticuleDistFromTarg;
    private GameObject closestEnemy;

    private void Start()
    {
        secondsSinceLastShot = secondsBetweenShots;
        secsCharging = 0;
        if (myHomingReticule != null) { myHomingReticule.SetActive(false); }
    }

    private void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
    }

    public void ChargeAndFire(Vector3 targetPosition, bool fireNow)
    {
        if (isChargable)
        {
            //Charge up the weapon
            secsCharging += Time.deltaTime;
            //Debug.Log(secsCharging);
            if (isHoming)
            {
                ////Can currently be only done on the player
                closestEnemy = References.thePlayer.GetClosestEnemyToAimTarget();
                //Vector3 dir = (References.thePlayer.transform.position - closestEnemy.transform.position).normalized;
                //myHomingReticule.transform.rotation = dir;

                //myHomingReticule.SetActive(true);
            }

            //I've we've been told to fire
            if (fireNow)
            {
                //If we're fully charged, fire
                if (secsCharging >= secsToCharge)
                {
                    Fire(targetPosition);
                    secsCharging = 0;
                }
                //If we aren't fully charged, reset
                else
                {
                    secsCharging = 0;
                }
            }
        }
        else
        {
            //If weapon doesn't charge, just fire
            if (fireNow) { Fire(targetPosition); }
        }


    }

    public void Fire(Vector3 targetPosition)
    {

        if (secondsSinceLastShot >= secondsBetweenShots)
        {
            GameObject newBullet = Instantiate(bulletPrefab, myBulletSource.transform.position + myBulletSource.transform.forward, transform.rotation);
            if (isHoming)
            {
                BulletBehaviour bulletBehaviour = newBullet.GetComponent<BulletBehaviour>();
                if (closestEnemy == null)
                {
                    closestEnemy = References.thePlayer.GetClosestEnemyToAimTarget();
                }
                bulletBehaviour.homingTarget = closestEnemy;
            }


            //Change target based on accuracy value
            Vector3 inaccurateTargetPosition = targetPosition;
            //float inaccuracy = Vector3.Distance(transform.position, inaccurateTargetPosition) / accuracy;
            //inaccurateTargetPosition.x += Random.Range(-inaccuracy, inaccuracy);
            //inaccurateTargetPosition.z += Random.Range(-inaccuracy, inaccuracy);
            //Send bullet towards new target
            newBullet.transform.LookAt(inaccurateTargetPosition);
            secondsSinceLastShot = 0;
        }
    }

}
