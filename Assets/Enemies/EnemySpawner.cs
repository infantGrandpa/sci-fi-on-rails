using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool activeOnStart;
    public GameObject myDollyCart;


    [SerializeField]
    private Mesh myMesh;
    [SerializeField]
    private BoxCollider myBoxCollider;
    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(activeOnStart);
        }
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.IsChildOf(References.thePlayer.transform))
        {
            //Child ourselves to the dolly cart
            transform.parent = myDollyCart.transform;
            transform.localPosition = Vector3.zero;
            Collider myCollider = GetComponent<Collider>();
            myCollider.enabled = false;         //Don't allow the trigger to be triggered again
            
            //Activate enemies
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireMesh(myMesh, transform.position, Quaternion.identity, myBoxCollider.size);
        
    }
}
