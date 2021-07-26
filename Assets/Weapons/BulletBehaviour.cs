using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletSpeed;
    public float secondsToExist;
    public float damage;

    private float secondsLeftBeforeDestroyed;

    private void Start()
    {
        Rigidbody myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = transform.forward * bulletSpeed;
        secondsLeftBeforeDestroyed = secondsToExist;
    }

    private void Update()
    {
        secondsLeftBeforeDestroyed -= Time.deltaTime;

        if (secondsLeftBeforeDestroyed <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject theirGameObject = collision.gameObject;
        HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();
        if (theirHealthSystem != null)
        {
            theirHealthSystem.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
