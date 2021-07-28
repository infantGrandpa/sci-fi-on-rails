using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletSpeed;
    public float rotateSpeed;
    public float secondsToExist;
    public float damage;
    public AudioSource myAudioSource;

    private float secondsLeftBeforeDestroyed;
    public GameObject homingTarget;
    private Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
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

    private void FixedUpdate()
    {
        if (homingTarget != null)
        {
            Vector3 direction = homingTarget.transform.position - transform.position;
            direction.Normalize();
            Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);
            myRigidbody.angularVelocity = rotateAmount * rotateSpeed;
            myRigidbody.velocity = transform.forward * bulletSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject theirGameObject = other.gameObject;
        HealthSystem theirHealthSystem = theirGameObject.GetComponentInParent<HealthSystem>();
        if (theirHealthSystem != null)
        {
            theirHealthSystem.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
