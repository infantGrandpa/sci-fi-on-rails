using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float secondsBetweenShots;
    private float secondsSinceLastShot;
    public float accuracy;

    private void Start()
    {
        secondsSinceLastShot = 0;
    }

    private void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
    }

    public void Fire(Vector3 targetPosition)
    {
        if (secondsSinceLastShot >= secondsBetweenShots)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);

            //Change target based on accuracy value
            Vector3 inaccurateTargetPosition = targetPosition;
            //float inaccuracy = Vector3.Distance(transform.position, inaccurateTargetPosition) / accuracy;
            //inaccurateTargetPosition.x += Random.Range(-inaccuracy, inaccuracy);
            //inaccurateTargetPosition.z += Random.Range(-inaccuracy, inaccuracy);
            //Send bullet towards new target
            newBullet.transform.LookAt(inaccurateTargetPosition);
            secondsSinceLastShot = secondsBetweenShots;
        }
    }

}
