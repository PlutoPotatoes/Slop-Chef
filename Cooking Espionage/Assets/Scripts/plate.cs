using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate : MonoBehaviour
{
    private CapsuleCollider foodPickupZone;
    private bool plateFull = false;
    private Rigidbody heldFood;

    // Start is called before the first frame update
    void Start()
    {
        foodPickupZone = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if( heldFood != null)
        {
            // is still using gravity and isn't very smooth, start here
            heldFood.position = transform.position;
            heldFood.useGravity = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!plateFull && other.tag == "Finished_Food")
        {
            other.TryGetComponent<Rigidbody>(out Rigidbody objectRigidBody);
            heldFood = objectRigidBody;


        }
    }
}
